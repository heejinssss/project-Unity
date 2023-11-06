using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TalkManager1 : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr;
    public CameraShake1 cameraShake;

    void Awake() {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(1000, new string[] {
            "�ķ�� ������ �ķ�� ������ (�޴޴�) �ķ�� ������:1",
            "��:13",
            "�ķ�� ������ �ķ�� ������ (�޴޴�) �ķ�� ������:1",
            "�ڱ��.:10",
            "��? ��? �ķ�� ������:1",
            "�� ������ ���� �� ����?:10",
            "�ڱⰡ ��ġ�� �ϴϱ� ������ ������ �� Ƣ�ݾ�.:10",
            "��~ ��ġ�� �̷��� �Ծ�� �� ���ִ� ��. (�޴޴�):2",
            "���������� �����̰�:15",
            "�ٸ��� �� �� �̷��� ���� �ž�? ���� �� �� �˾Ҿ�.:15",
            "...�˾Ҿ�. �����Ұ�.:5",
            "�Ͽ�ư... ��, ������ ���Դ�. (�ҽ��� ������ �Ѵ�):9",
            "�߾�!!!! ������!!!!:2",
            "�������� ������� �� �������� ��?? ��¥ ū�ϳ� ���߳�.:4",
            "��:12"
        });

        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[2]);
        portraitData.Add(1000 + 3, portraitArr[3]);
        portraitData.Add(1000 + 4, portraitArr[4]);
        portraitData.Add(1000 + 5, portraitArr[5]);
        portraitData.Add(1000 + 6, portraitArr[6]);
        portraitData.Add(1000 + 7, portraitArr[7]);
        portraitData.Add(1000 + 8, portraitArr[8]);
        portraitData.Add(1000 + 9, portraitArr[9]);
        portraitData.Add(1000 + 10, portraitArr[10]);
        portraitData.Add(1000 + 11, portraitArr[11]);
        portraitData.Add(1000 + 12, portraitArr[12]);
        portraitData.Add(1000 + 13, portraitArr[13]);
        portraitData.Add(1000 + 14, portraitArr[14]);
        portraitData.Add(1000 + 15, portraitArr[15]);
    }

    public string GetTalk(int id, int talkIndex) {

        if (talkIndex == 0 || talkIndex == 2 || talkIndex == 7) // "�޴޴�" ��ũ��Ʈ�� ���� ������ ȭ�� ����
        {
            cameraShake.shakeDuration = 1.0f;
        }

        if (talkIndex == talkData[id].Length) {
            SceneManager.LoadScene("Intro.02");
            return null;
        }
        else
            return talkData[id][talkIndex];
    }

    public Sprite GetPortrait(int id, int portraitIndex) {
        return portraitData[id + portraitIndex];
    }
}
