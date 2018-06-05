using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : Placeable {

    public GameMaster gm;
    public float range = 3f;
    public int damage = 1;

    public float baseCooldown = 1f;
    public float shotCooldown;

    public float startTime = 3f;
    public float timeLeft;

    public float mult;
    public float rotSpeed = 3f;

    public Vector3 startSize;

    SpriteRenderer spr;

    // Use this for initialization
    void Start () {
        gm = GameObject.FindObjectOfType<GameMaster>();

        shotCooldown = 0.1f;
        timeLeft = startTime;

        spr = GetComponentInChildren<SpriteRenderer>();

        startSize = spr.transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {
        if (gm.gameOn == false) { return; }

        shotCooldown -= Time.deltaTime;
        timeLeft -= Time.deltaTime;

        if(shotCooldown < 0)
        {
            foreach (Enemy enemy in FindEnemies())
            {
                enemy.TakeDamage(damage);
            }

            shotCooldown = baseCooldown;
        }

        mult =  Mathf.Pow( 1 - (timeLeft / startTime),2);

        spr.transform.localScale = startSize * (1 - mult) ;

        transform.Rotate(new Vector3(0,0,rotSpeed*Time.deltaTime * (mult+0.1f)));


        if(timeLeft < 0)
        {
            Destroy(this.gameObject);
        }
    }

    public List<Enemy> FindEnemies()
    {
        List<Enemy> result = new List<Enemy>();

        Enemy[] enemies = FindObjectsOfType<Enemy>();

        for (int i = 0; i < enemies.Length; i++)
        {
            //Debug.Log("futi");
            if ( Vector3.Distance(transform.position, enemies[i].transform.position) <= range)
            {         
                result.Add( enemies[i]);
            }
        }
        return result;

    }
}
