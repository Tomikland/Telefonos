using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placer : MonoBehaviour {

    public GameObject cannonPrefab;
    public GameObject spellPrefab;
    GameMaster gm;

	// Use this for initialization
	void Start () {
        gm = FindObjectOfType<GameMaster>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PLaceCannonoOnTile(Tile t)
    {
        //Debug.Log("Placing");

        Vector3 pos = t.tileGo.transform.position;

        GameObject go = Instantiate(cannonPrefab,pos,Quaternion.identity,transform);

        go.name = "Cannon" + Random.Range(1, 100);

        Placeable pl = go.GetComponent<Placeable>();

        gm.SetMoney( gm.money - pl.price);

        t.building = pl;
    }
    public void PlaceSpellOnTile(Tile t)
    {

        Vector3 pos = t.tileGo.transform.position;

        GameObject go = Instantiate(spellPrefab, pos, Quaternion.identity, transform);

        go.name = "Spell" + Random.Range(1, 100);

        Placeable pl = go.GetComponent<Placeable>();

        gm.SetMoney(gm.money - pl.price);

    }

    public bool IsPlaceable(Tile t)
    {
        if (t != null &&
            t.building == null && 
            t.tileType == TileType.Buildable && gm.money >= cannonPrefab.GetComponent<Placeable>().price 
            && gm.gameOn == true)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public bool IsPath(Tile t)
    {
        if(t != null && 
           t.tileType == TileType.Path && 
           gm.money >= spellPrefab.GetComponent<Placeable>().price)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
