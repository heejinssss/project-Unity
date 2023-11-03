using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class FireballRemove : MonoBehaviour
{
    private Rigidbody2D rb; // ��ų�� Rigidbody2D ������Ʈ
    public GameObject effect;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (transform.position.x <= -10 || transform.position.x > 10)
        {
            gameObject.SetActive(false); // �� ���� ������Ʈ�� �ı�
            Destroy(gameObject);
        }
    }

    // ��ų ������ ��
    public void Hit(GameObject e)
    {
        Remove();
        GameObject Effect = Instantiate(e, transform.position, transform.rotation);
    }

    public void Remove()
    {
        gameObject.SetActive(false) ;
        Destroy(gameObject);
    }


    // ��ų �¾��� ��
    public void Active()
    {
        Hit(effect);
    }
}
