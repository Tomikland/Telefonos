using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TileHelper {

    static Tile[,] tiles;

	public static Vector3 TilePosition(Tile t)
    {
        return new Vector3(t.x, t.y , 0);
    }

    public static List<Tile> Neighbours4(Tile t)
    {
        List<Tile> neighbours = new List<Tile>();

        if(t.x != 0)
        {
            neighbours.Add(tiles[t.x - 1, t.y]);
        }
        if (t.x != tiles.GetLength(0))
        {
            neighbours.Add(tiles[t.x + 1, t.y]);
        }
        if (t.y != 0)
        {
            neighbours.Add(tiles[t.x , t.y-1]);
        }
        if (t.y != tiles.GetLength(1))
        {
            neighbours.Add(tiles[t.x , t.y + 1]);
        }

        return neighbours;
    }

    public static void SetTileArray (Tile[,] t)
    {
        tiles = t;
    }
}
