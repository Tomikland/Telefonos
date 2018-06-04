﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placer : MonoBehaviour {

    public GameObject selectedPrefab;
    GameMaster gm;

	// Use this for initialization
	void Start () {
        gm = FindObjectOfType<GameMaster>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlaceSelectedOnTile(Tile t)
    {
        //Debug.Log("Placing");

        Vector3 pos = t.tileGo.transform.position;

        GameObject go = Instantiate(selectedPrefab,pos,Quaternion.identity,transform);

        go.name = "Cannon" + Random.Range(1, 100);

        Placeable pl = selectedPrefab.GetComponent<Placeable>();

        gm.SetMoney( gm.money - pl.price);

        t.building = pl;
    }

    public bool IsPlaceable(Tile t)
    {
        if (t.building == null && 
            t.tileType == TileType.Buildable && gm.money >= selectedPrefab.GetComponent<Placeable>().price 
            && gm.gameOn == true)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
