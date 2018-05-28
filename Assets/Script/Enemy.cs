using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    GameMaster gm;

    List<Tile> path = new List<Tile>();

    float speed = 1f;
    Sprite enemySprite;

    Tile currTile;
    Tile nextTile;
    public int currIndex = 0;


	// Use this for initialization
	void Start () {
        gm = GameObject.FindObjectOfType<GameMaster>();

        currTile = path[currIndex];
        nextTile = path[currIndex + 1];
    }
	
	// Update is called once per frame
	void Update () {

        Vector3 pos = transform.position;

        //Vector3 pos1 = TileHelper.PositionOfTile(currTile);
        Vector3 pos2= TileHelper.PositionOfTile(nextTile);

        if (Vector3.Distance(pos,pos2) < 0.01f)
        {
            currIndex++;

            currTile = path[currIndex];
            nextTile = path[currIndex + 1];
        }

        pos = Vector3.MoveTowards(pos, pos2, speed * Time.deltaTime);


        transform.position = pos;

	}
}
