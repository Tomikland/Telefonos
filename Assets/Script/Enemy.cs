using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    GameMaster gm;
    GameGrid gg;

    public List<Tile> path = new List<Tile>();

    public float speed = 1f;
    public Sprite enemySprite;
    public int startHealth = 1;
    public int currHealth;
    public GameObject spriteGO;

    public Tile currTile;
    public Tile nextTile;
    public int currIndex = 0;

    public Vector2 dir;
    public int worth = 5;   

    public GameObject healthBar;



	// Use this for initialization
	void Start () {
        gm = GameObject.FindObjectOfType<GameMaster>();
        gg = GameObject.FindObjectOfType<GameGrid>();

        path = gg.path;


        currTile = path[currIndex];
        nextTile = path[currIndex + 1];

        transform.position = TileHelper.TilePosition(currTile);

        currHealth = startHealth;

        //prediction = TileHelper.PredictEnemyPos(this, 2.6f);
    }
	
	// Update is called once per frame
	void Update () {

        

        if (gm.gameOn == false)
        {
            return;
        }

        if(path.Count < 1)
        {
            path = gg.path;
        }

        Vector3 pos = transform.position;

        //Vector3 pos1 = TileHelper.PositionOfTile(currTile);
        Vector3 pos2= TileHelper.TilePosition(nextTile);

        if (Vector3.Distance(pos,pos2) < 0.01f)
        {

            currTile = path[currIndex];

            if (currIndex == path.Count - 2)
            {
                gm.activeEnemies.Remove(this);
                gm.DamagePlayer();
                //despawn enemy
                
                
                Destroy(this.gameObject);
            }
            else
            {
                currIndex++; 
                nextTile = path[currIndex + 1];
            }
        }

        pos = Vector3.MoveTowards(pos, pos2, speed * Time.deltaTime);


        transform.position = pos;

        dir = nextTile.Position() - currTile.Position();
        spriteGO.transform.up = dir;

        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currHealth -= damage;
        UpdateHealthBar();

        if (currHealth <= 0)
        {
            gm.SetMoney(gm.money + worth);
            gm.activeEnemies.Remove(this);
            Destroy(gameObject);
        }
    }

    void UpdateHealthBar()
    {
        float size =  (float)currHealth / startHealth;
        Vector2 scl = healthBar.transform.localScale;
        Vector2 pos = healthBar.transform.localPosition;
        scl.x = size;

        if(dir.y == 0)
        {
            pos.y = -0.25f;
        }
        else
        {
            pos.y = -0.65f;
        }

        healthBar.transform.localPosition = pos;
        healthBar.transform.localScale = scl;
    }
}
