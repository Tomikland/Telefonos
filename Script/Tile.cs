using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType {Path, Buildable, Unbuildable, Unassigned};

public class Tile {

    public int x;
    public int y;

    public TileType tileType;

    public GameObject tileGo;

    public Placeable building = null;


    public Tile(int _x, int _y)
    {
        x = _x;
        y = _y;
        tileType = TileType.Unassigned;
    }
    public Vector2 Position()
    {
        return new Vector2(x, y);
    }

    public override string ToString()
    {
        return "T" + x + "_" + y;
    }
}
