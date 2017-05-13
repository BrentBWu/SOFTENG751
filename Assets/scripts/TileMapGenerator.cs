using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileMapGenerator : MonoBehaviour
{
    public Texture2D levelTexture;
    public Color sandColor;
    public Color waterColor;
    public Color spawnerColor;
    public Color wallColor;

    public Texture2D sandTexture;
    public Texture2D waterTexture;
    public Texture2D wallTexture;

    public GameObject tileMapBase;
    public GameObject sandPrefab;
    public GameObject waterPrefab;
    public GameObject spawnerPrefab;
    public GameObject wallPrefab;

    [System.Serializable]
    public class MapPack
    {
        // Unused variable elsewhere, just allows for organizing in the editor (hence private).
        [SerializeField]
        private string name;
        public GameObject[] sandPrefab;
        public GameObject[] wallPrefab;
        public GameObject[] spawnerPrefab;
    }

    public MapPack[] mapPacks;
    [HideInInspector]
    public int mapPackIndex;

    // Array of the spawn tiles is constructed during map creation to make it easier to find them.
    public List<Transform> spawnLocations { get; private set; }

    int height;
    int width;

    int[][] map;

    List<GameObject> gameObjects;
    GameObject tileMap;
    
    enum TileType
    {
        NONE = 0,
        SAND = 1,
        WATER = 2,
        WALL = 3,
        SPAWNER = 4
    }


    // Use this for initialization
    void Start()
    {

        if (gameObjects != null)
        {
            gameObjects = null;
        }

		

        GenerateMap();
    }

    public void GenerateMap()
    {
		spawnLocations = new List<Transform>();

		if (gameObjects == null)
        {
            gameObjects = new List<GameObject>();
        }

        tileMap = Instantiate(tileMapBase);
        tileMap.transform.position = new Vector3(0, 0, 0);
        tileMap.transform.parent = gameObject.transform;


        height = levelTexture.height;
        width = levelTexture.width;

        map = new int[width][];

        for (int x = 0; x < width; x++)
        {
            map[x] = new int[height];
        }

        //iterates each pixel of the image and loads it in
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Convert the image pixel to the type specified.
                map[x][y] = (int)convertColorToTileType(levelTexture.GetPixel(x, y));
            }
        }

        Debug.Log("Loaded level image into memory");
        //which map pack is being used (volcano, wasteland, etc)
        MapPack mapPack = mapPacks[mapPackIndex];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject tile = null;
                TileType tileType = (TileType)map[x][y];
                //instantiate a tile of the type specified by the pixel colour
                if (tileType == TileType.SAND)
                {
                    tile = Instantiate(mapPack.sandPrefab[Random.Range(0, mapPack.sandPrefab.Length)]);
                }
                else if (tileType == TileType.SPAWNER)
                {
                    tile = Instantiate(mapPack.spawnerPrefab[Random.Range(0, mapPack.spawnerPrefab.Length)]);
					spawnLocations.Add(tile.transform);
                }
                else if (tileType == TileType.WALL)
                {
                    tile = Instantiate(mapPack.wallPrefab[Random.Range(0, mapPack.wallPrefab.Length)]);
                }
                else if (tileType == TileType.WATER)
                {
                    tile = Instantiate(waterPrefab);
                }
                else
                {
                    continue;
                }

                gameObjects.Add(tile);

                tile.transform.parent = tileMap.transform;
                //recenter the grid
                tile.transform.position = new Vector3(x - width / 2, y - height / 2, 0);
                tile.transform.localScale = new Vector3(3.125f, 3.125f, 1);
            }
        }

        Camera mainCam = Camera.main;
        mainCam.orthographicSize = width / 2.5f;
        //mainCam.transform.position = new Vector3(width / 2, height / 2, -10);

        Debug.Log("Finished generating map");

    }

    TileType convertColorToTileType(Color color)
    {
        TileType tileType = TileType.NONE;

        if (color.Equals(sandColor))
        {
            tileType = TileType.SAND;
        }
        else if (color.Equals(waterColor))
        {
            tileType = TileType.WATER;
        }
        else if (color.Equals(wallColor))
        {
            tileType = TileType.WALL;
        }
        else if (color.Equals(spawnerColor))
        {
            tileType = TileType.SPAWNER;
        }

        return tileType;
    }


    // Update is called once per frame
    void Update()
    {

    }

    public List<GameObject> GameObjects { get { return gameObjects; } private set { this.gameObjects = value; } }
    public GameObject TileMap { get { return tileMap; } private set { this.tileMap = value; } }
	public int Width { get { return width; } private set { this.width = value; } }
	public int Height { get { return height; } private set { this.height = value; } }
}
