using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    public BossDamaged bossDamaged;
    public bool isStopped = false;
    public bool StartComplete = false;

    public GameObject Fireball;
    public float launchSpeed; // ��ų ������ �ӵ�


    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Queue<string> queue;
    List<string> list;
    float endPosition;
    public float liftAmount = 1.0f;  // ������Ʈ�� �ö� �� ����
    public float liftDuration = 1.0f;  // �ö󰡴� �� �ɸ��� �� �ð�



    void Start()
    {
        
        bossDamaged = GetComponent<BossDamaged>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        anim.SetTrigger("Lift");
        StartCoroutine(Lift());

        // ��ɾ� ����Ʈ �ʱ�ȭ 
        list = new List<string>();
        list.Add("Fly");
        list.Add("Spit");
        list.Add("Teleport");
        list.Add("Spit");


        // ��� ť 
        queue = new Queue<string>();
        for (int i = 0; i < list.Count; i++)
        {
            queue.Enqueue(list[i]);
            queue.Enqueue("Stay");
        }
    }

    void Update()
    {
        if (StartComplete && anim.GetBool("isLifted") && !isStopped)
        {
            if (bossDamaged.hp <= 0)
            {
                // �ǹ� -> ��� �巡�� 
                // ��� -> �ݻ� 
                // �ݻ� -> �� 
                // �� -> ���� 
            }
            else if (!anim.GetBool("isFlying") && anim.GetBool("isLifted"))
            {
                // ť���� ���� ���� ���� ��� 
                string pattern = queue.Dequeue();
                Debug.Log(pattern);
                if (pattern.Equals("Fly"))
                {
                    Fly();
                } else if (pattern.Equals("Teleport"))
                {
                    Invoke("Teleport", 2);
                } else if (pattern.Equals("Stay"))
                {
                    StartCoroutine(Stay());
                } else if (pattern.Equals("Spit"))
                {
                    Spit();
                }
                if (!pattern.Equals("Stay")) queue.Enqueue(pattern); // �ٽ� ť�� ���� 
                queue.Enqueue("Stay");

            }
            else if (anim.GetBool("isFlying"))
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
    void Spit()
    {
        anim.SetTrigger("Spit");
        Invoke("CreateProjectile", 1f);
    }

    void CreateProjectile()
    {
        float angleStep = 120 / 9; // ���� ����

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


    void Fly()
    {

        anim.SetBool("isFlying", true);
        endPosition = transform.position.x > 0 ? -6 : 6;
        Vector2 force;
        if (transform.position.x > 0)
        {
            force = -transform.right * 3000;
        } else
        {
            force = transform.right * 3000;
        }
       
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    

    void Teleport()
    {
        transform.position = new Vector3(-transform.position.x, -transform.position.y, 0);
        turn();
    }

    IEnumerator Stay()
    {
        isStopped = true;
        yield return new WaitForSeconds(3f);
        isStopped = false;
    }




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

    

    void Attack()
    {

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

    
}
