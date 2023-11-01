using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement4 : MonoBehaviour
{
    // static public members
    public static PlayerMovement4 instance;

    // -----------------------------------------------------------------------------------------
    // public members
    public float moveSpeed = 4f;
    public Rigidbody2D rb;
    public GameManager4 gameManager;

    // -----------------------------------------------------------------------------------------
    // private members
    private Vector2 movement;
    bool isHorizonMove;

    Animator anim;
    Vector3 dirVec;
    GameObject scanObject;


    // -----------------------------------------------------------------------------------------
    // awake method to initialisation
    void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            gameManager.StageMove4();
        }
    }

    // -----------------------------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        // update members
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Check Button Down & Up
        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");

        // Check Horizontal Move
        if (hDown)
            isHorizonMove = true;
        else if (vDown)
            isHorizonMove = false;
        else if (hUp || vUp)
            isHorizonMove = movement.x != 0;

        // Animation
        if (anim.GetInteger("hAxisRaw") != movement.x)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)movement.x);
        }
        else if (anim.GetInteger("vAxisRaw") != movement.y)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)movement.y);
        }
        else
        {
            anim.SetBool("isChange", false);
        }

        // Direction
        if (vDown && movement.y == 1) // 상
            dirVec = Vector3.up;
        else if (vDown && movement.y == -1) // 하
            dirVec = Vector3.down;
        else if (hDown && movement.x == -1) // 좌
            dirVec = Vector3.left;
        else if (hDown && movement.x == 1) // 우
            dirVec = Vector3.right;

        // Scan Object
        // 스페이스바 눌러서 스캔
        if (Input.GetButtonDown("Jump") && scanObject != null)
        {
            if (scanObject.name == "LegBoss")
            {
                gameManager.SceneMove4("Scene 4 - 1");
            }
        }

    }
    // -----------------------------------------------------------------------------------------
    // fixed update methode
    void FixedUpdate()
    {
        // Move
        Vector2 moveVec = isHorizonMove ? new Vector2(movement.x, 0) : new Vector2(0, movement.y);
        rb.MovePosition(rb.position + moveVec * moveSpeed * Time.fixedDeltaTime);

        // Ray
        Debug.DrawRay(rb.position, dirVec * 0.8f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rb.position, dirVec, 0.8f, LayerMask.GetMask("Object"));

        if (rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
        {
            scanObject = null;
        }
    }


}
