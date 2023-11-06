using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticRunner : MonoBehaviour
{
    public float Speed;

    public float moveSpeed = 5.0f; // 이동 속도를 조절할 변수
    private int spacebarCount = 0;

    Rigidbody2D rigid;
    Animator anim;
    float h;
    float v;
    bool isHorizonMove; // 수평으로 이동하고 있는가?
    Vector3 dirVec;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 스페이스바가 눌렸는지 확인
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    spacebarCount++;
        //}

        //// 스페이스바가 3번 눌렸을 때 오른쪽으로 이동
        //if (spacebarCount >= 3)
        //{
        //    // 오른쪽으로 이동
        //    transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);
        //}

        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");

        if (hDown)
            isHorizonMove = true;
        else if (vDown)
            isHorizonMove = false;
        else if (hUp || vUp)
            isHorizonMove = h != 0;

        if (anim.GetInteger("hAxisRaw") != h)
        {
            anim.SetBool("isChanged", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if (anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetBool("isChanged", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
            anim.SetBool("isChanged", false);

        // Direction
        if (vDown && v == 1) // Up Key
            dirVec = Vector3.up;
        else if (vDown && v == -1) // Down Key
            dirVec = Vector3.down;
        else if (hDown && h == -1) // Left Key
            dirVec = Vector3.left;
        else if (hDown && h == 1) // Right Key
            dirVec = Vector3.right;
    }

    void FixedUpdate()
    {
        // 수평 이동이라면 vs 수평 이동이 아니라면
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * Speed;

    }
}
