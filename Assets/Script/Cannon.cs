using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Placeable {

    public float range = 3f;
    public float damage = 1f;
    public float projectileSpeed = 1f;

    public float cooldown = 1f;
    public float shotCooldown;


	// Use this for initialization
	void Start () {

        price = 10;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
