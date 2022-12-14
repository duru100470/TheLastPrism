using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TILE_ID
{
    Dirt,
    Stone,
    DarkStone,
    Sand,
    StoneCoalOre,
    StoneCopperOre,
    StoneIronOre,
    StoneGoldOre,
    StoneLuxShardOre,
    DarkStoneCoalOre,
    DarkStoneCopperOre,
    DarkStoneIronOre,
    DarkStoneGoldOre,
    DarkStoneLuxShardOre,
    Debug
}

public enum ITEM_ID
{
    Dirt,
    Stone,
    DarkStone,
    Sand,
    CoalOre,
    CopperOre,
    IronOre,
    GoldOre,
    LuxShardOre,
    StonePickaxe,
    StoneHammer,
    Workbench,
    Debug
}

public enum STRUCTURE_ID
{
    Workbench
}

[System.Serializable]
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

    public static Coordinate operator +(Coordinate c1, Coordinate c2)
    {
        return new Coordinate(c1.X + c2.X, c1.Y + c2.Y);
    }

    public static Coordinate operator -(Coordinate c1, Coordinate c2)
    {
        return new Coordinate(c1.X - c2.X, c1.Y - c2.Y);
    }

    public static int Distance(Coordinate c1, Coordinate c2)
    {
        return Mathf.Abs(c1.X - c2.X) + Mathf.Abs(c1.Y - c2.Y);
    }

    public static Coordinate WorldPointToCoordinate(Vector3 point)
    {
        return new Coordinate(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y));
    }

    public static Vector2 CoordinatetoWorldPoint(Coordinate coor)
    {
        return new Vector2(coor.X + 0.5f, coor.Y + 0.5f);
    }
}