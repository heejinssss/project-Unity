using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager4 : MonoBehaviour
{
    public PlayerMovement4 player;
    public LegBossShape legBoss;
    public GameObject[] Stages;

    private int curStage = 0;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("isReturnToTopDownWorld") == 1)
        {
            // 미니게임에서 돌아왔을 경우
            // 다시 0으로 초기화하자, 다음 번의 오작동을 막기 위해서
            PlayerPrefs.SetInt("isReturnToTopDownWorld", 0);
            WhenReturnToTopDownWorld();
        }

        if (PlayerPrefs.GetInt("isClear") == 1)
        {
            // 보스를 클리어하고 나왔을 때, (나왔을 때만 반영됨, 이후는 반영X)
            legBoss.ChangeShapeWhenClear();

            PlayerPrefs.SetInt("isClear", 0);
        }
    }

    public void StageMove4()
    {
        Stages[curStage].SetActive(false);

        if (curStage == 0)
        {
            PlayerReposition(new Vector3(7.5f, -3.5f, -1f));
        }  
        else if(curStage == 1)
        {
            PlayerReposition(new Vector3(7.5f, -7f, -1f));
        }

        curStage = 1 - curStage;
        Stages[curStage].SetActive(true);
    }

    public void SceneMove4(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void PlayerReposition(Vector3 newPos)
    {
        player.transform.position = newPos;
    }

    void WhenReturnToTopDownWorld()
    {
        Stages[0].SetActive(false);
        Stages[1].SetActive(true);
        player.transform.position = new Vector3(7.5f, 12.7f, -1f);
    }
}
