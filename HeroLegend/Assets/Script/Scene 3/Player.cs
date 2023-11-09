using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public enum State { Stand, Run, Jump, Hit, Death }
    public float startJumpPower;
    public float jumpPower;
    private float reducedJumpPower;
    public bool isGround;
    public bool isJumpKey;
    public UnityEvent onHit;
    public GameManager gameManager;

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
        if (!GameManager.isLive)
            return;

        // 1-1. �� ���� (�⺻ ����)
        if (Input.GetButtonDown("Jump") && isGround) // ���� ù ����
        {
            rigid.AddForce(Vector2.up * startJumpPower, ForceMode2D.Impulse);
        }

        // Input�� FixedUpdate�� ������� ���, ���� ������ ��찡 ���� -> ���� ����
        isJumpKey = Input.GetButton("Jump");
    }

    void FixedUpdate()
    {
        if (!GameManager.isLive)
            return;

        // 1-2. �� ����
        if (isJumpKey && !isGround) // ���� ���� ����
        {
            reducedJumpPower = Mathf.Lerp(reducedJumpPower, 0, 0.1f); // jumpPower -> 0
            rigid.AddForce(Vector2.up * reducedJumpPower, ForceMode2D.Impulse);
        }
    }

    // 2. ���� (���� �浹 �̺�Ʈ)
    void OnCollisionStay2D(Collision2D collision)
    {
        if (!isGround)
        {
            ChangeAnim(State.Run);
            // sound.PlaySound(Sounder.Sfx.Land);
            reducedJumpPower = jumpPower; // ���� ���¿��� 0�� �Ǿ��ٰ�, �������� �� �ٽ� 1��
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

    // 3. ��ֹ� ��ġ (Ʈ���� �浹 �̺�Ʈ)
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // sound.PlaySound(Sounder.Sfx.Hit);
            OnDamaged(collision.transform.position);

        }
        else if (collision.gameObject.tag == "Item")
        {
            gameManager.health += 1;
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Finish")
        {
            // ��� ��ȯ
            gameManager.NextStage();
        }
    }


    void OnDamaged(Vector2 targetPos)
    {
        // Health Down
        gameManager.HealthDown();

        if (GameManager.isLive)
        {
            // Change Layer (Immortal Active)
            gameObject.layer = 9; // 9 = PlayerDamaged

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

    // 4. �ִϸ��̼�
    void ChangeAnim(State state)
    {
        animator.SetInteger("State", (int)state);
    }
}
