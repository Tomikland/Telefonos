using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Cannon : Placeable {

    public float range = 3f;
    public float damage = 1f;
    public float projectileSpeed = 1f;

    public float baseCooldown = 1f;
    public float shotCooldown;

    public GameObject projectilePrefab;

    public GameMaster gm;

    Lines lines;


	// Use this for initialization
	void Start () {

        gm = GameObject.FindObjectOfType<GameMaster>();
        lines = FindObjectOfType<Lines>();

        price = 10;
        lines.Flush();
	}
	
	// Update is called once per frame
	void Update () {

        if (gm.gameOn == false) { return; }

        shotCooldown -= Time.deltaTime;


		if(shotCooldown <= 0 )
        {
            lines.Flush();
            Enemy target = FindEnemy();
                shotCooldown = baseCooldown;
            if (target != null && target.path.Count > 0)
            {
                Vector2 p = target.transform.position;
                float t = 0;
                float delta;
                int i = 0;
                Vector2 result;

                do
                {
                    lines.DrawNewLine(transform.position,p);

                    Vector2 lastP = p;
                    t = Vector2.Distance(p, transform.position) / projectileSpeed;
                    p = TileHelper.PredictEnemyPos(target, t);

                    Debug.Log(p);


                    delta = Vector2.Distance(p, lastP);

                    if(i == 10)
                    {
                        Debug.Log("Couldn't find it in 500 iterations. That kind of sucks.");
                        result = target.transform.position;
                        return;//break;
                    }

                    result = p;
                    i++;
                }
                while (delta > 0.1);
                
                //Shoot
                GameObject go = Instantiate(projectilePrefab, transform.position, Quaternion.identity, transform);

                Projectile pr = go.GetComponent<Projectile>();

                pr.dest = result;
                pr.target = target;
                pr.speed = projectileSpeed;
                
            }

        }
	}

    public Enemy FindEnemy()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        if (enemies.Length < 1)
        {
            return null;
        }
        else
        {
            enemies = enemies.OrderBy(Enemy => Vector3.Distance(transform.position, Enemy.transform.position)).ToArray();

            for (int i = 0; i < enemies.Length; i++)
            {
                //Debug.Log("futi");
                if(enemies[i].tag != "Targeted" && Vector3.Distance(transform.position,enemies[i].transform.position) <= range)
                {
                    Debug.Log(gameObject.name);

                    enemies[i].tag = "Targeted";
                    return enemies[i];

                }
            }
            
            //return enemies[0];
        }
        return null;
    }

}
