using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public int count;
    public float speedRate;

    void Start()
    {
        count = transform.childCount;
    }

    void Update()
    {
        if (!GameManager.isLive)
            return;

        float totalSpeed = GameManager.globalSpeed * speedRate * Time.deltaTime * -1f;
        transform.Translate(totalSpeed, 0, 0); // Time.deltaTime ������ �� �Һ� �ð� -> �ð� ���� ����
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Land")
        {
            gameObject.SetActive(false);
        }
    }
}
