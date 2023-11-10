using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    public GameManager gameManager;
    public bool isStopped = false;
    public bool StartComplete = false;

    public GameObject Fireball;
    public GameObject aura;
    public GameObject aura2;
    public GameObject MiniDragon;

    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer sr;
    CapsuleCollider2D capCol;

    Queue<string> queue;
    List<string> list;
    List<float[]> locationList;

    float endPosition;
    public float liftAmount = 1.0f;  // ������Ʈ�� �ö� �� ����
    public float liftDuration = 1.0f;  // �ö󰡴� �� �ɸ��� �� �ð�
    float launchSpeed; // ��ų ������ �ӵ�
    float waitTime; // Stay ���� �ð�
    float forceLevel; // �̵��Ҷ� ���� ��
    float range; // ���̾ ����

    void Start()
    {
        CreateAura(aura, 0.7f);
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        capCol = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        anim.SetTrigger("Lift");
        StartCoroutine(Lift());

        // ��ɾ� ����Ʈ �ʱ�ȭ 
        list = new List<string>();
        list.Add("Fly");
        list.Add("Spit");
        list.Add("Teleport");
        list.Add("Spit");
        list.Add("Summons");


        // ��� ť �ʱ�ȭ
        queue = new Queue<string>();
        for (int i = 0; i < list.Count; i++)
        {
            queue.Enqueue(list[i]);
            queue.Enqueue("Stay");
        }

        // ��ȯ�� �̴ϵ巡�� ��ġ ����Ʈ �ʱ�ȭ
        locationList = new List<float[]> {
            new float[] { -0.5f, 0.7f },
            new float[] { 5f, -3.3f },
            new float[] { -2.4f, -1.3f }
        };
    }

    void Update()
    {
        // ����, �̵�, ��� �ӵ� ���� 
        if (gameManager.bossHealth > 9)
        {
            range = 5;
            waitTime = 1.5f;
            launchSpeed = 2.0f;
            forceLevel = 2000;
        } else if (gameManager.bossHealth > 6)
        {
            range = 7;
            sr.color = new Color(255, 100, 0, 255);
            waitTime = 1;
            launchSpeed = 5.0f;
            forceLevel = 4000;
        } else
        {
            range = 9;
            sr.color = new Color(255, 0, 0, 255);
            waitTime = 0.5f;
            launchSpeed = 7.0f;
            forceLevel = 6000;
        }

        if (!anim.GetBool("isDead") && StartComplete && anim.GetBool("isLifted") && !isStopped)
        {
            if (!anim.GetBool("isFlying") && anim.GetBool("isLifted"))
            {
                // ť���� ���� ���� ���� ��� 
                string pattern = queue.Dequeue();
                if (pattern.Equals("Fly"))
                {
                    Fly();
                    
                } else if (pattern.Equals("Teleport"))
                {
                    anim.SetTrigger("Teleport");
                    Invoke("Teleport", 0.5f);
                } else if (pattern.Equals("Stay"))
                {
                    StartCoroutine(Stay());
                } else if (pattern.Equals("Spit"))
                {
                    Spit();
                } else if (pattern.Equals("Summons"))
                {
                    Summons();
                }
                if (!pattern.Equals("Stay")) queue.Enqueue(pattern); // �ٽ� ť�� ���� 
                queue.Enqueue("Stay");

            }
            else if (anim.GetBool("isFlying")) // �̵� ��
            {
                if (endPosition < 0)
                {
                    if (transform.position.x <= endPosition)
                    {
                        rb.velocity = Vector2.zero;
                        anim.SetBool("isFlying", false);
                        turn();
                    }
                }
                else
                {
                    if (transform.position.x >= endPosition)
                    {
                        rb.velocity = Vector2.zero;
                        anim.SetBool("isFlying", false);
                        turn();
                    }
                }
            }
        }
    }

    // ���̾ 
    void Spit()
    {
        anim.SetTrigger("Spit");
        Invoke("CreateProjectile", 1f);
    }

    // �߻�ü ����
    void CreateProjectile()
    {
        float angleStep = 120 / range; // ���� ����

        for (int i = 1; i < 10; i++)
        {
            // ������Ÿ���� ���� ��ġ�� �÷��̾� ������ ���� �ű�
            Vector3 startPosition;
            GameObject projectile;
            Rigidbody2D rb;
            float angle;
            float angleDegrees;
            Vector2 direction;

            // ������Ÿ�� �߻�
            if (sr.flipX) // ����
            {
                angleDegrees = -120f - (angleStep * i);
                angle = Mathf.Deg2Rad * (angleDegrees); // ������ �������� ��ȯ

                startPosition = transform.position - transform.right * 1.7f + transform.up * 0.8f;
                Quaternion rotation = Quaternion.Euler(0f, 0f, angleDegrees);
                projectile = Instantiate(Fireball, startPosition, rotation);

                rb = projectile.GetComponent<Rigidbody2D>();
                direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)); // ���� ���� ���
                
            }
            else // ������
            {
                angleDegrees = -60f + (angleStep * i);
                angle = Mathf.Deg2Rad * (angleDegrees); // ������ �������� ��ȯ

                startPosition = transform.position + transform.right * 1.7f + transform.up * 0.8f;
                Quaternion rotation = Quaternion.Euler(0f, 0f, angleDegrees);
                projectile = Instantiate(Fireball, startPosition, rotation);

                rb = projectile.GetComponent<Rigidbody2D>();
                direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)); // ���� ���� ���
                
            }
            rb.AddForce(direction * launchSpeed, ForceMode2D.Impulse);
        }
    }

    // �巡�� �̵� 
    void Fly()
    {

        anim.SetBool("isFlying", true);
        endPosition = transform.position.x > 0 ? -6 : 6;
        Vector2 force;
        if (transform.position.x > 0)
        {
            force = -transform.right * forceLevel;
        } else
        {
            force = transform.right * forceLevel;
        }
       
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    
    // �巡�� �����̵� 
    void Teleport()
    {
        transform.position = new Vector3(-transform.position.x, -transform.position.y, 0);
        turn();
    }

    IEnumerator Stay()
    {
        isStopped = true;
        yield return new WaitForSeconds(waitTime);
        isStopped = false;
    }

    // �巡�� ��������Ʈ ������ 
    void turn()
    {
        if (sr.flipX)
        {
            sr.flipX = false;
        } else
        {
            sr.flipX = true;
        }
    }

    // �巡�� ��� 
    public void OnDie()
    {
        isStopped = true;
        rb.velocity = Vector3.zero;
        anim.SetBool("isDead", true);
        capCol.isTrigger = false;
        rb.gravityScale = 1;
    }

    void Up()
    {
        anim.SetBool("isLifted", true);
    }

    IEnumerator Lift()
    {
        // �ִϸ��̼��� ���
        Invoke("Up", 0.2f);

        // 1�� ���
        yield return new WaitForSeconds(1f);

        // ������Ʈ �̵�
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + new Vector3(0, liftAmount, 0);

        float elapsedTime = 0;
        while (elapsedTime < liftDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / liftDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ���������� ��Ȯ�� ��ġ�� ����
        transform.position = targetPosition;
        yield return new WaitForSeconds(1f);
        StartComplete = true;

    }

    // �̴� �巡�� ��ȯ 
    void Summons()
    {
        CreateAura(aura, 0.7f);
        CreateAura(aura2, 1.3f);
        foreach (float[] location in locationList)
        {
            Vector3 loc = new Vector3(location[0], location[1]);
            Instantiate(MiniDragon, loc, transform.rotation);
        }
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
    void CreateAura(GameObject Aura, float location)
    {
        GameObject aura1 = Instantiate(Aura, transform.position + Vector3.up * location, transform.rotation);
        StartCoroutine(FadeOutSprite(aura1));
    }
}
