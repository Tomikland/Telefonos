using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour {

    public GameObject tilePrefab;
    public Tile[,] tiles;

    public List<Color> tileTypeToColor = new List<Color>();

    public List<Texture2D> mapList = new List<Texture2D>();
    public int mapIndex;
    public TileType[,] tileTypeMap;

	// Use this for initialization
	void Start () {

        LoadMaps();

        ReadMap(0);

        Generate(mapList[mapIndex].width, mapList[mapIndex].height);

	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void Generate(int sizeX, int sizeY)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i));
        }
        


        tiles = new Tile[sizeX, sizeY];

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                Vector3 position = new Vector3(x,y,0);

                GameObject go = Instantiate(tilePrefab,position,Quaternion.identity,gameObject.transform);

                go.name = "Tile " + x + "_" + y;

                Tile tileScript = new Tile();
                tileScript.tileGo = go;

                tiles[x, y] = tileScript;

                tileScript.tileType = tileTypeMap[x, y];

                //Visuals

                SpriteRenderer spr = go.GetComponentInChildren<SpriteRenderer>();

                spr.color = tileTypeToColor[(int)tileScript.tileType];

            }
        }


        Vector3 camPos = new Vector3(sizeX / 2f, sizeY / 2f, -10);
        Camera.main.transform.position = camPos;
    }

    void ReadMap(int index)
    {
        Texture2D texture = mapList[index];
        tileTypeMap = new TileType[texture.width, texture.height];

        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {

                Color color = texture.GetPixel(x, y);

                TileType type = TileType.Unassigned;
                if(color == Color.green)
                {
                    type = TileType.Buildable;
                }
                if (color == Color.red)
                {
                    type = TileType.Unbuildable;
                }
                if (color == new Color(1f, 1f, 0f))
                {
                    type = TileType.Path;
                }

                tileTypeMap[x, y] = type;
            }
        }
    }

    void LoadMaps()
    {
        int mapNum = 1;

        Texture2D tempMap;

        while (true)
        {
            tempMap = Resources.Load("maps/map" + mapNum) as Texture2D;

            if(tempMap != null)
            {
                mapList.Add(tempMap);
            }
            else
            {
                Debug.Log("Load stopped at" + mapNum);
                break;
            }

            mapNum++;
        }

    }
}
