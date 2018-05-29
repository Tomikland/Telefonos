using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PathFinder  {

    static public List<Tile> FindPath(Tile[,] tiles,Tile src, Tile dest)
    {
        List<Tile> path = new List<Tile>();

        Tile currTile = src;
        path.Add(src);


        int count = 0;
        do
        {
            count++;

            foreach (Tile n in TileHelper.Neighbours4(currTile))
            {
                if(n.tileType == TileType.Path && path.Contains(n) == false)
                {
                    path.Add(n);

                    //Debug.Log("Added tile"+n.x +"_"+n.y);

                    currTile = n;
                    break;
                }
            }

        }
        while (currTile != dest && count < 100);
        Debug.Log("Path found in " + count + " iterations");

            return path;
    }


}
