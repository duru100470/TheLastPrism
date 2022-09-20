using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Dirt,
    Stone,
    Sand,
    CoalOre,
    CopperOre,
    IronOre,
    GoldOre,
    LuxShardOre,
    Debug
}

public class Coordinate
{
    private readonly int x;
    private readonly int y;
    public int X => x;
    public int Y => y;

    public Coordinate(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override int GetHashCode()
    {
        return x ^ y;
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;

        if (GetType() != obj.GetType())
            return false;

        Coordinate point = (Coordinate)obj;

        if (x != point.x)
            return false;

        return y == point.y;
    }

    public static bool operator ==(Coordinate c1, Coordinate c2)
    {
        return Object.Equals(c1, c2);
    }

    public static bool operator !=(Coordinate c1, Coordinate c2)
    {
        return !Object.Equals(c1, c2);
    }
}