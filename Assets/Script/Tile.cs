using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType {Path, Buildable, Unbuildable, Unassigned};

public class Tile {


    public TileType tileType;

    public GameObject tileGo;

    public Tile()
    {
        tileType = TileType.Buildable;
    }
}
