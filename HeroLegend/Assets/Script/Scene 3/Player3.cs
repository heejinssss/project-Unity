using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player3 : MonoBehaviour
{
    public enum State { Stand, Run, Jump, Hit, Death }
    public float startJumpPower;
    public float jumpPower;
    private float reducedJumpPower;
    public bool isGround;
    public bool isJumpKey;
    public UnityEvent onHit;
    public GameManager3 gameManager;

    Rigidbody2D rigid;
    Animator animator;
    // Sounder sound;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        // sound = GetComponent<Sounder>();
    }

    void Start()
    {
        // sound.PlaySound(Sounder.Sfx.Start);
    }

    void Update()
    {
        if (!GameManager3.isLive)
            return;

        // 1-1. 숏 점프 (기본 점프)
        if (Input.GetButtonDown("Jump") && isGround) // 누른 첫 상태
        {
            rigid.AddForce(Vector2.up * startJumpPower, ForceMode2D.Impulse);
        }

        // Input이 FixedUpdate에 들어있을 경우, 가끔 씹히는 경우가 있음 -> 변수 선언
        isJumpKey = Input.GetButton("Jump");
    }

    void FixedUpdate()
    {
        if (!GameManager3.isLive)
            return;

        // 1-2. 롱 점프
        if (isJumpKey && !isGround) // 누른 상태 지속
        {
            reducedJumpPower = Mathf.Lerp(reducedJumpPower, 0, 0.1f); // jumpPower -> 0
            rigid.AddForce(Vector2.up * reducedJumpPower, ForceMode2D.Impulse);
        }
    }

    // 2. 착지 (물리 충돌 이벤트)
    void OnCollisionStay2D(Collision2D collision)
    {
        if (!isGround)
        {
            ChangeAnim(State.Run);
            // sound.PlaySound(Sounder.Sfx.Land);
            reducedJumpPower = jumpPower; // 점프 상태에서 0이 되었다가, 착지했을 때 다시 1로
        }
        isGround = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Land")
        {
            ChangeAnim(State.Jump);
            // sound.PlaySound(Sounder.Sfx.Jump);
            isGround = false;
        }
    }

    // 3. 장애물 터치 (트리거 충돌 이벤트)
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy");
            // rigid.simulated = false; // rigidBody는 simulated로 활성화 및 비활성화
            // sound.PlaySound(Sounder.Sfx.Hit);
            // onHit.Invoke(); // onHit에 연결된 함수 호출

            OnDamaged(collision.transform.position);

        }
        else if (collision.gameObject.tag == "Item")
        {
            gameManager.health += 1;
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Finish")
        {
            // 장면 전환
            Debug.Log("Finish");
            gameManager.NextStage();
        }
    }


    void OnDamaged(Vector2 targetPos)
    {
        // Health Down
        gameManager.HealthDown();

        if (GameManager3.isLive)
        {
            // Change Layer (Immortal Active)
            gameObject.layer = 9; // 9 = PlayerDamaged
            // Reaction Force
            // int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1; // 플레이어 x 축 - 충돌 정보 x축
            // rigid.AddForce(new Vector2(dirc, 1) * 5, ForceMode2D.Impulse);
            // Animation
            ChangeAnim(State.Hit);
            Invoke("OffDamaged", 1);

        }
    }

    void OffDamaged()
    {
        gameObject.layer = 8; // 8 = Player
        ChangeAnim(State.Run);
    }


    public void OnDie()
    {
        transform.GetChild(1).gameObject.SetActive(false);
        ChangeAnim(State.Death);
    }

    // 4. 애니메이션
    void ChangeAnim(State state)
    {
        animator.SetInteger("State", (int)state);
    }
}
