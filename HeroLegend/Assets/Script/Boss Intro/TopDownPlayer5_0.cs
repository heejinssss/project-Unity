using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayer5_0 : MonoBehaviour
{
    Rigidbody2D rigid;
    public GameManager5_0 gameManager;
    float h;
    float v;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump"))
            gameManager.Action();
    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(h, v);    
    }
}
