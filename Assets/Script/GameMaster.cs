using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

    GameGrid gg;
    EnemySpawner es;


    public int startMoney = 175;
    public int money = 0;
    public int health = 3;
    public bool gameOn;


    public Text moneyText;
    public GameObject healthContainer;
    public GameObject heart;

    public List<Enemy> activeEnemies;

    public GameObject nextLevelScreen;
    public GameObject endGameScreen;
    public GameObject gameOverScreen;
    public GameObject pauseScreen;
    public GameObject restartScreen;

	// Use this for initialization
	void Start () {


        gg = FindObjectOfType<GameGrid>();
        es = FindObjectOfType<EnemySpawner>();

        money = startMoney;

        UpdateStatusBar();



        gameOn = true;

        if (es.cooldown < -2f)
        {
            Debug.Log("Secondary win");
        }
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
        StartCoroutine(WaitThenSetGameon(0.5f,true));
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
    public void PromptRestart()
    {
        gameOn = false;

        restartScreen.SetActive(true);
    }

    public void CancelRestart()
    {
        StartCoroutine(WaitThenSetGameon(0.1f, true));
        restartScreen.SetActive(false);
    }


    public void Reset()
    {
        nextLevelScreen.SetActive(false);
        endGameScreen.SetActive(false);
        gameOverScreen.gameObject.SetActive(false);
        restartScreen.SetActive(false);

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
           
    
            money = startMoney;

        UpdateStatusBar();
    }

    public void Restart()
    {
        gg.mapIndex = 0;

        

        Debug.Log("RESTART");

        gg.mapIndex = 0;
        gg.SetUpMap();

        es.waveIndex = 0;

        Reset();

        StartCoroutine(WaitThenSetGameon(0.5f,true));
    }

    IEnumerator WaitThenSetGameon(float seconds, bool val)
    {
        yield return new WaitForSeconds(seconds);
        gameOn = val;

    }

    public void PauseUnPause()
    {
        if (restartScreen.activeSelf == true)
        {
            CancelRestart();
        }
        else
        {
            pauseScreen.SetActive(!pauseScreen.activeSelf);
            StartCoroutine(WaitThenSetGameon(0.01f, !gameOn));
        }
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
