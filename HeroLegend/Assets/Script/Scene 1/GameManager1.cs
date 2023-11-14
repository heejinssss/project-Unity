using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager1 : MonoBehaviour
{
    public static GameManager1 instance;
    [Header("# Game Control")]
    public bool isLive;
    public float gameTime;
    [Header("# Player Info")]
    public float health;
    public float maxHealth = 100;
    public float bosshealth = 1000;
    public float maxbosshealth = 1000;
    public float characterdamage;

    // public int level;
    [Header("# Game Object")]
    public PoolManager1 pool;
    public Player1 player;
    public Result1 uiResult;
    // public Transform uiJoy;

    void Awake()
    {
        instance = this;
        Stop();

    }
    public void GameStart() 
    {
        // playerId = id;
        health = maxHealth;
        isLive = true;
        // bosshealth = maxbosshealth;
        player.gameObject.SetActive(true);
        // uiLevelUp.Select(playerId % 2);
        Resume();  
        // AudioManager.instance.PlayBgm(true);
        // AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }
    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }
    IEnumerator GameOverRoutine()
    {
        isLive = false;
        yield return new WaitForSeconds(0.5f);
        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();
        // AudioManager.instance.PlayBgm(false);
        // AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose);

    }
    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }
    IEnumerator GameVictoryRoutine()
    {
        isLive = false;
        // enemyCleaner.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();

        // AudioManager.instance.PlayBgm(false);
        // AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);

    }
    public void GameRetry()
    {
        SceneManager.LoadScene(6);
    }
    public void NextGameStage()
    {
        SceneManager.LoadScene(1);
    }


    void Update()
    {
        if (!isLive)
            return;

        gameTime += Time.deltaTime;
        if (bosshealth < 0)
        {
            GameVictory();
        }

        // if (gameTime > maxGameTime)
        // {
        //     gameTime = maxGameTime;
        //     GameVictory();
        // }
    }

    public void Stop() 
    {
        isLive = false;
        Time.timeScale = 0;
        // uiJoy.localScale = Vector3.zero;
    }
    public void Resume() 
    {
        isLive = true;
        Time.timeScale = 1;
        // uiJoy.localScale = Vector3.one;
    }

}