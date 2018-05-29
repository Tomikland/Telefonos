using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    GameMaster gm;
    GameGrid gg;

    public List<Tile> path = new List<Tile>();

    public float speed = 1f;
    public Sprite enemySprite;
    public GameObject spriteGO;

    public Tile currTile;
    public Tile nextTile;
    public int currIndex = 0;

    public Vector2 prediction;



	// Use this for initialization
	void Start () {
        gm = GameObject.FindObjectOfType<GameMaster>();
        gg = GameObject.FindObjectOfType<GameGrid>();

        path = gg.path;

        currTile = path[currIndex];
        nextTile = path[currIndex + 1];

        transform.position = TileHelper.TilePosition(currTile);

        prediction = TileHelper.PredictEnemyPos(this, 2.6f);
    }
	
	// Update is called once per frame
	void Update () {

        Vector3 pos = transform.position;

        //Vector3 pos1 = TileHelper.PositionOfTile(currTile);
        Vector3 pos2= TileHelper.TilePosition(nextTile);

        if (Vector3.Distance(pos,pos2) < 0.01f)
        {

            currTile = path[currIndex];

            if (currIndex == path.Count - 1)
            {
                gm.DamagePlayer();
                //despawn enemy

                Destroy(this.gameObject);
            }
            else
            {
                nextTile = path[currIndex + 1];
                currIndex++; 
            }
        }

        pos = Vector3.MoveTowards(pos, pos2, speed * Time.deltaTime);


        transform.position = pos;

        Vector2 dir = nextTile.Position() - currTile.Position();
        spriteGO.transform.up = dir;
	}
}
