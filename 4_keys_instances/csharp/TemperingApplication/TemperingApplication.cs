﻿/*
 * (c) Copyright, Real-Time Innovations, 2020.  All rights reserved.
 * RTI grants Licensee a license to use, modify, compile, and create derivative
 * works of the software solely for use with RTI Connext DDS. Licensee may
 * redistribute copies of the software provided that all such copies are subject
 * to this license. The software is provided "as is", with no warranty of any
 * type, including any warranty for fitness for any purpose. RTI is under no
 * obligation to maintain or support the software. RTI shall not be liable for
 * any incidental or consequential damages arising out of the use or inability
 * to use the software.
 */

using System;
using System.Threading;
using System.Threading.Tasks;
using Omg.Dds.Core;
using Rti.Dds.Core;
using Rti.Dds.Core.Status;
using Rti.Dds.Domain;
using Rti.Dds.Publication;
using Rti.Dds.Subscription;
using Rti.Dds.Topics;

namespace KeysInstances
{
    /// <summary>
    /// TemperingApplication
    /// </summary>
    public class TemperingApplication
    {
        private readonly Random rand = new Random();
        private bool shutdownRequested;

        private void PublishTemperature(
            DataWriter<Temperature> writer,
            string sensorId)
        {
            // Create temperature sample for writing
            var temperature = writer.CreateData();
            while (!shutdownRequested)
            {
                // Modify the data to be written here
                temperature.sensor_id = sensorId;

                // Currently we don't send above 32 degrees, to make the output
                // in the MonitoringCtrlApplication more readable. Increase the
                // range here to see the temperature printed in the
                // MonitoringCtrlApplication
                temperature.degrees = rand.Next(30, 33);

                writer.Write(temperature);

                Thread.Sleep(100);
            }
        }

        private void ProcessLot(
            DataReader<ChocolateLotState> lotStateReader,
            DataWriter<ChocolateLotState> lotStateWriter)
        {
            // Take all samples. Samples are loaned to application, loan is
            // returned when LoanedSamples is Disposed. ValidData iterates only over
            // samples such that sample.Info.ValidData is true.
            using var samples = lotStateReader.Take();
            foreach (var sample in samples.ValidData())
            {
                if (sample.next_station == StationKind.TEMPERING_CONTROLLER)
                {
                    Console.WriteLine("Processing lot #" + sample.lot_id);

                    // Send an update that the tempering station is processing lot
                    var updatedState = new ChocolateLotState(sample)
                    {
                        lot_status = LotStatusKind.PROCESSING,
                        next_station = StationKind.INVALID_CONTROLLER,
                        station = StationKind.TEMPERING_CONTROLLER
                    };
                    lotStateWriter.Write(updatedState);

                    // "Processing" the lot.
                    Thread.Sleep(5000);

                    // Exercise #3.1: Since this is the last step in processing,
                    // notify the monitoring application that the lot is complete
                    // using a dispose
                }
            }
        }

        private void RunExample(int domainId, string sensorId)
        {
            // A DomainParticipant allows an application to begin communicating in
            // a DDS domain. Typically there is one DomainParticipant per application.
            // DomainParticipant QoS is configured in USER_QOS_PROFILES.xml
            DomainParticipant participant = DomainParticipantFactory.Instance
                .CreateParticipant(domainId);
            
            // A Topic has a name and a datatype.
            Topic<Temperature> temperatureTopic = participant.CreateTopic<Temperature>(
                "ChocolateTemperature");
            Topic<ChocolateLotState> lotStateTopic = participant.CreateTopic<ChocolateLotState>(
                "ChocolateLotState");

            // A Publisher allows an application to create one or more DataWriters
            // Publisher QoS is configured in USER_QOS_PROFILES.xml
            Publisher publisher = participant.CreatePublisher();

            // Create DataWriters of Topics "ChocolateTemperature" & "ChocolateLotState"
            // DataWriter QoS is configured in USER_QOS_PROFILES.xml
            DataWriter<Temperature> temperatureWriter =
                publisher.CreateDataWriter(temperatureTopic);
            DataWriter<ChocolateLotState> lotStateWriter =
                publisher.CreateDataWriter(lotStateTopic);

            // A Subscriber allows an application to create one or more DataReaders
            // Subscriber QoS is configured in USER_QOS_PROFILES.xml
            Subscriber subscriber = participant.CreateSubscriber();

            // This DataReader reads data of type Temperature on Topic
            // "ChocolateTemperature". DataReader QoS is configured in
            // USER_QOS_PROFILES.xml
            DataReader<ChocolateLotState> lotStateReader =
                subscriber.CreateDataReader(lotStateTopic);

            // Obtain the DataReader's Status Condition
            StatusCondition statusCondition = lotStateReader.StatusCondition;

            // Enable the 'data available' status.
            statusCondition.EnabledStatuses = StatusMask.DataAvailable;

            // Associate an event handler with the status condition.
            // This will run when the condition is triggered, in the context of
            // the dispatch call (see below)
            statusCondition.Triggered +=
                _ => ProcessLot(lotStateReader, lotStateWriter);

            // Create a WaitSet and attach the StatusCondition
            var waitset = new WaitSet();
            waitset.AttachCondition(statusCondition);

            // Create a thread to periodically publish the temperature
            Console.WriteLine($"ChocolateTemperature Sensor with ID: {sensorId} starting");
            var temperatureTask = Task.Run(
                () => PublishTemperature(temperatureWriter, sensorId));

            while (!shutdownRequested)
            {
                // Wait for ChocolateLotState
                Console.WriteLine("Waiting for lot");
                waitset.Dispatch(Duration.FromSeconds(4));
            }

            temperatureTask.Wait();
        }

        /// <summary>
        /// Main function, receiving structured command-line arguments
        /// via the System.Console.DragonFruit package.
        /// For example: dotnet run -- --sensor-id mySensor
        /// </summary>
        /// <param name="domainId">The domain ID to create the DomainParticipant</param>
        /// <param name="sensorId">Identifies the sensor ID used in the example</param>
        public static void Main(int domainId = 0, string sensorId = "default_id")
        {
            var example = new TemperingApplication();

            // Setup signal handler
            Console.CancelKeyPress += (_, eventArgs) =>
            {
                Console.WriteLine("Shuting down...");
                eventArgs.Cancel = true; // let the application shutdown gracefully
                example.shutdownRequested = true;
            };

            try
            {
                example.RunExample(domainId, sensorId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("RunExample exception: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
