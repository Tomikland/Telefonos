using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TileHelper {

	public static Vector3 TilePosition(Tile t)
    {
        return new Vector3(t.x, t.y , 0);
    }
}
