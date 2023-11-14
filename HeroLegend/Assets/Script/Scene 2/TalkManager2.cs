using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager2 : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateDate();
    }

    // Update is called once per frame
    void GenerateDate()
    {
        talkData.Add(1, new string[] { "이곳인가...", 
            "여기가 맞는 것 같은데...", 
            "어렸을 때 방문했던 모습과는 너무 달라요.. 강이 흐르고 풍족한 지역이었는데...", 
            "지거국은 1000년 역사의 찍어먹는 탕수육 요리가 유명한 나라였어요. 전세계 사람들이 찍어먹는 탕수육을 먹기 위해 올 정도로요! 하지만...", 
            "전에 간단하게 들었지만... 5마왕 중 찍어먹는 탕수육을 혐오하는 마왕 \"부머기라스\"가 그의 마법 군대와 함께 지거국을 침공했어요. 그 이후로 한번도 방문해본적이 없었는데.. \n이렇게 달라졌을 줄이야.. 흑흑 ㅜㅜㅠ", 
            "흑흑흑", "흑", "흐긓긓그", "흐긓ㄱ", 
            "용사님 이곳은 어렸을 적 저의 추억이 많은 왕국이에요. 꼭 이곳을 구해주세요.. ㅜㅜ. 마왕 설명서는 이곳을 잘 찾아보면 나올 거에요. 설명서를 찾고 저 포탈을 타고 가면 마왕에게 갈 수 있어요..", 
            "울고 있을 때가 아니다. 내가 물리쳐 줄게!" });
        talkData.Add(2, new string[] {"조각상이다. 지거국의 여신상일까.."});
        talkData.Add(3, new string[] { "마왕에게 당한 사람의 해골인 것 같다." });
        talkData.Add(4, new string[] { "마왕에게 당한 사람의 해골인 것 같다." });
        talkData.Add(5, new string[] { "마왕에게 당한 사람의 해골인 것 같다." });
        talkData.Add(6, new string[] { "용사의 공략집인 것 같다. 더 읽어보자", 
            "왕국을 정복한 부머기라스는 찍먹충들을 혐오한다. 그래서 전국민이 탕수육을 찍어먹는 지거국을 침공한 것이다. 탕수육을 부어먹으려하면 호들갑을 떠는 지거국 사람들의 인간성은 유명하지만 이건아니다.. 언젠가 나타날 \"운명의 용사\"를 위해 이 공략집을 남긴다.",
            "1. 부하들은 모두 나중에 소스를 부어먹기 위해 소스를 붓지 않은 탕수육을 들고 있다. 불(Z)을 2번 맞추면 부하가 쓰려져 부어먹으려고 가지고있던 탕수육을 떨어뜨린다. 가까이 가서 탕수육을 던지고 맞는 부하들은 아직 소스를 붓지않은 탕수육을 맞고 스트레스를 받아 쓰러진다.",
            "2. 기력이 부족할 때 부하에게 불(Z), 기름(X)을 한번씩 맞추면 튀겨져서 맛있는 치킨이 되고 이걸 먹으면 기력을 보충할 수 있다.",
            "3. 마왕의 가죽은 두꺼워서 불(Z)이 통하지 않는다. 붓지 않은 탕수육을 던져 스트레스를 받게해 쓰러뜨려야 한다.",
            "부록. 불(Z) 기름(X), 점프(Space Bar)"});
        talkData.Add(7, new string[] { "드래곤의 두개골이다. 마왕과 부하들은 드래곤인걸까.." });
        talkData.Add(8, new string[] { "마왕에게 갈 수 있는 포탈이다." });
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
        {
            return null;
        } else
        {
            return talkData[id][talkIndex];
        }
    }
}
