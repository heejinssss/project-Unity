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
        talkData.Add(1001, new string[] { "다리떨리아 성에는 무시무시한 완벽 마왕인 고만터러가 살고 있어요.",
                                          "그는 커다란 몽둥이를 휘두르는 무시무시한 괴력을 지녔지만 다리 떠는 것을 극도로 싫어해서 이에 데미지를 입는다고 해요.",
                                          "하지만 용사님의 다리 떨기 실력이라면 충분히 무찌를 수 있을거예요!",

                                          });
        talkData.Add(1002, new string[] { "또 만났네?", "반가워!" });
        talkData.Add(3000, new string[] { "아무 일도 일어나지 않는다..." });
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }
}
