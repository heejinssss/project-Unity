using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Noise : MonoBehaviour
{


    public float damage;
    
    Rigidbody2D rigid;
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, Vector3 dir)
    {
        this.damage = damage;
        // rigid.velocity = dir;
        rigid.velocity = dir * 30f;
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;
        
        Debug.Log("명중");
        rigid.velocity = Vector2.zero;
        gameObject.SetActive(false);
        
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        gameObject.SetActive(false);
    }

}

    // public float speed;
    // public float distance;
    // public LayerMask isLayer;

    // void Start()
    // {
    //     // 해당 메소드가 실행될때 꼭 'gameObject'가 파괴되었는지 확인합니다.
    //     Invoke("DestroyNoise", 2f);
    // }

    // void Update()
    // {
    //     // gameObject가 파괴된 경우 해당 스크립트에서 더 이상 아무 작업을 하지 않습니다.
    //     if (gameObject == null)
    //     {
    //         return;
    //     }

    //     RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right, distance, isLayer);

    //     if (ray.collider != null)
    //     {
    //         if (ray.collider.CompareTag("Enemy"))
    //         {
    //             Debug.Log("명중");
    //         }
            
    //         // 메서드 호출을 중단하기 위해 리턴하고 게임 오브젝트를 파괴합니다.
    //         DestroyNoise();
    //         return;
    //     }

        // if (transform.rotation.y == 0)
        // {
        //     transform.Translate(transform.right * speed * Time.deltaTime);
        // }
        // else
        // {
        //     transform.Translate(transform.right * -1 * speed * Time.deltaTime);
        // }
    // }

    // void DestroyNoise()
    // {
    //     // gameObject가 이미 파괴된 경우에는 다시 파괴하지 않습니다.
    //     if (gameObject != null)
    //     {
    //         gameObject.SetActive(false);
    //         Destroy(gameObject);
    //     }
    // }

