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


	// Use this for initialization
	void Start () {

        gm = GameObject.FindObjectOfType<GameMaster>();

        price = 10;
	}
	
	// Update is called once per frame
	void Update () {

        if (gm.gameOn == false) { return; }

        shotCooldown -= Time.deltaTime;

		if(shotCooldown <= 0 )
        {
            Enemy target = FindEnemy();
                shotCooldown = baseCooldown;
            if (target != null && target.path.Count > 0)
            {

                //Shoot
                float t = Vector3.Distance(target.transform.position, transform.position) / projectileSpeed;

                //Debug.Log("Shoot!!");

                GameObject go = Instantiate(projectilePrefab, transform.position, Quaternion.identity, transform);

                Projectile pr = go.GetComponent<Projectile>();

                pr.dest = TileHelper.PredictEnemyPos(target, t);
                //pr.dest = target.transform.position;
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
