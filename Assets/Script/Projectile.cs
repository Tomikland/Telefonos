using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed;
    public Enemy target;
    public Vector2 dest;

    public GameMaster gm;

	// Use this for initialization
	void Start () {
		gm = GameObject.FindObjectOfType<GameMaster>();
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
        if (target != null)
        {
            gm.SetMoney(gm.money + 1);
            Destroy(target.gameObject);
        }
        Destroy(gameObject);
    }
}
