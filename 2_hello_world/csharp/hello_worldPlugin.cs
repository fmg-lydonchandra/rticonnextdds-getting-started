/*
WARNING: THIS FILE IS AUTO-GENERATED. DO NOT MODIFY.

This file was generated from hello_world.idl
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

    public struct HelloWorldUnmanaged : Rti.Dds.NativeInterface.TypePlugin.INativeTopicType<HelloWorld>
    {

        private NativeString msg;

        public void Destroy(bool optionalsOnly)
        {
            if (optionalsOnly)
            {
                return;
            }
            msg.Destroy();
        }

        public void FromNative(HelloWorld sample, bool keysOnly = false)
        {

            sample.msg = msg.FromNative();
        }

        public void Initialize(bool allocatePointers = true, bool allocateMemory = true)
        {
            msg.Initialize(size: ((int) 256), allocateMemory: allocateMemory);
        }

        public void ToNative(HelloWorld sample, bool keysOnly = false)
        {
            msg.ToNative(sample.msg, ((int) 256));
        }
    }

    internal class HelloWorldPlugin : Rti.Dds.NativeInterface.TypePlugin.InterpretedTypePlugin<HelloWorld, HelloWorldUnmanaged>
    {
        internal HelloWorldPlugin() : base("HelloWorld", isKeyed: false, CreateDynamicType(isPublic: false))
        {
        }

        public static DynamicType CreateDynamicType(bool isPublic = true)
        {
            var dtf = ServiceEnvironment.Instance.Internal.GetTypeFactory(isPublic);
            var tsf = ServiceEnvironment.Instance.Internal.TypeSupportFactory;

            // HelloWorld struct
            var HelloWorldStructMembers = new StructMember[]
            {
                new StructMember("msg", dtf.CreateString(((int) 256)), id: 0)
            };

            return tsf.CreateTypeWithAccessInfo<HelloWorldUnmanaged>(
                dtf.BuildStruct()
                .WithExtensibility(ExtensibilityKind.Extensible)
                .WithName("HelloWorld")
                .AddMembers(HelloWorldStructMembers));
        }
    }
}
public class HelloWorldSupport : Rti.Dds.Topics.TypeSupport<HelloWorld>
{
    public HelloWorldSupport() : base(
        new Implementation.HelloWorldPlugin(),
        new Lazy<DynamicType>(() =>Implementation.HelloWorldPlugin.CreateDynamicType(isPublic: true)))
    {
    }

    public static HelloWorldSupport Instance { get; } =
    ServiceEnvironment.Instance.Internal.TypeSupportFactory.CreateTypeSupport<HelloWorldSupport, HelloWorld>();
}

