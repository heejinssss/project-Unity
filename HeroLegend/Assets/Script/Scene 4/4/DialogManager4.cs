using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager4 : MonoBehaviour
{
    public TalkManager4 talkManager;
    public GameObject talkPanel;
    public Image portraitImg;
    public Text talkText;
    public GameObject scanObject;
    public GameManager4 gameManager;
    public Camera4 camera4;

    public bool isAction;
    public int talkIndex;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjData4 objData = scanObject.GetComponent<ObjData4>();
        Talk(objData.id, objData.isNpc);

        talkPanel.SetActive(isAction);
    }

    public void Talk(int id, bool isNpc)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);
        audioSource.Play();

        if (talkData == null)
        {
            talkIndex = 0;
            isAction = false;

            // 보스와의 대화가 끝났다면 보스전 시작
            if (id == 100)
            {
                camera4.ZoomCamera();
                Invoke("StartLegBoss", 1.75f);
            }
            else if (id == 101)
            {
                gameManager.DeactivateResultDelieverObject();

                // 다음 지역 선택하는 곳으로 전환하자
                // 코드 들어갈 부분 /////////////////////

                /////////////////////////////////////////            
            }
            else if (id == 102)
            {
                gameManager.DeactivateResultDelieverObject();
            }

            return;
        }

        // 여기서 npc에 따른 분기처리도 가능
        if (isNpc)
        {
            talkText.text = talkData.Split(':')[0];

            // 초상화를 다 보여주기
            portraitImg.sprite = talkManager.GetPortrait(int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            talkText.text = talkData;

            // 안보이게 하기
            portraitImg.color = new Color(1, 1, 1, 0);
        }

        isAction = true;
        talkIndex++;
    }

    void StartLegBoss()
    {
        gameManager.SceneMove4("Scene 4 - 1");
    }
}
