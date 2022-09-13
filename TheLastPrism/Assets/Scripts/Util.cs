using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Dirt,
    Stone,
    Default   
}

public class Coordinate
{
    public int X { get; set; }
    public int Y { get; set; }
    public int distance { get; set; }
    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }
    public Coordinate(int x, int y, int _distance)
    {
        X = x;
        Y = y;
        distance = _distance;
    }
    public static Coordinate operator*(Coordinate p1, int scalar)
    {
        return new Coordinate(p1.X * scalar, p1.Y * scalar);
    }
    public static float EuclideanDist(Coordinate p1, Coordinate p2)
    {
        return Mathf.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
    }
    public static int Distance(Coordinate p1, Coordinate p2)
    {
        return Mathf.Abs(p1.X - p2.X) + Mathf.Abs(p1.Y - p2.Y);
    }
    public static Coordinate operator+(Coordinate p1, Coordinate p2)
    {
        return new Coordinate(p1.X + p2.X, p1.Y + p2.Y);
    }
    public static Coordinate operator -(Coordinate p1, Coordinate p2)
    {
        return new Coordinate(p1.X - p2.X, p1.Y - p2.Y);
    }
    public Coordinate GetUpTile()
    {
        Coordinate ret = new Coordinate(X, Y + 1);
        return ret;
    }
    public Coordinate GetDownTile()
    {
        Coordinate ret = new Coordinate(X, Y - 1);
        return ret;
    }
    public Coordinate GetLeftTile()
    {
        Coordinate ret = new Coordinate(X - 1, Y);
        return ret;
    }
    public Coordinate GetRightTile()
    {
        Coordinate ret = new Coordinate(X + 1, Y);
        return ret;
    }
}