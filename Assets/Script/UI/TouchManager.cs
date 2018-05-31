using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour {

    Placer pl;

    // Use this for initialization
    void Start () {
        pl = GameObject.FindObjectOfType<Placer>();
	}
	
	// Update is called once per frame
	void Update () {


        if(Input.GetMouseButtonDown(0)){


            Tile tile = TileHelper.TileUnderPos(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            if (pl.IsPlaceable(tile))
            {
                pl.PlaceSelectedOnTile(tile);

            }
        }

        foreach (Touch t in Input.touches)
        {

            if (t.phase == TouchPhase.Began)
            {
                Tile tile = TileHelper.TileUnderPos(Camera.main.ScreenToWorldPoint(t.position));

                if (pl.IsPlaceable(tile))
                {
                    pl.PlaceSelectedOnTile(tile);

                }
            }
        }

	}
}
