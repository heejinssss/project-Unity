using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capsuleCollider;
    public GameObject chicken;
    public GameObject food;
    public GameObject fireBall;
    public GameObject aura;
    public GameManager gameManager;

    public int nextMove; // ���� �ӵ� 
    public int launchSpeed; // ���̾ �߻� �ӵ� 
    int fired; // �� ���� Ƚ��
    int oiled; // �⸧ ���� Ƚ�� 
    bool dead;

    // Start is called before the first frame update
    void Awake()
    {
        CreateAura();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        fired = 0;
        oiled = 0;

        // ���� �Ŵ��� ã��
        if (gameManager == null)
        {
            // Find the GameManager in the scene and assign it
            gameManager = FindObjectOfType<GameManager>();
            if (gameManager == null)
            {
                Debug.LogError("GameManager not found in the scene.");
            }
        }

        Invoke("Think", 5);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!dead)
        {
            // Move
            rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

            // Platform check
            Vector2 vec = new Vector2(rigid.position.x + rigid.velocity.normalized.x / 2, rigid.position.y - 0.5f);
            Vector2 direction = spriteRenderer.flipX ? Vector2.right : Vector2.left; // ĳ���Ͱ� ������ �ٶ󺸸� ��������, �������� �ٶ󺸸� ���������� ����ĳ��Ʈ
            RaycastHit2D rayhitForward = Physics2D.Raycast(vec, direction, 1, LayerMask.GetMask("PlatForm"));
            Debug.DrawLine(vec, vec + direction * 1, Color.red);

            if (rayhitForward.collider != null)
            {
                Turn();
            }

            if (rigid.velocity.normalized.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (rigid.velocity.normalized.x > 0)
            {
                spriteRenderer.flipX = false;
            }
        }
    }
    void CreateProjectile()
    {
        int v = (spriteRenderer.flipX) ? -1 : 1;

        // ������Ÿ���� ���� ��ġ�� �÷��̾� ������ ���� �ű�
        Vector3 startPosition = transform.position + v * transform.right * 0.7f + transform.up * 0.1f;

        // �������κ��� ���ο� ������Ÿ�� ������Ʈ ����
        GameObject projectile = Instantiate(fireBall, startPosition, Quaternion.Euler(0, v == 1 ? 0 : 180, 0));

        // ������Ÿ�Ϸκ��� ������ٵ� 2D ������Ʈ ������
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        // ������Ÿ�� �߻�
        rb.AddForce(v * transform.right * launchSpeed, ForceMode2D.Impulse);

    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);
        // sorite animation
        if (nextMove == 0)
        {
            anim.SetBool("isFlying", false);
            StartCoroutine(ShootFireBall());
        } else
        {
            anim.SetBool("isFlying", true);
        }
        

        // ���⿡ ���� ������
        if (nextMove != 0)
        {
            spriteRenderer.flipX = nextMove < 0;
        }

        // ���
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }
    void Turn()
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == 1;

        CancelInvoke();
        Invoke("Think", 2);
    }

    void DeActive()
    {
        // ������Ʈ ��Ȱ��ȭ �� ����
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    void OnDamaged(Color color)
    {
        // ������Ʈ ������
        spriteRenderer.color = color;

        // �巡�� ��� 
        if (fired >= 2 || (oiled >= 1 && fired >= 1))
        {
            
            if (fired >= 1 && oiled >= 1)
            {
                TurnChicken();
            }
            else if (fired >= 2)
            {
                OnDie();
                Instantiate(food, transform.position - transform.up * 0.3f, transform.rotation);
            }
        }
    }

    void OnDie()
    {
        dead = true;
        anim.SetBool("isDead", true);
        capsuleCollider.enabled = false;
        rigid.velocity = Vector2.zero;
        rigid.AddForce(Vector2.up * 7, ForceMode2D.Impulse);
        rigid.gravityScale = 1;
        Invoke("DeActive", 2);
        if(gameManager.stageIndex < gameManager.Stages.Length - 1) gameManager.EnemyCountDown();
    }

    void TurnChicken()
    {
        dead = true;
        Instantiate(chicken, transform.position - transform.up * 0.3f, transform.rotation);
        DeActive();
        gameManager.EnemyCountDown();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "PlayerSkill")
        {
            if (collision.gameObject.CompareTag("PlayerSkillFire"))
            {
                fired++;
                OnDamaged(new Color(255, 0, 0, 255));
            }
            else if (collision.gameObject.CompareTag("PlayerSkillOil"))
            {
                oiled++;
                OnDamaged(new Color(255, 255, 0, 255));
            }
            SkillRemove skillRemove = collision.gameObject.GetComponent<SkillRemove>();
            skillRemove.Active();
        } else if (LayerMask.LayerToName(collision.gameObject.layer) == "PlayerAttack" && collision.gameObject.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
        {
            OnDie();
        }
        
    }

    IEnumerator ShootFireBall()
    {
        yield return new WaitForSeconds(1); 
        anim.SetTrigger("isAttacking");
        Invoke("CreateProjectile", 0.8f);
        yield return new WaitForSeconds(1);         
    }


    IEnumerator FadeOutSprite(GameObject objectToFade)
    {
        SpriteRenderer spriteRenderer = objectToFade.GetComponent<SpriteRenderer>();
        Color fadeColor = spriteRenderer.color;

        // 1�� ���� ������ ������� ��
        for (float i = 2; i >= 0; i -= Time.deltaTime)
        {
            // �� ���� �� ����
            fadeColor.a = i;
            spriteRenderer.color = fadeColor;
            yield return null;
        }

        Destroy(objectToFade);
    }

    // �ƿ�� ���� �� ������� �ϴ� �Լ�
    void CreateAura()
    {
        GameObject aura1 = Instantiate(aura, transform.position + Vector3.down * 0.2f, transform.rotation);
        StartCoroutine(FadeOutSprite(aura1));
    }
}
