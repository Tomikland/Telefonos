using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Cannon : Placeable {

    public float range = 3f;
    public int damage = 1;
    public float projectileSpeed = 1f;

    public float baseCooldown = 1f;
    public float shotCooldown;

    public GameObject projectilePrefab;

    public GameMaster gm;
    public float increment = 0.05f;
    public float threshold = 0.05f;

    Lines lines;


	// Use this for initialization
	void Start () {

        gm = GameObject.FindObjectOfType<GameMaster>();
        lines = FindObjectOfType<Lines>();

        //price = 10;
        lines.Flush();
	}
	
	// Update is called once per frame
	void Update () {


        //Enemy target = FindEnemy();
       // Vector2 p = target.transform.position;
       // lines.DrawNewLine(p, this.transform.position);

        
        if (gm.gameOn == false) { return; }

        shotCooldown -= Time.deltaTime;


		if(shotCooldown <= 0 )
        {
            lines.Flush();
            Enemy target = FindEnemy();
            shotCooldown = baseCooldown;
            if (target != null && target.path.Count > 0)
            {
                float d1;
                float d2;
                Vector2 p = target.transform.position;
                Vector2 result = target.transform.position;
                float t = 0;

                do
                {
                    t += increment;
                    p = TileHelper.PredictEnemyPos(target, t);

                    if(p == Vector2.zero || t > 10)
                    {
                        //Debug.Log("Couldn't find a shot");
                        p = target.transform.position;
                        break;
                    }

                    d1 = target.speed * t;
                    d2 = Vector2.Distance(p, transform.position);
                //Debug.Log(t);

                } while (Mathf.Abs( d1 / d2 - (target.speed / projectileSpeed)) > threshold);



                result = p;

                //Shoot

                Vector3 offset = new Vector3(0, 0, -0.1f);
                GameObject go = Instantiate(projectilePrefab, transform.position + offset, Quaternion.identity, transform);

                Projectile pr = go.GetComponent<Projectile>();

                Vector2 cannonPos = transform.position;

                //transform.rotation = Quaternion.FromToRotation(transform.position, result);

                pr.dest = result;
                pr.target = target;
                pr.damage = this.damage;
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
                    //Debug.Log(gameObject.name);

                    enemies[i].tag = "Targeted";
                    return enemies[i];

                }
            }
            
            //return enemies[0];
        }
        return null;
    }

}
