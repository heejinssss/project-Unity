using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDamaged : MonoBehaviour
{
    public GameManager gameManager;
    SpriteRenderer spriteRenderer;
    Animator anim;
    Rigidbody2D rigid;
    bool isStunned;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Transform playerTransform = this.transform;
        anim = playerTransform.Find("UnitRoot").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            // enemy skill �ǰ� ȿ��
            if (collision.gameObject.CompareTag("EnemySkill")) collision.gameObject.GetComponent<FireballRemove>().Hit();

            if (!isStunned) OnDamaged(collision.transform.position); // ���� ���� �ƴҽ� �ǰ� 
        }
    }

    public void OnDie()
    {
        anim.SetBool("isDead", true);
    }
    public void OnDamaged(Vector2 targetPos)
    {
        // ü�� ����
        gameManager.HealthDown();

        // �ǰ� �� ������ 
        ChangeColor(new Color(1, 0, 0, 1));

        // ����
        isStunned = true;

        // �ǰ� �� �ݵ�
        int dirc = ((transform.position.x - targetPos.x) > 0) ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);

        // 2���� ���� �� �ǰ� ���� ���� 
        Invoke("OffDamaged", 2);
    }


    public void OffDamaged()
    {
        gameObject.layer = 0;
        ChangeColor(new Color(1, 1, 1, 1));
        isStunned = false;
    }

    void ChangeColor(Color newColor)
    {
        // ��� SpriteRenderer ������Ʈ ã��
        SpriteRenderer[] children = GetComponentsInChildren<SpriteRenderer>(); 
        foreach (SpriteRenderer sr in children)
        {
            sr.color = newColor; // �� SpriteRenderer ���� ����
        }
    }
}
