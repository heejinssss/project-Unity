using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    // �д� ��Ʈ ��(�д� ��Ʈ ��)
    public int bpm = 0;

    // ���� ���ӿ����� ������ ����� �ϹǷ� float ��� double
    double currentTime = 0d;

    AudioSource startCountdownAudio;

    // bool noteActive = true;
    bool noteActive = false;

    [SerializeField] Transform tfNoteAppear = null;

    TimingManager theTimingManager;
    EffectManager theEffectManager;
    ScoreManager theScoreManager;

    private void Start()
    {
        theEffectManager = FindObjectOfType<EffectManager>();
        theTimingManager = GetComponent<TimingManager>();
        theScoreManager = FindObjectOfType<ScoreManager>();

        startCountdownAudio = GetComponent<AudioSource>();
        startCountdownAudio.Play();

        Invoke("activateNote", 4f);
    }

    void activateNote()
    {
        noteActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (noteActive)
        {
            currentTime += Time.deltaTime;

            // 60(double) / bpm => 1��Ʈ �� �ð�
            if (currentTime >= 30d / bpm)
            {
                if (Random.value < 0.4)
                {
                    GameObject t_note = ObjectPool.instance.noteQueue.Dequeue();
                    t_note.transform.position = tfNoteAppear.position;
                    t_note.SetActive(true);
                    t_note.transform.localScale = new Vector3(1, 1, 1);
                    theTimingManager.boxNoteList.Add(t_note);
                }

                // ������ ������ ������Ʈ �ϱ⿡ 0.51005xx �� �̷� ������ ������ ����
                // �ٵ� �̸� �׳� 0���� �ʱ�ȭ �ع����� �� ������ �����Ǽ� ���߿��� ���� �ȸ°� �׷� �� �ִ�
                // �׷��� �װͱ��� ����ؼ� ���� ��Ʈ�� ���� ��ŭ �� ���� ������ ������ ����
                currentTime -= 30d / bpm;
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            if (collision.GetComponent<Note>().GetNoteFlag())
            {
                theTimingManager.MissRecord();
                theEffectManager.JudgementEffect(4);
                theEffectManager.LegBossAttackEffect();
                theScoreManager.IncreaseScore(4);
            }
            theTimingManager.boxNoteList.Remove(collision.gameObject);

            ObjectPool.instance.noteQueue.Enqueue(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
    }

    public void RemoveNote()
    {
        noteActive = false;

        for (int i = 0; i < theTimingManager.boxNoteList.Count; i++)
        {
            theTimingManager.boxNoteList[i].SetActive(false);
            ObjectPool.instance.noteQueue.Enqueue(theTimingManager.boxNoteList[i]);
        }
    }
}
