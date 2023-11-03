using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerDamaged : MonoBehaviour
{
    public GameManager gameManager;
    SpriteRenderer spriteRenderer;
    Animator anim;
    Rigidbody2D rigid;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Transform playerTransform = this.transform;
        anim = playerTransform.Find("UnitRoot").GetComponent<Animator>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {            
            OnDamaged(collision.transform.position);
        }
    }

    public void OnDie()
    {
        
    }
    public void OnDamaged(Vector2 targetPos)
    {
        // health Down
        gameManager.HealthDown();

        gameObject.layer = 9;

        // view Alpha
       ChangeColor(new Color(1, 0, 0, 1));

        // Reaction Force
        int dirc = ((transform.position.x - targetPos.x) > 0) ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);


        anim.SetTrigger("isStunned");
        Invoke("OffDamaged", 2);
    }


    public void OffDamaged()
    {
        gameObject.layer = 0;
        ChangeColor(new Color(1, 1, 1, 1));
    }

    void ChangeColor(Color newColor)
    {
        SpriteRenderer[] children = GetComponentsInChildren<SpriteRenderer>(); // 모든 SpriteRenderer 컴포넌트를 찾습니다.
        foreach (SpriteRenderer sr in children)
        {
            sr.color = newColor; // 각 SpriteRenderer의 색상을 변경합니다.
        }
    }
}
