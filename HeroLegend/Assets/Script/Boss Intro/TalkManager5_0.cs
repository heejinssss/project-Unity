using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager5_0 : MonoBehaviour
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
                "드디어 다섯 마왕을 모두 무찔렀군.:1",
                "그런데 공주, 지도 중앙에 있는 해골은 뭐죠?:7",
                "그곳이 이세계의 최강자인 파르펙토 마왕이 있는 곳이에요.:14",
                "지금까지 수많은 용사들이 마왕을 무찌르기 위해 도전하였으나 멀쩡하게 살아돌아온 자는 없었어요.:13",
                "운이 좋게 살아서 돌아오더라도, \n좌우대칭에 집착하게 된 용사님도 계셨고, \n말 끝에 마침표를 붙이지 않으면 말을 끝낼 수 없는 저주에 걸린 용사님도 계셨어요.:13",
                "아무래도 세계관 최강이다 보니 쉽지 않겠네요.:6",
                "대신에, 그동안의 제보를 통해 들은 사실이 하나 있어요.:8",
                "그게 뭐죠?:6",
                "파르펙토 마왕은 5대륙의 마왕들이 가진 필살기를 모두 구사할 수 있다고 해요.:9",
                "지금까지 용사님께서 처치했던 마왕과의 싸움을 상기해본다면 \n파르펙토 마왕과의 싸움에 큰 도움이 될 것 같아요.:9",
                "그렇군요.:1",
                "(잠시 생각하면서 다리를 떤다):default",
                "단, 주의할 점이 있어요.:14",
                "파르펙토 마왕은 그만의 고유한 필살기가 있다고 하는데, \n그것이 무엇인지는 아직 아무도 확인하지 못했다고 해요.:14",
                "그 점만 유의한다면… 용사님이 이 세계를 구하실 수 있을 거라고 생각해요.:9",
                "알겠어요. \n꼭 마왕을 무찔러서 이세계를 예전처럼 돌려놓을 수 있도록 할게요.:0",
                "보스존은 위험하니 저 혼자 다녀올게요. 공주 걱정말고 기다려요.:1",
                "(울먹) 네! 믿어요 용사님!:9"
        });
        talkData.Add(
            1,
            new string[] {
                "test:default"
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
