using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager5_0 : MonoBehaviour
{
    public int stageIndex;
    public GameObject[] stages;

    public TalkManager5_0 talkManager;
    public GameObject talkPanel;
    public GameObject defaultPanel;
    public Image portrait;
    public Text talkText;
    public Text defaultText;
    public int talkIdx;
    public bool isAction;

    private int[] TopDownStages = { 1 };

    BGMSounder5_0 sounder;

    void Awake()
    {
        sounder = GetComponent<BGMSounder5_0>();

        Talk(stageIndex);
    }

    public void NextStage()
    {
        if (stageIndex == stages.Length) // Ending
        {
            SceneManager.LoadScene("Map");
        }
        // Change Stages
        if (stageIndex < stages.Length)
        {
            stages[stageIndex].SetActive(false);
            stageIndex++;
            stages[stageIndex].SetActive(true);
        }

        if (TopDownStages.Contains(stageIndex)) // Top-Down Scene
        {
            // Need To Add Logic
        }
        // Talk(stageIndex);
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
}
