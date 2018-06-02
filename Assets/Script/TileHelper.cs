using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TileHelper {

    static Tile[,] tiles;

	public static Vector3 TilePosition(Tile t)
    {
        return new Vector3(t.x, t.y , 0);
    }

    public static Tile TileUnderPos(Vector2 pos)
    {
        return tiles[Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y)];
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

    public static Vector2 RoundVector(Vector2 a)
    {
        return new Vector2(Mathf.Round(a.x) , Mathf.Round(a.y));
    }
    
    public static Vector2 PredictEnemyPos(Enemy enemy, float t)
    {
        Vector2 pos = enemy.transform.position;
        Vector2 pos2 = RoundVector(pos);
        float remainder_pos = Vector2.Distance(pos,pos2) ; //start point to start tile
        float full = enemy.speed * t; //distance from start point to end point
        int index = Mathf.RoundToInt(enemy.speed * t);
        int endIndex = index + enemy.currIndex; //index of the end tile
        float remainder = full - index; //end tile to end point
        Vector2 endDir =
        Mathf.Sign(remainder_pos + remainder) == -1 && index + enemy.currIndex < enemy.path.Count ? 
        enemy.path[index + enemy.currIndex].Position() - enemy.path[(index - 1) + enemy.currIndex].Position() 
        : 
        enemy.path[(index + 1) + enemy.currIndex].Position() - enemy.path[index + enemy.currIndex].Position();
        Tile  endTile = enemy.path[index + enemy.currIndex];
        Vector2 result = endTile.Position() + endDir * (remainder_pos + remainder);

        Debug.Log("After "+ t + " seconds the boat will be at " + result+ " Enemy pos: " + pos);

        return result;
    }

}
