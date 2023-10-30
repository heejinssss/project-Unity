using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed = 3.0f;
    public float moveSpeed = 3.0f; // �÷��̾��� �¿� �̵� �ӵ�
    public float jumpForce = 17.0f; // ������ ���� ��
    private bool isJumping = false; // ���� ������ �ƴ��� �Ǵ��ϱ� ���� ����
    Animator anim; // �ִϸ��̼� ��ȯ�� ���� 

    private Rigidbody2D rb; // �÷��̾��� Rigidbody2D ������Ʈ
    private RectTransform rectTransform; // �÷��̾� ȸ�� ������Ʈ 

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody2D ������Ʈ�� �����ɴϴ�.
        rb = GetComponent<Rigidbody2D>();
        rectTransform = GetComponent<RectTransform>();

        Transform playerTransform = this.transform;
        anim = playerTransform.Find("UnitRoot").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        // Player Move
        float h = Input.GetAxisRaw("Horizontal");
        rb.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        // �ִ� �ӵ� ����
        if (rb.velocity.x > maxSpeed) // Right Max Speed
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        else if (rb.velocity.x < maxSpeed * (-1)) // Left
            rb.velocity = new Vector2(maxSpeed * (-1), rb.velocity.y);

        // Speed ���߱�
        if (Input.GetButtonUp("Horizontal"))
        {
            rb.velocity = new Vector2(rb.velocity.normalized.x * 0.5f, rb.velocity.y);
        }

        // �̵����⿡ ���� Player ������
        if (Input.GetButton("Horizontal"))
        {
            if (rb.velocity.normalized.x > 0)
            {
                rectTransform.localRotation = Quaternion.Euler(0, 180, 0);
            } else if (rb.velocity.normalized.x < 0)
            {
                rectTransform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }

        if (Mathf.Abs(rb.velocity.x) > 0.3)
            anim.SetBool("isRunning", true);
        else
            anim.SetBool("isRunning", false);
    }

    void Jump()
    {
        // ����
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
        }

        // ����
        if (rb.velocity.y < 0)
        {
            Debug.DrawRay(rb.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector3.down, 1, LayerMask.GetMask("PlatForm"));
            if (hit.collider != null)
            {
                if (hit.distance < 0.5f)
                {
                    isJumping = false;
                }
            }
        }
    }
}
