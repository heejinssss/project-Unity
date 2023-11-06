using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;
    bool isLive;
    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    // SpriteRenderer spriter;
    WaitForFixedUpdate wait;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        // spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }


    void FixedUpdate()
    {
        // if (!GameManager.instance.isLive)
        //     return;
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) 
            return;
    

    }

    void LateUpdate()
    {
        // if (!GameManager.instance.isLive)
        //     return;
        if (!isLive) 
            return;
    
    }

    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
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
        
        // health -= collision.GetComponent<Noise>().damage;
        // StartCoroutine(KnockBack());

        if (health > 0) {

        }
        else {
            Dead();
        }

    }

    // IEnumerator KnockBack()
    // {
    //     yield return wait;
    //     Vector3 playerPos = GameManager.instance.player.transform.position;        
    //     Vector3 dirVec = transform.position - playerPos;
    //     rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    // }

    void Dead()
    {
        gameObject.SetActive(false);

    }
}
