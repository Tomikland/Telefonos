using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Placeable {

    public float range = 3f;
    public float damage = 1f;
    public float projectileSpeed = 1f;

    public float baseCooldown = 1f;
    public float shotCooldown;

    public GameObject projectilePrefab;


	// Use this for initialization
	void Start () {

        price = 10;
	}
	
	// Update is called once per frame
	void Update () {

        shotCooldown -= Time.deltaTime;

		if(shotCooldown <= 0)
        {
            shotCooldown = baseCooldown;
            Enemy target = FindEnemy();

            //Shoot
            float t;

            Debug.Log("Shoot!!");

            GameObject go = Instantiate(projectilePrefab,transform.position,Quaternion.identity,transform);

            Projectile pr = go.GetComponent<Projectile>();

            pr.dest = target.transform.position;
            pr.speed = projectileSpeed;

        }
	}

    public Enemy FindEnemy()
    {
        return FindObjectOfType<Enemy>();
    }
}
