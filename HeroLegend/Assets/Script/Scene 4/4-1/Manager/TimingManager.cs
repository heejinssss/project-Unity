using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{

    // ������ ��Ʈ�� ��� List => ���������� �ִ��� ��� ��Ʈ�� ���ؾ� ��
    public List<GameObject> boxNoteList = new List<GameObject>();

    int[] judgementRecord = new int[5];

    [SerializeField] Transform Center = null;            // ���� ������ �߽��� �˷��ִ� ����
    [SerializeField] RectTransform[] timingRect = null;  // �پ��� ���� ���� (Perfect, Cool, Good, Bad)
    Vector2[] timingBoxs = null;                         // ���� ������ �ּҰ�(x), �ִ밪(y)
                                                         // ���⿡ RectTransform�� ���� �����ؼ� �־��ٰ�

    EffectManager theEffect;
    ScoreManager theScoreManager;

    // Start is called before the first frame update
    void Start()
    {
        theEffect = FindObjectOfType<EffectManager>();
        theScoreManager = FindObjectOfType<ScoreManager>();

        // Ÿ�̹� �ڽ� ����
        timingBoxs = new Vector2[timingRect.Length];
        for (int i = 0; i < timingRect.Length; i++)
        {
            // ������ ���� ������ �ּҰ��� �ִ밪
            // 0��° Perfect�� ���� ������ ���� ����, 3��° Bad�� ���� �аڴ�
            timingBoxs[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2,
                              Center.localPosition.x + timingRect[i].rect.width / 2);
        }
    }

    public void CheckTiming()
    {
        theEffect.SpaceButtonDownEffect();

        for (int i = 0; i < boxNoteList.Count; i++)
        {
            float t_notePosX = boxNoteList[i].transform.localPosition.x;

            for (int x = 0; x < timingBoxs.Length; x++)
            {
                if (timingBoxs[x].x <= t_notePosX && t_notePosX <= timingBoxs[x].y)
                {
                    boxNoteList[i].GetComponent<Note>().HideNote();
                    boxNoteList.RemoveAt(i);

                    // ����Ʈ ����
                    if (x < timingBoxs.Length - 1) // bad ������ �ƴ� ����
                    {
                        theEffect.NoteHitEffect();
                    }
                    judgementRecord[x]++;          // ���� ���
                    theEffect.JudgementEffect(x);  // ���� ����

                    // ���� ����
                    theScoreManager.IncreaseScore(x);

                    return;
                }
            }
        }

        theScoreManager.IncreaseScore(timingBoxs.Length);
        theEffect.JudgementEffect(timingBoxs.Length);
        theEffect.LegBossAttackEffect();
        MissRecord();
    }

    public int[] GetJudgementRecord()
    {
        return judgementRecord;
    }

    public void MissRecord()
    {
        judgementRecord[4]++;   // ���� ���
    }
}
