using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager4 : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(1001, new string[] { "�ٸ������� ������ ���ù����� �Ϻ� ������ ���ͷ��� ��� �־��.",
                                          "�״� Ŀ�ٶ� �����̸� �ֵθ��� ���ù����� ������ �������� �ٸ� ���� ���� �ص��� �Ⱦ��ؼ� �̿� �������� �Դ´ٰ� �ؿ�.",
                                          "������ ������ �ٸ� ���� �Ƿ��̶�� ����� ��� �� �����ſ���!",

                                          });
        talkData.Add(1002, new string[] { "�� ������?", "�ݰ���!" });
        talkData.Add(3000, new string[] { "�ƹ� �ϵ� �Ͼ�� �ʴ´�..." });
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }
}
