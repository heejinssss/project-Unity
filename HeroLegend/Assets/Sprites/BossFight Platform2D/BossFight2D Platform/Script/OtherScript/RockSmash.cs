using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSmash : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    private PlayerHealth playerHealth;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerHealth = FindObjectOfType(typeof(PlayerHealth)) as PlayerHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Rock") || collision.CompareTag("Enemy"))
        {
            ShakeCamera.Instance.ShakeCameraTwo();
            transform.eulerAngles = new Vector3(0, 0, 0);
            rb.freezeRotation = true;
            anim.SetTrigger("Destroy");
            Destroy(gameObject, 0.65f);
        }
        if (collision.CompareTag("Player"))
        {
            playerHealth.TakeDamage(5);
            ShakeCamera.Instance.ShakeCameraTwo();
            transform.eulerAngles = new Vector3(0, 0, 0);
            rb.freezeRotation = true;
            anim.SetTrigger("Destroy");
            Destroy(gameObject, 0.65f);
        }
    }
}
