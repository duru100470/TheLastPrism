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
    [SerializeField]
    private bool[,] cavernMatrix;

    [Header("World Border")]
    [SerializeField]
    private GameObject[] borderObject;

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

        // Generate CA Matrix
        cavernMatrix = new bool[worldXSize, worldYSize];
        InitializeMatrix(0.50f);
        for (int i = 0; i < 4; i++)
            cavernMatrix = DoSimulationStep();

        // Generate map
        caveNoiseTexture = GenerateNoiseTexture(seed, caveFreq);
        tileAtlas.oreDatas[0].spread = GenerateNoiseTexture(seed + 1000, tileAtlas.oreDatas[0].freq);
        tileAtlas.oreDatas[1].spread = GenerateNoiseTexture(seed + 2000, tileAtlas.oreDatas[1].freq);
        tileAtlas.oreDatas[2].spread = GenerateNoiseTexture(seed + 3000, tileAtlas.oreDatas[2].freq);
        tileAtlas.oreDatas[3].spread = GenerateNoiseTexture(seed + 4000, tileAtlas.oreDatas[3].freq);
        tileAtlas.oreDatas[4].spread = GenerateNoiseTexture(seed + 5000, tileAtlas.oreDatas[4].freq);
        GenerateTerrain();

        GenerateWorldBorder();

        // Update Entire Ruletiles
        for (int x = 0; x < worldXSize; x++)
            for (int y = 0; y < worldYSize; y++)
            {
                (TileManager.Instance.TileArray[x, y] as RuleTile)?.UpdateRuleTile();
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
                float dirtHeight =
                    Mathf.PerlinNoise((x + seed + 5000) * terrainFreq, seed * terrainFreq) * 10 + 15;

                float sandHeight =
                    (Mathf.PerlinNoise((x + seed + 6000) * terrainFreq, seed * terrainFreq) * 10 + 15) *
                    Mathf.Max((Mathf.Abs(x - worldXSize * 0.5f) * 2 / worldXSize - 0.5f), 0);

                float darkStoneHeight =
                    Mathf.PerlinNoise((x + seed + 7000) * terrainFreq, seed * terrainFreq) * 10 + 30;

                // For generating the floor of ocean
                if (y == Mathf.FloorToInt(height - sandHeight) && (x < worldXSize * .5f - planeWidth || x > worldXSize * .5f + planeWidth))
                    TileManager.Instance.PlaceTile(new Coordinate(x, y), TILE_TYPE.Dirt);

                if (y > height - sandHeight)
                {
                    TileManager.Instance.PlaceTile(new Coordinate(x, y), TILE_TYPE.Sand);
                    continue;
                }

                if (caveNoiseTexture.GetPixel(x, y).r > 0.2f)
                {

                    if (!cavernMatrix[x, y]) continue;

                    if (y > height - dirtHeight)
                        TileManager.Instance.PlaceTile(new Coordinate(x, y), TILE_TYPE.Dirt);
                    else
                    {
                        if (y > darkStoneHeight)
                        {
                            if (tileAtlas.oreDatas[4].spread.GetPixel(x, y).r > tileAtlas.oreDatas[4].size && y <= tileAtlas.oreDatas[4].maxSpawnHeight)
                                TileManager.Instance.PlaceTile(new Coordinate(x, y), TILE_TYPE.StoneLuxShardOre);
                            else if (tileAtlas.oreDatas[3].spread.GetPixel(x, y).r > tileAtlas.oreDatas[3].size && y <= tileAtlas.oreDatas[3].maxSpawnHeight)
                                TileManager.Instance.PlaceTile(new Coordinate(x, y), TILE_TYPE.StoneGoldOre);
                            else if (tileAtlas.oreDatas[2].spread.GetPixel(x, y).r > tileAtlas.oreDatas[2].size && y <= tileAtlas.oreDatas[2].maxSpawnHeight)
                                TileManager.Instance.PlaceTile(new Coordinate(x, y), TILE_TYPE.StoneIronOre);
                            else if (tileAtlas.oreDatas[1].spread.GetPixel(x, y).r > tileAtlas.oreDatas[1].size && y <= tileAtlas.oreDatas[1].maxSpawnHeight)
                                TileManager.Instance.PlaceTile(new Coordinate(x, y), TILE_TYPE.StoneCopperOre);
                            else if (tileAtlas.oreDatas[0].spread.GetPixel(x, y).r > tileAtlas.oreDatas[0].size && y <= tileAtlas.oreDatas[0].maxSpawnHeight)
                                TileManager.Instance.PlaceTile(new Coordinate(x, y), TILE_TYPE.StoneCoalOre);
                            else
                                TileManager.Instance.PlaceTile(new Coordinate(x, y), TILE_TYPE.Stone);
                        }
                        else
                        {
                            if (tileAtlas.oreDatas[4].spread.GetPixel(x, y).r > tileAtlas.oreDatas[4].size && y <= tileAtlas.oreDatas[4].maxSpawnHeight)
                                TileManager.Instance.PlaceTile(new Coordinate(x, y), TILE_TYPE.DarkStoneLuxShardOre);
                            else if (tileAtlas.oreDatas[3].spread.GetPixel(x, y).r > tileAtlas.oreDatas[3].size && y <= tileAtlas.oreDatas[3].maxSpawnHeight)
                                TileManager.Instance.PlaceTile(new Coordinate(x, y), TILE_TYPE.DarkStoneGoldOre);
                            else if (tileAtlas.oreDatas[2].spread.GetPixel(x, y).r > tileAtlas.oreDatas[2].size && y <= tileAtlas.oreDatas[2].maxSpawnHeight)
                                TileManager.Instance.PlaceTile(new Coordinate(x, y), TILE_TYPE.DarkStoneIronOre);
                            else if (tileAtlas.oreDatas[1].spread.GetPixel(x, y).r > tileAtlas.oreDatas[1].size && y <= tileAtlas.oreDatas[1].maxSpawnHeight)
                                TileManager.Instance.PlaceTile(new Coordinate(x, y), TILE_TYPE.DarkStoneCopperOre);
                            else if (tileAtlas.oreDatas[0].spread.GetPixel(x, y).r > tileAtlas.oreDatas[0].size && y <= tileAtlas.oreDatas[0].maxSpawnHeight)
                                TileManager.Instance.PlaceTile(new Coordinate(x, y), TILE_TYPE.DarkStoneCoalOre);
                            else
                                TileManager.Instance.PlaceTile(new Coordinate(x, y), TILE_TYPE.DarkStone);
                        }
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

    private void GenerateWorldBorder()
    {
        borderObject[0].transform.localScale = new Vector3(worldXSize, 1f, 1f);
        borderObject[2].transform.localScale = new Vector3(worldXSize, 1f, 1f);
        borderObject[1].transform.localScale = new Vector3(1f, worldYSize, 1f);
        borderObject[3].transform.localScale = new Vector3(1f, worldYSize, 1f);

        borderObject[0].transform.position = new Vector3(worldXSize * .5f, -0.5f, 0f);
        borderObject[1].transform.position = new Vector3(-0.5f, worldYSize * .5f, 0f);
        borderObject[2].transform.position = new Vector3(worldXSize * .5f, worldYSize + .5f, 0f);
        borderObject[3].transform.position = new Vector3(worldXSize + .5f, worldYSize * .5f, 0f);
    }

    private void InitializeMatrix(float _rate)
    {
        for (int x = 0; x < worldXSize; x++)
            for (int y = 0; y < worldYSize; y++)
            {
                if (Random.Range(0, 100) < _rate * 100 + y * .5f - 25)
                    cavernMatrix[x, y] = true;
            }
    }

    private bool[,] DoSimulationStep()
    {
        bool[,] newMatrix = new bool[worldXSize, worldYSize];

        for (int x = 0; x < worldXSize; x++)
            for (int y = 0; y < worldYSize; y++)
            {
                int cnt = CountAliveNeighbours(x, y);

                if (cavernMatrix[x, y])
                {
                    if (cnt < 3)
                        newMatrix[x, y] = false;
                    else
                        newMatrix[x, y] = true;
                }
                else
                {
                    if (cnt > 4)
                        newMatrix[x, y] = true;
                    else
                        newMatrix[x, y] = false;
                }
            }

        return newMatrix;
    }

    private int CountAliveNeighbours(int x, int y)
    {
        int count = 0;

        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                int neighbour_x = x + i;
                int neighbour_y = y + j;
                //If we're looking at the middle point
                if (i == 0 && j == 0)
                {
                    //Do nothing, we don't want to add ourselves in!
                }
                //In case the index we're looking at it off the edge of the map
                else if (neighbour_x < 0 || neighbour_y < 0 || neighbour_x >= worldXSize || neighbour_y >= worldYSize)
                {
                    count = count + 1;
                }
                //Otherwise, a normal check of the neighbour
                else if (cavernMatrix[neighbour_x, neighbour_y])
                {
                    count = count + 1;
                }
            }
        }

        return count;
    }
}
