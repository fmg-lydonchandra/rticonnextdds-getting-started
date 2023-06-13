
/*
WARNING: THIS FILE IS AUTO-GENERATED. DO NOT MODIFY.

This file was generated from hello_world.idl
using RTI Code Generator (rtiddsgen) version 3.1.1.
The rtiddsgen tool is part of the RTI Connext DDS distribution.
For more information, type 'rtiddsgen -help' at a command shell
or consult the Code Generator User's Manual.
*/

using System;
using System.Reflection;
using System.Collections.Generic;
using Rti.Types;
using System.Linq;
using Omg.Types;

public class HelloWorld :  IEquatable<HelloWorld>
{
    [Bound(256)]
    public string msg { get; set; } = string.Empty;

    public HelloWorld()
    {
    }

    public HelloWorld(string  msg)
    {
        this.msg = msg;
    }

    public HelloWorld(HelloWorld other)
    {
        if (other == null)
        {
            return;
        }

        this.msg = other.msg;

    }

    public override int GetHashCode()
    {
        var hash = new HashCode();

        hash.Add(msg);

        return hash.ToHashCode();
    }

    public bool Equals(HelloWorld other)
    {
        if (other == null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return this.msg.Equals(other.msg);
    }

    public override bool Equals(object obj) => this.Equals(obj as HelloWorld);

    public override string ToString() => HelloWorldSupport.Instance.ToString(this);
}

