using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TalkManager2 : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr;

    void Awake() {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Intro.02");
        }
    }

    void GenerateData()
    {
        talkData.Add(1000, new string[] {
            "�ķ�� ������ �ķ�� ������ �޴޴� �ķ�� ������:1",
            "����¥ ¥������ ���� �� �԰ڳ�!:11",
            "�� ���� �� �״ϱ� �� ���ϰ� �����Ÿ��鼭 �Ծ�.:15",
            "��? ��! ��� ��!:2",
            "... ��� �Ʊ��� �̰� �� ����� ����.:5",
            "�ķ�� ������:1",
            "�ܢ��������� ���ؾ߸� ���� �Գ��� �߸��ص� �������� �� �߸Ծ��ܢ�",
            "��¥ ���ס�:5",
            "�ϱ䡦 ���� �� �� �Ծ��µ� ���� Ƣ������ ȭ ���� ������.:5",
            "(����ģ������ ī���� ������)",
            "[�߽� �޽���] ��ģ��. �Ʊ�� �̾��߾�. ���� �ٸ� ���� �����Ÿ��ϱ� ���� �� �Դ�'��' �Ű� ������?:5",
            "[�߽� �޽���] �Ʊ�� �ʰ� ���ڱ� ȭ���� �� <����>�����µ� <������> �����غ��� �� �߸��� ū �� ����.:5",
            "[�߽� �޽���] ���� <���> �ϸ� �� ȭ�� Ǯ���� �𸣰ڳ�. ���� ��ٸ���.:1",
            "[���� �޽���] ��¥ ��������ε� �� �������� �ϳ�. �츮 �����.:10",
            "[���� �޽���] �ʰ� �ٸ����� �͵� �Ȱ�. ���������� �̻��ϰ� ������� �͵� �Ⱦ�.:10",
            "[���� �޽���] ������ ���Դ� �ɷ� ���� ���°͵� ������ ���Ĺ��� �� ����.:15",
            "[���� �޽���] �ٽô� ������ ����.:15",
            "��??? ��������?? �ô밡 ��� �ô��ε� �������� Ÿ���̾�?!:2",
            "�� ���������� �ڱⰡ ���غ��� �͵� �ƴϰ�? ���� ��¥!:2",
            "[�߽� �޽���] ��!! �� <��������>�� ������ ���� �ִ� �� �־�??:4"
        });

        //talkData.Add(100, new string[] { "����� ��Ź�̴�." });
        //talkData.Add(200, new string[] { "����� ��Ź�̴�." });

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
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

    public Sprite GetPortrait(int id, int portraitIndex) {
        return portraitData[id + portraitIndex];
    }
}
