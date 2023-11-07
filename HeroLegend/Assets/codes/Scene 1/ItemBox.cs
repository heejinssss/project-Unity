using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    bool isLive;
    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    // SpriteRenderer spriter;
    WaitForFixedUpdate wait;
    public Transform player;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        // spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }
    void LateUpdate()
    {
        if (isLive)
            return;
        // Vector3 distance_box = player.position - transform.position;
        // if (!isLive && distance_box[0] > 18)
        // {   
        //     isLive = true;
        //     // transform.gameObject.SetActive(true);
        // }
        
        // Debug.Log(distance_box);


        // if (!GameManager.instance.isLive)
        //     return;
        if (!isLive)
        {
            Vector3 distance_box = player.position - transform.position;
            
            if (!isLive && distance_box[0] > 18)
            {   
            isLive = true;
            transform.gameObject.SetActive(true);
            }
        } 
            return;
        
    }

    void OnEnable()
    {
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        // spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
        health = maxHealth;
    }

    // public void Init(SpawnData data)
    // {
    //     anim.runtimeAnimatorController = animCon[data.spriteType];
    //     maxHealth = data.health;
    //     health = data.health;
    // }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Noise") || !isLive)
            return;
        
        health -= collision.GetComponent<Noise>().damage;
        // StartCoroutine(KnockBack());

        if (health > 0) {

        }
        else {
            Dead();
        }

    }
    
    void Dead()
    {
        gameObject.SetActive(false);

    }

}

