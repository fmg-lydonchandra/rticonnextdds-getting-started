
/*
WARNING: THIS FILE IS AUTO-GENERATED. DO NOT MODIFY.

This file was generated from chocolate_factory.idl
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

public class Temperature :  IEquatable<Temperature>
{
    [Bound(256)]
    public string sensor_id { get; set; } = string.Empty;

    public int degrees { get; set; }

    public Temperature()
    {
    }

    public Temperature(string  sensor_id, int  degrees)
    {
        this.sensor_id = sensor_id;
        this.degrees = degrees;
    }

    public Temperature(Temperature other)
    {
        if (other == null)
        {
            return;
        }

        this.sensor_id = other.sensor_id;
        this.degrees = other.degrees;

    }

    public override int GetHashCode()
    {
        var hash = new HashCode();

        hash.Add(sensor_id);
        hash.Add(degrees);

        return hash.ToHashCode();
    }

    public bool Equals(Temperature other)
    {
        if (other == null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return this.sensor_id.Equals(other.sensor_id) &&
        this.degrees.Equals(other.degrees);
    }

    public override bool Equals(object obj) => this.Equals(obj as Temperature);

    public override string ToString() => TemperatureSupport.Instance.ToString(this);
}

public enum LotStatusKind
{
    WAITING,
    PROCESSING,
    COMPLETED
}

public class ChocolateLotState :  IEquatable<ChocolateLotState>
{
    public uint lot_id { get; set; }

    public LotStatusKind lot_status { get; set; }

    public ChocolateLotState()
    {
        lot_status = (LotStatusKind) 0;
    }

    public ChocolateLotState(uint  lot_id, LotStatusKind  lot_status)
    {
        this.lot_id = lot_id;
        this.lot_status = lot_status;
    }

    public ChocolateLotState(ChocolateLotState other)
    {
        if (other == null)
        {
            return;
        }

        this.lot_id = other.lot_id;
        this.lot_status = other.lot_status;

    }

    public override int GetHashCode()
    {
        var hash = new HashCode();

        hash.Add(lot_id);
        hash.Add(lot_status);

        return hash.ToHashCode();
    }

    public bool Equals(ChocolateLotState other)
    {
        if (other == null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return this.lot_id.Equals(other.lot_id) &&
        this.lot_status.Equals(other.lot_status);
    }

    public override bool Equals(object obj) => this.Equals(obj as ChocolateLotState);

    public override string ToString() => ChocolateLotStateSupport.Instance.ToString(this);
}

