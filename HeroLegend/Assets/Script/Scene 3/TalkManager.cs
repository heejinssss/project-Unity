using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;
    public Sprite[] portraitArr;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        // n0 ~ n9 : #n  
        talkData.Add(
            0, 
            new string[] {
                "blackStart:desc",
                "���� ���踦 ���ϴ� �͵� �߿������� �䵵 ì�ܵ�ž���! ��ħ �տ� ������� �ֳ׿�!:9",
                "blackEnd:desc",
                "�ķ��� ����¥��¬¬:2",
                "�����ķ�� �򸣸� ¬¬:2",
                "...:8",
                "�ֱ׷���? (���� ���� ������):6",
                "�ƴ� ���ԡ� �ס�:13",
                "blackShockStart:desc",
                "blackShockEnd:desc",
                "moveBoss:desc",
                "\"�ڳ�!!! ����ü.. �׷� ��ġ�� ����� ��� ��� ���ΰ���??\":default",
                "\"�� �Ű��� �̷��� �Ž����� �ϴٴ�!!! ������ �������� ���� ���ΰ�?\":default",
                "��? ���׼��ߡ� (���� ���� ������):6",
                "�� �� ġ�¡� �ĸ����ĸ� ���� �ĸ���ŷ ???:14",
                "��?? ���� ������ ���� �Ĵ翡�� ���� �Ա⵵ �ؿ�???:2",
                "\"�� �ɾ���� �ο��� ���� ������ ����!!! ���� ���� ������!\":default",
                "��?? �װ� ������:2",
                "ruleStart:desc",
                "�ĸ����ĸ��� �ĸ���ŷ�� �� �丮�� ���� �� 0.1 �ʿ� �� ���� ���� �Դ� �����̿���.:14",
                "���ݱ��� �� � ��絵 �ĸ���ŷ�� �̰ܳ� �� �������..:13",
                "���� ���� ������ ���� �����ϸ鼭 �ֺ��� ���Ĺ��� Ƣ���, ���� �緹�� �鷯���� ������ ġ�彺����� ��������� �Դ� ������ ���� ���� ���߱� �������ҡ�!!!:12",
                "���� ���Ͻóס�:3",
                "������ �����̶�� �ĸ���ŷ�� ����ĥ �� �����ſ���!\n���� ������ �帱�Կ�!:9",
                "ruleMid:desc",
                "ruleEnd:desc",
                "blackBossStart:desc",
                "������ �ο��� �Ŵ� �̴� �������̱�... ����ϰھ�:16",
                "��� �ѹ� ����ͺ��ð�!!!:16",
                "blackBossEnd:desc"
                // 1��° �������� ���� UI
        });
        talkData.Add(
            2,
            new string[] {
                "���� �̰� ����...:17",
                "���� ��ġ�⸦ �� �� �ƴ� �༮�̱�...:17",
                "(����):3",
                "��...! ������ ���� ���� �� ����ġ ���� ���̾�!!!:16"
                // 2��° �������� ���� UI
        });
        talkData.Add(
            4,
            new string[] {
                "���ƾƾ�!!!!:17",
                "���̻�.. ��ġ��� Ƥ ������ ������ �� ���� ������������ ������ �� �� ����!!!!!!!!!:16",
                "slimeChangeStart:desc",
                "�������� �� ������� ���������:18",
                "slimeChangeEnd:desc"
                // ���� �������� ���� UI
        });
        talkData.Add(
            6,
            new string[] {
                "skip:desc",
                "bossEndStart:desc",
                "\"�� ���� �����߸��ٴ�...\":default",
                "\"�ʸ� �ְ��� ��ġ�ⷯ�� �����ϰڴ�...\":default",
                "\"�׷��� ��... �������!!!\":default",
                "bossEndEnd:desc",
                "���� ����!!! �����̶�� �س��� �� �˾Ҿ��!!!:9",
                "������! ���� �׷���?:1",
                "���� �� ��ġ�� �ϰŵ��... ��������ĸ�...:1",
                "�׷� � ���⼭ ������? ���...:9"
                // �������� Ŭ���� UI
        });

        // Knight
        portraitData.Add(0, portraitArr[0]);
        portraitData.Add(1, portraitArr[1]);
        portraitData.Add(2, portraitArr[2]);
        portraitData.Add(3, portraitArr[3]);
        portraitData.Add(4, portraitArr[4]);
        portraitData.Add(5, portraitArr[5]);
        portraitData.Add(6, portraitArr[6]);
        portraitData.Add(7, portraitArr[7]);

        // Princess
        portraitData.Add(8, portraitArr[8]);
        portraitData.Add(9, portraitArr[9]);
        portraitData.Add(10, portraitArr[10]);
        portraitData.Add(11, portraitArr[11]);
        portraitData.Add(12, portraitArr[12]);
        portraitData.Add(13, portraitArr[13]);
        portraitData.Add(14, portraitArr[14]);
        portraitData.Add(15, portraitArr[15]);

        // Boss
        portraitData.Add(16, portraitArr[16]);
        portraitData.Add(17, portraitArr[17]);
        portraitData.Add(18, portraitArr[18]);

    }

    public string GetTalk(int idx, int talkIdx)
    {
        if (talkIdx == talkData[idx].Length)
            return null;
        else
            return talkData[idx][talkIdx];
    }

    public Sprite GetPortrait(int portraitIndex)
    {
        return portraitData[portraitIndex];
    }
}
