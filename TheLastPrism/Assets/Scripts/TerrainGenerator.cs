using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [Header("World Setting")]
    [SerializeField]
    private int worldXSize;
    [SerializeField]
    private int worldYSize;
    [SerializeField]
    private int planeWidth;
    [SerializeField]
    private int seaWidth;
    [SerializeField]
    private int leftSeaDepth;
    [SerializeField]
    private int rightSeaDepth;
    [SerializeField]
    private int heightMultiplier;
    [SerializeField]
    private int heightAddition;

    [Header("Ores Setting")]
    [SerializeField]
    private TileAtlas tileAtlas;

    [Header("Noise Setting")]
    [SerializeField]
    private float caveFreq;
    [SerializeField]
    private float terrainFreq;
    [SerializeField]
    private float seed;
    [SerializeField]
    private Texture2D caveNoiseTexture;

    private void Start()
    {
        // Randomize map seed
        seed = Random.Range(-10000, 10000);
        leftSeaDepth += (int)seed % 10;
        rightSeaDepth += ((int)seed + 5) % 10;

        // Initialize TileManager 
        TileManager.Instance.TileArray = new Tile[worldXSize, worldYSize];
        TileManager.Instance.worldXSize = worldXSize;
        TileManager.Instance.worldYSize = worldYSize;

        // Generate map
        caveNoiseTexture = GenerateNoiseTexture(seed, caveFreq);
        tileAtlas.oreDatas[0].spread = GenerateNoiseTexture(seed + 1000, tileAtlas.oreDatas[0].freq);
        tileAtlas.oreDatas[1].spread = GenerateNoiseTexture(seed + 2000, tileAtlas.oreDatas[1].freq);
        tileAtlas.oreDatas[2].spread = GenerateNoiseTexture(seed + 3000, tileAtlas.oreDatas[2].freq);
        tileAtlas.oreDatas[3].spread = GenerateNoiseTexture(seed + 4000, tileAtlas.oreDatas[3].freq);
        tileAtlas.oreDatas[4].spread = GenerateNoiseTexture(seed + 5000, tileAtlas.oreDatas[4].freq);
        GenerateTerrain();

        // Update Entire Ruletiles
        for (int x = 0; x < worldXSize; x++)
            for (int y = 0; y < worldYSize; y++)
            {
                ( TileManager.Instance.TileArray[x, y] as RuleTile )?.UpdateRuleTile();
            }

        TileManager.Instance.IsGenerating = false;
    }

    public void GenerateTerrain()
    {
        for (int x = 0; x < worldXSize; x++)
        {
            float height = Mathf.PerlinNoise((x + seed) * terrainFreq, seed * terrainFreq) * heightMultiplier + heightAddition;

            // Generate sea terrain
            if (x >= 0 && x < seaWidth)
            {
                height -= leftSeaDepth;
            }
            else if (x < (worldXSize / 2) - planeWidth)
            {
                height -= Mathf.SmoothStep(leftSeaDepth, 0, 
                (float)(x - seaWidth) / (float)((worldXSize / 2 - planeWidth) - seaWidth));
            }
            else if (x >= (worldXSize / 2) + planeWidth && x < worldXSize - seaWidth)
            {
                height -= Mathf.SmoothStep(0, rightSeaDepth, 
                (float)(x - ((worldXSize / 2) + planeWidth)) / (float)(worldXSize - seaWidth - ((worldXSize / 2) + planeWidth)));
            }
            else if (x >= worldXSize - seaWidth)
            {
                height -= rightSeaDepth;
            }

            for (int y = 0; y < height; y++)
            {
                if (caveNoiseTexture.GetPixel(x, y).r > 0.2f)
                {
                    float dirtHeight = 
                        Mathf.PerlinNoise((x + seed + 5000) * terrainFreq, seed * terrainFreq) * 10 + 20;
                    
                    if (y > height - dirtHeight)
                        TileManager.Instance.PlaceTile(new Coordinate(x, y), TileType.Dirt);
                    else
                    {
                        if (tileAtlas.oreDatas[4].spread.GetPixel(x, y).r > tileAtlas.oreDatas[4].size)
                            TileManager.Instance.PlaceTile(new Coordinate(x, y), TileType.LuxShardOre);
                        else if (tileAtlas.oreDatas[3].spread.GetPixel(x, y).r > tileAtlas.oreDatas[3].size)
                            TileManager.Instance.PlaceTile(new Coordinate(x, y), TileType.GoldOre);
                        else if (tileAtlas.oreDatas[2].spread.GetPixel(x, y).r > tileAtlas.oreDatas[2].size)
                            TileManager.Instance.PlaceTile(new Coordinate(x, y), TileType.IronOre);
                        else if (tileAtlas.oreDatas[1].spread.GetPixel(x, y).r > tileAtlas.oreDatas[1].size)
                            TileManager.Instance.PlaceTile(new Coordinate(x, y), TileType.CopperOre);
                        else if (tileAtlas.oreDatas[0].spread.GetPixel(x, y).r > tileAtlas.oreDatas[0].size)
                            TileManager.Instance.PlaceTile(new Coordinate(x, y), TileType.CoalOre);
                        else
                            TileManager.Instance.PlaceTile(new Coordinate(x, y), TileType.Stone);
                    }
                }
            }
        }
    }

    private Texture2D GenerateNoiseTexture(float seed, float freq)
    {
        Texture2D noise = new Texture2D(worldXSize, worldYSize);

        for (int x = 0; x < noise.width; x++)
        {
            for (int y = 0; y < noise.height; y++)
            {
                float v = Mathf.PerlinNoise((x + seed) * freq, (y + seed) * freq);
                noise.SetPixel(x, y, new Color(v, v, v));
            }
        }

        noise.Apply();
        return noise;
    }
}
