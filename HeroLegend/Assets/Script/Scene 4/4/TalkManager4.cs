using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager4 : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    public Sprite[] portraitArr;

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(1010, new string[] { "�ٸ������� ������ ���ù����� �Ϻ� ������ [���ͷ�]�� ��� �־��.:12",
                                          "�״� Ŀ�ٶ� �����̸� �ֵθ��� ���ù����� ������ �������� �ٸ� ���� ���� �ص��� �Ⱦ��ؼ� �̿� �������� �Դ´ٰ� �ؿ�.:12",
                                          "������ ������ [�ٸ� ����] �Ƿ��̶�� ����� ��� �� �����ſ���!:9",
                                          "Ī���̶�� �����ҰԿ䡦:7",
                                          "���ݱ��� ������ ������ ���������� �ƹ��� [���ͷ�]�� �̱� �� �������.:8",
                                          "������ �׵��� ������� ����� ���� ������ ������ �� �־����.\n���ʿ��� ������ [���ͷ�]�� ������ ���ص帱�Կ�!:8"
                                          });
        talkData.Add(1020, new string[] { "<����>\n1. [���ͷ�]�� �����̸� �ֵѷ� �����Ѵ�.:8",
                                          "2. �̸� ƨ�ܳ��� ����� �ٷ� [���뿡 �°� �ٸ��� ����]�̴�.:8",
                                          "3. ���� ���� [1000��]�� �޼��ϸ� �¸��Ѵ�.:8",
                                          "�׷��� ����! ������ �Ƿ��� �Ͼ��!:9"
                                          });
        talkData.Add(3000, new string[] { "�ƹ� �ϵ� �Ͼ�� �ʴ´�..." });
        talkData.Add(100, new string[] { "ũ������!!\n�״�� ���Ѱ�? �׷��ٸ� ������!:16" });
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

    public Sprite GetPortrait(int portraitIndex)
    {
        return portraitArr[portraitIndex];
    }
}
