using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    int seed;
    int samplePos = 0;

    public GameObject enemyPrefab;

    public List<Wave> waves = new List<Wave>();
    public Dictionary<int, List<Wave>> mapComp = new Dictionary<int, List<Wave>>();
    public enum EnemyType { green, brown, viking }

    public List<GameObject> enemyPrefabs = new List<GameObject>();

    GameMaster gm;
    GameGrid gg;
	// Use this for initialization
	void Start () {


        gg = FindObjectOfType<GameGrid>();
        gm = FindObjectOfType<GameMaster>();

        //----------------------------STAGE 1-------------------------------------

        waves = new List<Wave>();
        waves.Add(new Wave(3,2f,new List<EnemyType> {EnemyType.brown, EnemyType.green }));
        waves.Add(new Wave(5, 1.5f, new List<EnemyType> { EnemyType.brown, EnemyType.viking }));
        mapComp.Add(0, waves);

        //----------------------------STAGE 2-------------------------------------

        waves = new List<Wave>();
        waves.Add(new Wave(3, 2f, new List<EnemyType> { EnemyType.brown, EnemyType.green }));
        waves.Add(new Wave(5, 1.5f, new List<EnemyType> { EnemyType.brown, EnemyType.viking }));
        mapComp.Add(1, waves);

        //----------------------------STAGE 2-------------------------------------

        waves = new List<Wave>();
        waves.Add(new Wave(3, 2f, new List<EnemyType> { EnemyType.brown, EnemyType.green }));
        waves.Add(new Wave(5, 1.5f, new List<EnemyType> { EnemyType.brown, EnemyType.viking }));
        mapComp.Add(2, waves);
    }

    public float minCooldown = 0.15f;
    public float maxCooldown = 10f;

    public float cooldown = 2;
    int waveIndex;

    public int spawnedEnemies;
	// Update is called once per frame
	void Update () {

        waves = mapComp[gg.mapIndex];

        if (gm.gameOn == false)
        {
            return;
        }

        if (waveIndex >= waves.Count && gm.activeEnemies.Count == 0)
        {
            waveIndex = 0;
            gm.NextLevel();

        }

        cooldown -= Time.deltaTime;

        if(cooldown <= 0)
        {
            if(waveIndex >= waves.Count)
            {
                return;
            }

            Wave wave = waves[waveIndex];
            if(wave == null)
            {
                return;
            }

            //spawn
            //Debug.Log("Spawn!!");
            GameObject go = Instantiate(enemyPrefabs[(int)ChooseTypeToSpawn(wave)] ,transform);

            gm.activeEnemies.Add(go.GetComponent<Enemy>());
            spawnedEnemies++;

            cooldown = wave.cooldown;
            
            if(spawnedEnemies >= wave.length )
            {
                waveIndex++;               
                spawnedEnemies = 0;
            }
        }

	}

    public EnemyType ChooseTypeToSpawn(Wave wave)
    {
        int rand = Random.Range(0, wave.types.Count);
        return wave.types[rand];
    }


    public class Wave
    {

        public int length;
        public float cooldown;
        
        public List<EnemyType> types = new List<EnemyType>();

        public Wave(int Length, float Cooldown, List<EnemyType> Types)
        {
            length = Length;
            cooldown = Cooldown;
            types = Types;
        }
    }
}
