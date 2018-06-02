using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    int seed;
    int samplePos = 0;

    public GameObject enemyPrefab;
	// Use this for initialization
	void Start () {

        seed = Random.Range(0, 100);

    }

    public float minCooldown = 0.15f;
    public float maxCooldown = 10f;

    public float cooldown = 2;
	// Update is called once per frame
	void Update () {

        cooldown -= Time.deltaTime;

        if(cooldown <= 0)
        {
            //spawn
            //Debug.Log("Spawn!!");
            GameObject go = Instantiate(enemyPrefab,transform);


            cooldown = 2f;//Mathf.Lerp(minCooldown, maxCooldown, 1 / 1 / Time.time);
            samplePos++;

        }

	}
}
