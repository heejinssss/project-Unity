using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{
    const float ORIGIN_SPEED = 3;
    public int stageIndex;
    public int health;
    public Player player;
    public GameObject[] stages;
    public GameObject playerObj;

    public static float globalSpeed;
    public static float score;
    public static bool isLive;
    public GameObject uiHealth;
    public GameObject uiOver;

    public TalkManager talkManager;
    public GameObject talkPanel;
    public GameObject defaultPanel;
    public GameObject rulePanel;
    public GameObject black;
    public Image portrait;
    public Text talkText;
    public Text defaultText;
    public int talkIdx;
    public bool isAction;

    private int[] storyStages = { 0, 2, 4, 6 };
    public GameObject boss0;
    public GameObject UIMask;
    public GameObject UISlime;
    public GameObject UIBoss;

    void Awake()
    {
        isLive = true; // 게임 시작 버튼을 눌렀을 때 true로 변환

        if (!PlayerPrefs.HasKey("Score"))
            PlayerPrefs.SetFloat("Score", 0);

        Talk(stageIndex); 
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
        if (storyStages.Contains(stageIndex))
        {
            playerObj.SetActive(false);
            uiHealth.SetActive(false);
            Talk(stageIndex);
        } else
        {
            playerObj.SetActive(true);
            uiHealth.SetActive(true);
        }
    }

    public void HealthDown()
    {
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

    void Talk(int stage)
    {
        string talkData = talkManager.GetTalk(stage, talkIdx);

        if (talkData == null)
        {
            isAction = false;
            talkIdx = 0;
            talkPanel.SetActive(false);
            defaultPanel.SetActive(false);
            NextStage();
            return;
        }

        if (talkData.Split(":")[1] == "default")
        {
            defaultPanel.SetActive(true);
            defaultText.text = talkData.Split(":")[0];
        }
        else if (talkData.Split(":")[1] == "desc")
        {
            switch (talkData.Split(":")[0])
            {
                case "ruleStart":
                    rulePanel.SetActive(true);
                    break;
                case "ruleEnd":
                    rulePanel.SetActive(false);
                    break;
                case "blackStart":
                    black.SetActive(true);
                    break;
                case "blackEnd":
                    black.SetActive(false);
                    break;
                case "blackShockStart":
                    black.SetActive(true);
                    defaultPanel.SetActive(true);
                    defaultText.text = "쾅!!!!!!!!!!!!!";
                    break;
                case "blackShockEnd":
                    defaultPanel.SetActive(false);
                    black.SetActive(false);
                    break;
                case "moveBoss":
                    boss0.transform.Translate(-7.5f, -2f, 0, Space.Self);
                    break;
                case "blackBossStart":
                    black.SetActive(true);
                    UIMask.SetActive(true);
                    break;
                case "blackBossEnd":
                    black.SetActive(false);
                    UIMask.SetActive(false);
                    isAction = false;
                    talkIdx = 0;
                    talkPanel.SetActive(false);
                    defaultPanel.SetActive(false);
                    NextStage();
                    break;
                case "slimeChangeStart":
                    black.SetActive(true);
                    UISlime.SetActive(true);
                    break;
                case "slimeChangeEnd":
                    black.SetActive(false);
                    UISlime.SetActive(false);
                    isAction = false;
                    talkIdx = 0;
                    talkPanel.SetActive(false);
                    defaultPanel.SetActive(false);
                    NextStage();
                    break;
                case "bossEndStart":
                    black.SetActive(true);
                    UIBoss.SetActive(true);
                    break;
                case "bossEndEnd":
                    black.SetActive(false);
                    UIBoss.SetActive(false);
                    break;
                default:
                    break;
            }
        }
        else
        {
            talkPanel.SetActive(true);
            talkText.text = talkData.Split(":")[0];
            portrait.sprite = talkManager.GetPortrait(int.Parse(talkData.Split(":")[1]));
        }

        isAction = true;
        talkIdx++;
    }

    public void Action()
    {
        talkPanel.SetActive(false);
        defaultPanel.SetActive(false);
        Talk(stageIndex);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1; // 시간 복구
        isLive = true;
    }

}
