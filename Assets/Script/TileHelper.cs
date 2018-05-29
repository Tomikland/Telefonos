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
    
    public static Vector2 PredictEnemyPos(Enemy enemy, float t)
    {
        Vector2 pos = enemy.transform.position;
        Vector2 startdelta = pos - enemy.currTile.Position();
        
        int endTileIndex = enemy.currIndex + Mathf.FloorToInt(t * enemy.speed);
        Tile endTile = enemy.path[endTileIndex];


        Vector2 startDir = enemy.nextTile.Position() - enemy.currTile.Position();

        Vector2 endDir =  enemy.path[endTileIndex + 1].Position() - enemy.path[endTileIndex].Position();

        float remainder = t / enemy.speed - Mathf.Floor(t / enemy.speed);

        Vector2 endDelta = endDir * remainder;

        //Debug.Log("enemy speed: "+enemy.speed+"Endtile: "+ endTile +"EndDir: " +endDir+ " Remainder: " + remainder + " EndDelta: "+ endDelta);

        return endTile.Position();
            
    }
}
