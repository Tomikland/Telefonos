using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType {Path, Buildable, Unbuildable, Unassigned};

public class Tile {

    public int x;
    public int y;

    public TileType tileType;

    public GameObject tileGo;



    public Tile(int _x, int _y)
    {
        x = _x;
        y = _y;
        tileType = TileType.Unassigned;
    }
}
