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


    public Text moneyText;
    public GameObject healthContainer;
    public GameObject heart;

    public List<Enemy> activeEnemies;

    public GameObject nextLevelScreen;
    public GameObject endGameScreen;
    public GameObject gameOverScreen;
    public GameObject PauseScreen;

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
        gameOverScreen.gameObject.SetActive(true);
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
        StartCoroutine(WaitBeforeStart());
    }
    public void PromptNextLevel()
    {
        gameOn = false;

        if (gg.mapIndex == gg.mapList.Count - 1)
        {
            endGameScreen.SetActive(true);
        }
        else
        {
            nextLevelScreen.SetActive(true);
        }
    }


    public void Reset()
    {
        Placer pl = FindObjectOfType<Placer>();
        for (int i = 0; i < pl.transform.childCount; i++)
        {
            Destroy(pl.transform.GetChild(i).gameObject);

        }
        
        for (int i = 0; i < es.transform.childCount; i++)
        {
            Destroy(es.transform.GetChild(i).gameObject);

        }

        Debug.Log("RESET");
            health = 3;
           
    
            money = 200;

        UpdateStatusBar();
    }

    public void Restart()
    {
        gg.mapIndex = 0;

        nextLevelScreen.SetActive(false);
        endGameScreen.SetActive(false);
        gameOverScreen.gameObject.SetActive(false);

        Debug.Log("RESTART");

        gg.mapIndex = 0;
        gg.SetUpMap();

        es.waveIndex = 0;

        Reset();

        StartCoroutine(WaitBeforeStart());
    }

    IEnumerator WaitBeforeStart()
    {
        yield return new WaitForSeconds(1);
        gameOn = true;

    }

    public void PauseUnPause()
    {
        PauseScreen.SetActive(!PauseScreen.activeSelf);
        gameOn = !gameOn;
    }

    public void UpdateStatusBar()
    {

        int i;
        for ( i = 0; i < Mathf.Max( healthContainer.transform.childCount,health); i++)
        {

            if (healthContainer.transform.childCount < i + 1)
            {
                Instantiate(heart,healthContainer.transform);
            }
            if ( i + 1 > health)
            {

                Destroy(healthContainer.transform.GetChild(i).gameObject);
            }
        }


        string mtext = ""+money;
        moneyText.text = mtext;
    }


}
