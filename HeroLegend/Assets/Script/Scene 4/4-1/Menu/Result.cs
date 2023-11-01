using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    // ��ҿ��� ��Ȱ��ȭ �س���, �ʿ��� �� Ȱ��ȭ��ų��
    [SerializeField] GameObject goUI = null;

    [SerializeField] TextMeshProUGUI[] txtCount = null;
    [SerializeField] TextMeshProUGUI txtScore = null;
    [SerializeField] TextMeshProUGUI txtMaxCombo = null;
    [SerializeField] TextMeshProUGUI txtResult = null;

    [SerializeField] AudioClip[] resultAudioClips = null;

    public int clearPoint = 1000;

    ScoreManager theScore;
    ComboManager theCombo;
    TimingManager theTiming;
    AudioSource resultAudio;

    private void Start()
    {
        theScore = FindObjectOfType<ScoreManager>();
        theCombo = FindObjectOfType<ComboManager>();
        theTiming = FindObjectOfType<TimingManager>();
        resultAudio = GetComponent<AudioSource>();
    }

    public void ShowResult()
    {
        goUI.SetActive(true);

        int[] t_judgement = theTiming.GetJudgementRecord();
        int t_currentScore = theScore.GetCurrentScore();
        int t_maxCombo = theCombo.GetMaxCombo();

        for (int i = 0; i < txtCount.Length; i++)
        {
            txtCount[i].text = string.Format("{0:#,##0}", t_judgement[i]);
                
        }

        txtScore.text = string.Format("{0:#,##0}", t_currentScore);
        txtMaxCombo.text = string.Format("{0:#,##0}", t_maxCombo);


        if (t_currentScore < clearPoint) 
        {
            // ���� ��
            txtResult.text = "<#FF0000>Fail..</color>";

            resultAudio.clip = resultAudioClips[1];
            resultAudio.Play();
        }
        else
        {
            // ���� ��
            txtResult.text = "<#00FF00>Clear!</color>";

            resultAudio.clip = resultAudioClips[0];
            resultAudio.Play();
        }
    }

    public void ReturnToTopDownWorld()
    {
        int t_currentScore = theScore.GetCurrentScore();

        // ������ ������ ����
        PlayerPrefs.SetInt("isReturnToTopDownWorld", 1);

        if (t_currentScore < clearPoint)
        {
            // �������� ��
            PlayerPrefs.SetInt("isClear", 0);
        }
        else
        {
            // �¸����� ��
            PlayerPrefs.SetInt("isClear", 1);
        }

        SceneManager.LoadScene("Scene 4");
    }
}
