using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    // void LateUpdate()
    // {
    //     if (transform.position.x > -15)
    //         return;
        
    //     transform.Translate(35, 0, 0, Space.Self);
    // }
    Collider2D coll;
    void Awake() 
    {
        coll = GetComponent<Collider2D>();
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;


        switch (transform.tag) {
            case "Ground":
                float diffX = playerPos.x - myPos.x;
                
                float dirX = diffX < 0 ? -1 : 1;
                diffX = Math.Abs(diffX);
                
                if (dirX == 1 && diffX > 20) {
                    transform.Translate(Vector3.right * dirX * 80);
                }
                if (dirX == -1 && diffX > 20) {
                    transform.Translate(Vector3.right * dirX * 80);

                }
                break;

        }
    }
}