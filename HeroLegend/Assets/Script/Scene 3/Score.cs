using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    Text uiText;

    void Start()
    {
        uiText = GetComponent<Text>();

    }

    void LateUpdate()
    {
        if (!GameManager.isLive)
            return;

        uiText.text = GameManager.score.ToString("F0");
    }
}
