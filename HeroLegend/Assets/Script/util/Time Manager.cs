using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/* 공통으로 사용할 타이머 스크립트 */
/*
 * CONFIRM :: 초기화 + 세팅 필수
 */
public class TimeManager : MonoBehaviour
{

    /* 시간 */
    private float sec;
    private int min;

    /* 텍스트 UI */
    private Text text;

    /*
     * CONFIRM :: 타이머 식별자 논의 (게임별 닉네임을 분리하는가, 게임별 랭킹을 분리하는가, 닉네임을 UNIQUE 처리해도 되는가)
     */

    public void Start()
    {
       /*
        * TO DO :: DB에서 현재 playTime 받아와 세팅 (전제: 장면 전환 시마다의 DB 업데이트)
        */

    }

    public void Update()
    {
        sec += Time.deltaTime;
        if (sec >= 60f)
        {
            min++;
            sec = 0;
        }

        text.text = string.Format("{0:D2}:{1:D2}", min, (int)sec);
    }
}