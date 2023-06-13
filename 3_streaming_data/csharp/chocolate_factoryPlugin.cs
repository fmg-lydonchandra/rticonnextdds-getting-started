/*
WARNING: THIS FILE IS AUTO-GENERATED. DO NOT MODIFY.

This file was generated from chocolate_factory.idl
using RTI Code Generator (rtiddsgen) version 3.1.1.
The rtiddsgen tool is part of the RTI Connext DDS distribution.
For more information, type 'rtiddsgen -help' at a command shell
or consult the Code Generator User's Manual.
*/

using System;
using System.Runtime.InteropServices;
using Omg.Types;
using Omg.Types.Dynamic;
using Rti.Types;
using Rti.Dds.Core;
using Rti.Types.Dynamic;
using Rti.Dds.NativeInterface.TypePlugin;

namespace Implementation
{

    public struct TemperatureUnmanaged : Rti.Dds.NativeInterface.TypePlugin.INativeTopicType<Temperature>
    {

        private NativeString sensor_id;
        private int degrees;

        public void Destroy(bool optionalsOnly)
        {
            if (optionalsOnly)
            {
                return;
            }
            sensor_id.Destroy();
        }

        public void FromNative(Temperature sample, bool keysOnly = false)
        {

            sample.sensor_id = sensor_id.FromNative();
            sample.degrees = degrees;
        }

        public void Initialize(bool allocatePointers = true, bool allocateMemory = true)
        {
            sensor_id.Initialize(size: ((int) 256), allocateMemory: allocateMemory);
            degrees = (int) 0;
        }

        public void ToNative(Temperature sample, bool keysOnly = false)
        {
            sensor_id.ToNative(sample.sensor_id, ((int) 256));
            degrees = sample.degrees;
        }
    }

    internal class TemperaturePlugin : Rti.Dds.NativeInterface.TypePlugin.InterpretedTypePlugin<Temperature, TemperatureUnmanaged>
    {
        internal TemperaturePlugin() : base("Temperature", isKeyed: false, CreateDynamicType(isPublic: false))
        {
        }

        public static DynamicType CreateDynamicType(bool isPublic = true)
        {
            var dtf = ServiceEnvironment.Instance.Internal.GetTypeFactory(isPublic);
            var tsf = ServiceEnvironment.Instance.Internal.TypeSupportFactory;

            // Temperature struct
            var TemperatureStructMembers = new StructMember[]
            {
                new StructMember("sensor_id", dtf.CreateString(((int) 256)), id: 0),
                new StructMember("degrees", dtf.GetPrimitiveType<int>(), id: 1)
            };

            return tsf.CreateTypeWithAccessInfo<TemperatureUnmanaged>(
                dtf.BuildStruct()
                .WithExtensibility(ExtensibilityKind.Extensible)
                .WithName("Temperature")
                .AddMembers(TemperatureStructMembers));
        }
    }
}
public class TemperatureSupport : Rti.Dds.Topics.TypeSupport<Temperature>
{
    public TemperatureSupport() : base(
        new Implementation.TemperaturePlugin(),
        new Lazy<DynamicType>(() =>Implementation.TemperaturePlugin.CreateDynamicType(isPublic: true)))
    {
    }

    public static TemperatureSupport Instance { get; } =
    ServiceEnvironment.Instance.Internal.TypeSupportFactory.CreateTypeSupport<TemperatureSupport, Temperature>();
}

namespace Implementation
{
    internal class LotStatusKindPlugin : Rti.Dds.NativeInterface.TypePlugin.EnumTypePlugin
    {
        public LotStatusKindPlugin() : base(CreateDynamicType(isPublic: false))
        {
        }

        internal static DynamicType CreateDynamicType(bool isPublic = true)
        {
            var dtf = ServiceEnvironment.Instance.Internal.GetTypeFactory(isPublic);
            return dtf.BuildEnum()
            .WithName("LotStatusKind")
            .AddMember(new EnumMember("WAITING", 0))
            .AddMember(new EnumMember("PROCESSING", 1))
            .AddMember(new EnumMember("COMPLETED", 2))
            .WithExtensibility(ExtensibilityKind.Extensible)
            .Create();
        }
    }
}

public class LotStatusKindSupport : Rti.Dds.Topics.TypeSupport<LotStatusKind>
{
    public LotStatusKindSupport() : base(
        new Implementation.LotStatusKindPlugin(),
        new Lazy<DynamicType>(() =>Implementation.LotStatusKindPlugin.CreateDynamicType(isPublic: true)))
    {
    }

    public static LotStatusKindSupport Instance { get; } =
    ServiceEnvironment.Instance.Internal.TypeSupportFactory.CreateTypeSupport<LotStatusKindSupport, LotStatusKind>();
}

namespace Implementation
{

    public struct ChocolateLotStateUnmanaged : Rti.Dds.NativeInterface.TypePlugin.INativeTopicType<ChocolateLotState>
    {

        private uint lot_id;
        private LotStatusKind lot_status;

        public void Destroy(bool optionalsOnly)
        {
        }

        public void FromNative(ChocolateLotState sample, bool keysOnly = false)
        {

            sample.lot_id = lot_id;
            sample.lot_status = lot_status;
        }

        public void Initialize(bool allocatePointers = true, bool allocateMemory = true)
        {
            lot_id = (uint) 0u;
            lot_status = (LotStatusKind) 0;
        }

        public void ToNative(ChocolateLotState sample, bool keysOnly = false)
        {
            lot_id = sample.lot_id;
            lot_status = sample.lot_status;
        }
    }

    internal class ChocolateLotStatePlugin : Rti.Dds.NativeInterface.TypePlugin.InterpretedTypePlugin<ChocolateLotState, ChocolateLotStateUnmanaged>
    {
        internal ChocolateLotStatePlugin() : base("ChocolateLotState", isKeyed: false, CreateDynamicType(isPublic: false))
        {
        }

        public static DynamicType CreateDynamicType(bool isPublic = true)
        {
            var dtf = ServiceEnvironment.Instance.Internal.GetTypeFactory(isPublic);
            var tsf = ServiceEnvironment.Instance.Internal.TypeSupportFactory;

            // ChocolateLotState struct
            var ChocolateLotStateStructMembers = new StructMember[]
            {
                new StructMember("lot_id", dtf.GetPrimitiveType<uint>(), id: 0),
                new StructMember("lot_status", LotStatusKindSupport.Instance.GetDynamicTypeInternal(isPublic), id: 1)
            };

            return tsf.CreateTypeWithAccessInfo<ChocolateLotStateUnmanaged>(
                dtf.BuildStruct()
                .WithExtensibility(ExtensibilityKind.Extensible)
                .WithName("ChocolateLotState")
                .AddMembers(ChocolateLotStateStructMembers));
        }
    }
}
public class ChocolateLotStateSupport : Rti.Dds.Topics.TypeSupport<ChocolateLotState>
{
    public ChocolateLotStateSupport() : base(
        new Implementation.ChocolateLotStatePlugin(),
        new Lazy<DynamicType>(() =>Implementation.ChocolateLotStatePlugin.CreateDynamicType(isPublic: true)))
    {
    }

    public static ChocolateLotStateSupport Instance { get; } =
    ServiceEnvironment.Instance.Internal.TypeSupportFactory.CreateTypeSupport<ChocolateLotStateSupport, ChocolateLotState>();
}

