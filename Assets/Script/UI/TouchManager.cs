using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchManager : MonoBehaviour {

    Placer pl;
    GameMaster gm;
    EventSystem eventSys;

    // Use this for initialization
    void Start () {
        pl = GameObject.FindObjectOfType<Placer>();
        gm = GameObject.FindObjectOfType<GameMaster>();
        eventSys = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }
	
	// Update is called once per frame
	void Update () {


        if(gm.gameOn == false)
        {
            return;
        }

        if (eventSys.IsPointerOverGameObject())
        {
            return; // exit out of OnMouseDown() because its over the uGUI
        }

        if (Input.GetMouseButtonDown(0))
        {
            Tile tile = TileHelper.TileUnderPos(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            if (pl.IsPlaceable(tile))
            {
                pl.PLaceCannonoOnTile(tile);

            }
            else if (pl.IsPath(tile))
            {
                pl.PlaceSpellOnTile(tile);
            }
        }

        foreach (Touch t in Input.touches)
        {

            if (t.phase == TouchPhase.Began)
            {
                Tile tile = TileHelper.TileUnderPos(Camera.main.ScreenToWorldPoint(t.position));

                if (pl.IsPlaceable(tile))
                {
                    pl.PLaceCannonoOnTile(tile);

                }
                else if (pl.IsPath(tile))
                {
                    pl.PlaceSpellOnTile(tile);
                }
            }
        }

	}
}
