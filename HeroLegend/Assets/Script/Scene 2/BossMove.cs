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
    public float launchSpeed; // 스킬 나가는 속도


    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Queue<string> queue;
    List<string> list;
    float endPosition;
    public float liftAmount = 1.0f;  // 오브젝트가 올라갈 총 높이
    public float liftDuration = 1.0f;  // 올라가는 데 걸리는 총 시간



    void Start()
    {
        
        bossDamaged = GetComponent<BossDamaged>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        anim.SetTrigger("Lift");
        StartCoroutine(Lift());

        // 명령어 리스트 초기화 
        list = new List<string>();
        list.Add("Fly");
        list.Add("Spit");
        list.Add("Teleport");
        list.Add("Spit");


        // 명령 큐 
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
                // 실버 -> 블루 드래곤 
                // 블루 -> 금색 
                // 금색 -> 블랙 
                // 블랙 -> 죽음 
            }
            else if (!anim.GetBool("isFlying") && anim.GetBool("isLifted"))
            {
                // 큐에서 다음 패턴 꺼내 사용 
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
                if (!pattern.Equals("Stay")) queue.Enqueue(pattern); // 다시 큐에 넣음 
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
        float angleStep = 120 / 9; // 각도 간격

        for (int i = 1; i < 10; i++)
        {
            // 프로젝타일의 시작 위치를 플레이어 앞으로 조금 옮김
            Vector3 startPosition;
            GameObject projectile;
            Rigidbody2D rb;
            float angle;
            float angleDegrees;
            Vector2 direction;

            // 프로젝타일 발사
            if (sr.flipX) // 왼쪽
            {
                angleDegrees = -120f - (angleStep * i);
                angle = Mathf.Deg2Rad * (angleDegrees); // 각도를 라디안으로 변환

                startPosition = transform.position - transform.right * 1.7f + transform.up * 0.8f;
                Quaternion rotation = Quaternion.Euler(0f, 0f, angleDegrees);
                projectile = Instantiate(Fireball, startPosition, rotation);

                rb = projectile.GetComponent<Rigidbody2D>();
                direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)); // 방향 벡터 계산
                
            }
            else // 오른쪽
            {
                angleDegrees = -60f + (angleStep * i);
                angle = Mathf.Deg2Rad * (angleDegrees); // 각도를 라디안으로 변환

                startPosition = transform.position + transform.right * 1.7f + transform.up * 0.8f;
                Quaternion rotation = Quaternion.Euler(0f, 0f, angleDegrees);
                projectile = Instantiate(Fireball, startPosition, rotation);

                rb = projectile.GetComponent<Rigidbody2D>();
                direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)); // 방향 벡터 계산
                
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
        // 애니메이션을 재생
        Invoke("Up", 0.2f);

        // 1초 대기
        yield return new WaitForSeconds(1f);

        // 오브젝트 이동
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + new Vector3(0, liftAmount, 0);

        float elapsedTime = 0;
        while (elapsedTime < liftDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / liftDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 마지막으로 정확한 위치를 설정
        transform.position = targetPosition;
        yield return new WaitForSeconds(1f);
        StartComplete = true;

    }

    
}
