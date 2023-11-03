using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRemove : MonoBehaviour
{
    private Rigidbody2D rb; // ��ų�� Rigidbody2D ������Ʈ
    public GameObject effect;
    public GameObject deActiveEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.velocity = Vector3.zero;
            Hit(deActiveEffect);
        }
    }

    // ��ų ������ ��
    public void Hit(GameObject e)
    {
        Remove();
        GameObject Effect = Instantiate(e, transform.position, transform.rotation);
    }

    // ������ ��ų ��ȿȭ
    public void DeActive()
    {
        Hit(deActiveEffect);
    }

    // ��ų �¾��� ��
    public void Active()
    {
        Hit(effect);
    }

    public void Remove()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }


}
