using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformIgnore : MonoBehaviour
{
    public Collider2D platformCollider;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.GetComponent<Collider2D>(), platformCollider, true);
            Debug.Log("111111");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.GetComponent<Collider2D>(), platformCollider, false);
            Debug.Log("222222");
        }
    }
}
