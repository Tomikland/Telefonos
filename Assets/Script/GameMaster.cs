using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

    GameGrid gg;
    EnemySpawner es;

    public int money = 200;
    public int health = 3;
    public bool gameOn;

    public Text gameovertext;

    public Text moneyText;
    public GameObject healthContainer;
    public GameObject heart;

    public List<Enemy> activeEnemies;

    public GameObject nextLevelScreen;

	// Use this for initialization
	void Start () {


        gg = FindObjectOfType<GameGrid>();
        es = FindObjectOfType<EnemySpawner>();
        UpdateStatusBar();

        gameOn = true;
    }
	
	// Update is called once per frame
	void Update () {
		


	}

    public void DamagePlayer()
    {
        health--;

        UpdateStatusBar();


        if (health <= 0)
        {
            GameOver();
        }
    }

    public void SetMoney(int amt)
    {
        money = amt;

        UpdateStatusBar();

    }
    public void GameOver()
    {
        gameovertext.gameObject.SetActive(true);
        gameOn = false;
    }

    public void NextLevel()
    {
        nextLevelScreen.SetActive(false);

        Debug.Log("NEXTLEVEL");
        gameOn = false;

        gg.mapIndex++;
        gg.SetUpMap();

        es.waveIndex = 0;

        Reset();
        gameOn = true;
    }
    public void PromptNextLevel()
    {
        nextLevelScreen.SetActive(true);
    }


    public void Reset()
    {
        Placer pl = FindObjectOfType<Placer>();
        for (int i = 0; i < pl.transform.childCount; i++)
        {
            Destroy(pl.transform.GetChild(i).gameObject);

        }

            Debug.Log("RESET");
            health = 3;
           
    
            money = 200;
    }
    public void UpdateStatusBar()
    {

        int i;
        for ( i = 0; i < Mathf.Max( transform.childCount,health); i++)
        {
            if (healthContainer.transform.childCount <= i)
            {
                Instantiate(heart,healthContainer.transform);
            }
            else
            {
                Destroy(healthContainer.transform.GetChild(i).gameObject);
            }
        }


        string mtext = ""+money;
        moneyText.text = mtext;
    }


}
