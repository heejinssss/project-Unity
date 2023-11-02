using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager4 : MonoBehaviour
{
    public TalkManager4 talkManager;
    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;

    public bool isAction;
    public int talkIndex;

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjData4 objData = scanObject.GetComponent<ObjData4>();
        Talk(objData.id, objData.isNpc);

        talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNpc)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);

        if (talkData == null)
        {
            talkIndex = 0;
            isAction = false;
            return;
        }

        // ���⼭ npc�� ���� �б�ó���� ����
        talkText.text = talkData;

        isAction = true;
        talkIndex++;
    }
}
