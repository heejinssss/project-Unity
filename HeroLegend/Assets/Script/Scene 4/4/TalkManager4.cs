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
        talkData.Add(1010, new string[] { "다리떨리아 성에는 무시무시한 완벽 마왕인 [고만터러]가 살고 있어요.:12",
                                          "그는 커다란 몽둥이를 휘두르는 무시무시한 괴력을 지녔지만 다리 떠는 것을 극도로 싫어해서 이에 데미지를 입는다고 해요.:12",
                                          "하지만 용사님의 [다리 떨기] 실력이라면 충분히 무찌를 수 있을거예요!:9",
                                          "칭찬이라고 생각할게요…:7",
                                          "지금까지 수많은 용사들이 도전했지만 아무도 [고만터러]를 이길 순 없었어요.:8",
                                          "하지만 그들의 희생으로 저희는 많은 정보를 수집할 수 있었어요.\n안쪽에서 수집한 [고만터러]의 정보를 전해드릴게요!:8"
                                          });
        talkData.Add(1020, new string[] { "<설명서>\n1. [고만터러]는 몽둥이를 휘둘러 공격한다.:8",
                                          "2. 이를 튕겨내는 방법은 바로 [리듬에 맞게 다리를 떨기]이다.:8",
                                          "3. 종합 점수 [1000점]을 달성하면 승리한다.:8",
                                          "그러면 용사님! 용사님의 실력을 믿어요!:9"
                                          });
        talkData.Add(3000, new string[] { "아무 일도 일어나지 않는다..." });
        talkData.Add(100, new string[] { "크하하하!!\n그대는 강한가? 그렇다면 덤벼라!:16" });
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
