using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller3 : MonoBehaviour
{
    public int count;
    public float speedRate;

    void Start()
    {
        count = transform.childCount;
    }

    void Update()
    {
        if (!GameManager3.isLive)
            return;

        float totalSpeed = GameManager3.globalSpeed * speedRate * Time.deltaTime * -1f;
        transform.Translate(totalSpeed, 0, 0); // Time.deltaTime 프레임 별 소비 시간 -> 시간 단위 통일
    }
}
