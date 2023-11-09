using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager3 : MonoBehaviour
{
    const float ORIGIN_SPEED = 3;
    public int stageIndex;
    public int health;
    public Player3 player;
    public GameObject[] stages;

    public static float globalSpeed;
    public static float score;
    public static bool isLive;
    public GameObject uiOver;

    void Awake()
    {
        isLive = true; // 게임 시작 버튼을 눌렀을 때 true로 변환

        if (!PlayerPrefs.HasKey("Score"))
            PlayerPrefs.SetFloat("Score", 0);
    }

    void Update()
    {
        if (!isLive)
            return;

        score = health;
        globalSpeed = ORIGIN_SPEED;
    }

    public void NextStage()
    {
        // Change Stages
        if (stageIndex < stages.Length)
        {
            stages[stageIndex].SetActive(false);
            stageIndex++;
            stages[stageIndex].SetActive(true);
        }

    }

    public void HealthDown()
    {
        Debug.Log("nowHealth : " + health);
        if (health > 1)
        {
            health--;
        }
        else
        {
            // Player Die Effect
            player.OnDie();

            uiOver.SetActive(true);
            isLive = false;
        }
    }


    public void Restart()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1; // 시간 복구
        isLive = true;
    }
}
