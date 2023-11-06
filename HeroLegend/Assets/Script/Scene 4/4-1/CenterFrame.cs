using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterFrame : MonoBehaviour
{
    AudioSource myAudio;
    NoteManager theNote;
    ScoreManager theScore;
    EffectManager theEffect;
    bool musicStart = false;

    Result theResult;

    private void Start()
    {
        myAudio = GetComponent<AudioSource>();
        theNote = FindObjectOfType<NoteManager>();
        theScore = FindObjectOfType<ScoreManager>();
        theEffect = FindObjectOfType<EffectManager>();
        theResult = FindObjectOfType<Result>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!musicStart)
        {
            if (collision.CompareTag("Note"))
            {
                myAudio.Play();
                musicStart = true;
            }
        } 
        else if (!myAudio.isPlaying) { // 게임을 시작했고, 음악이 끝났으면 게임 종료
            PlayerController.s_canPresskey = false;
            theNote.RemoveNote();
            // theResult.ShowResult();
            
            if (theScore.DoesWin())
            {
                theEffect.LegBossBigHitEffect();
            }

            Invoke("InvokeShowResult", 2f);
        }
    }

    private void InvokeShowResult()
    {
        theResult.ShowResult();
    }
}
