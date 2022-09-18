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
    [SerializeField]
    private float caveFreq;
    [SerializeField]
    private float terrainFreq;
    [SerializeField]
    private float seed;
    [SerializeField]
    private Texture2D noiseTexture;

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
        GenerateNoiseTexture();
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
                if (noiseTexture.GetPixel(x, y).r > 0.2f)
                {
                    float dirtHeight = 
                        Mathf.PerlinNoise((x + seed + 5000) * terrainFreq, seed * terrainFreq) * 10 + 20;
                    
                    if (y > height - dirtHeight)
                        TileManager.Instance.PlaceTile(new Coordinate(x, y), TileType.Dirt);
                    else
                        TileManager.Instance.PlaceTile(new Coordinate(x, y), TileType.Stone);
                }
            }
        }
    }

    private void GenerateNoiseTexture()
    {
        noiseTexture = new Texture2D(worldXSize, worldYSize);

        for (int x = 0; x < noiseTexture.width; x++)
        {
            for (int y = 0; y < noiseTexture.height; y++)
            {
                float v = Mathf.PerlinNoise((x + seed) * caveFreq, (y + seed) * caveFreq);
                noiseTexture.SetPixel(x, y, new Color(v, v, v));
            }
        }

        noiseTexture.Apply();
    }
}
