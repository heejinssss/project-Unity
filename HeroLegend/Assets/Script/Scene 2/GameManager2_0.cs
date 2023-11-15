using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager2_0 : MonoBehaviour
{
    public TalkManager2 talkManager;
    public GameObject talkPanel;
    public Text talkText;
    public Image portraitImg;

    public GameObject scanObject;
    public GameObject portalButton;
    public GameObject cancelButton;

    public bool isAction;
    public int talkIndex;

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjData0 objData = scanObj.GetComponent<ObjData0>();
        Talk(objData.id);
        DeleteExclamationMark(objData);

        talkPanel.SetActive(isAction);
    }

    void DeleteExclamationMark(ObjData0 obj)
    {
        // 퀘스트 마크 지우기 
        Transform exclamationMark = obj.transform.Find("ExclamationMark");
        if (exclamationMark != null)
        {
            exclamationMark.gameObject.SetActive(false);
        }
    }

    void Talk(int id)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);
        if (talkData == null) {
            isAction = false;
            talkIndex = 0;
            return;
        }
        talkText.text = talkData;

        Sprite portrait = talkManager.GetPortrait(id, talkIndex);
        portraitImg.sprite = portrait;

        isAction = true;
        talkIndex++;
    }

    public void MoveToScene2()
    {
        SceneManager.LoadScene("Scene 2");
    }

    public void CancelMove()
    {
        SetActiveButton(false);
    }

    public void SetActiveButton(bool flag)
    {
        cancelButton.SetActive(flag);
        portalButton.SetActive(flag);
    }
}
