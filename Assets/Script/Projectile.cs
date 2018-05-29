using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed;
    public Vector2 dest;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 pos = transform.position;

        pos = Vector2.MoveTowards(pos, dest, speed * Time.deltaTime);

        transform.position = pos;

        if (Vector2.Distance(pos,dest) < 0.01f)
        {
            Explode();
        }
	}

    void Explode()
    {

    }
}
