using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene3_Boss : MonoBehaviour
{
    public GameManager3 gameManager;
    Rigidbody2D rigid;
    Animator animator;
    // Sounder sound;
    public GameObject[] objects;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        // sound = GetComponent<Sounder>();

        Think();

        Invoke("Think", 5);
    }


    void Think()
    {
        if (!GameManager3.isLive)
            return;

        int nextAnim = Random.Range(1, 4);
        ChangeAnim(nextAnim);

        switch (nextAnim)
        {
            case 1:
                // 1. Cleave
                Debug.Log("Cleave");
                Invoke("ChangeToIdle", 1.5f);
                ChangeActive(0);
                break;
            case 2:
                // 2. Smash
                Debug.Log("Smash");
                Invoke("ChangeToIdle", 2);
                ChangeActiveAll(2);
                break;
            case 3:
                // 3. Breath
                Debug.Log("Breath");
                Invoke("ChangeToIdle", 2);
                ChangeActive(1);
                break;
            default:
                break;
        }

        int turn = Random.Range(3, 6);
        Invoke("Think", turn);
    }

     void ChangeAnim(int state)
    {
        animator.SetInteger("State", state);
    }

    void ChangeToIdle()
    {
        Debug.Log("ChangeToIdle");
        animator.SetInteger("State", 0);
    }
    void ChangeActive(int idx)
    {
        objects[idx].gameObject.SetActive(true);
    }

    void ChangeActiveAll(int idx)
    {
        objects[idx].gameObject.SetActive(true);
        for (int index = 0; index < 4; index++)
        {
            objects[idx].transform.GetChild(index).gameObject.SetActive(true);
        }
    }
}

