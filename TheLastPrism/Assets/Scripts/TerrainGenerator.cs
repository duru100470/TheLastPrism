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
    [SerializeField]
    private Sprite tile;

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
    }

    public void GenerateTerrain()
    {
        for (int x = 0; x < worldXSize; x++)
        {
            float height = Mathf.PerlinNoise((x + seed) * terrainFreq, seed * terrainFreq) * heightMultiplier + heightAddition;

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
                    GameObject newTile = new GameObject(name = "tile");
                    newTile.transform.parent = this.transform;
                    newTile.AddComponent<SpriteRenderer>();
                    newTile.AddComponent<Tile>();
                    newTile.GetComponent<SpriteRenderer>().sprite = tile;
                    newTile.transform.position = new Vector2(x + 0.5f, y + 0.5f);
                    TileManager.Instance.TileArray[x, y] = newTile.GetComponent<Tile>();
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
