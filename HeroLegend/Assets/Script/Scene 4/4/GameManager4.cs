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
            // �̴ϰ��ӿ��� ���ƿ��� ���
            // �ٽ� 0���� �ʱ�ȭ����, ���� ���� ���۵��� ���� ���ؼ�
            PlayerPrefs.SetInt("isReturnToTopDownWorld", 0);
            WhenReturnToTopDownWorld();
        }

        if (PlayerPrefs.GetInt("isClear") == 1)
        {
            // ������ Ŭ�����ϰ� ������ ��, (������ ���� �ݿ���, ���Ĵ� �ݿ�X)
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
