using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new OreData", menuName = "Map Generation/Ore Data")]
public class OreData : ScriptableObject
{
    public TILE_TYPE oreType;
    public int maxSpawnHeight;
    public float freq;
    public float size;
    public Texture2D spread;
}