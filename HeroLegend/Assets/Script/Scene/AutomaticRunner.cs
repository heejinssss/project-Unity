using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AutomaticRunner : MonoBehaviour
{
    public float Speed;

    public float moveSpeed = 5.0f;
    private int spacebarCount = 0;
    public int isChangedController = 0;

    private bool spacebarActivated = false;
    private float spacebarEffectDuration = 2.0f; // 여자친구 이동 시간
    private float spacebarEffectTimer = 0.0f;

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
        if (spacebarActivated) // 3번째 스크립트 미클릭!
        {
            spacebarEffectTimer += Time.deltaTime;

            if (spacebarEffectTimer >= spacebarEffectDuration)
            {
                // 스페이스바 효과 종료
                spacebarActivated = false;
                spacebarCount = 0;
                spacebarEffectTimer = 4.0f;
            }
            else
            {
                // 스페이스바 효과 진행 중
                // true -> false로 변경해야 걷는 애니메이션 가능
                //anim.SetInteger("hAxisRaw", 1);
                //anim.SetBool("isChanged", true);

                if (anim.GetInteger("hAxisRaw") != 1)
                {
                    anim.SetBool("isChanged", true);
                    anim.SetInteger("hAxisRaw", 1);
                }
                else
                    anim.SetBool("isChanged", false);


                //if (anim.GetBool("isChanged") == true)
                //{
                //    anim.SetBool("isChanged", false);
                //    Debug.Log(anim.GetBool("Letsgirigiri"));
                //}

                Vector2 moveVec = new Vector2(h, 0);
                rigid.velocity = moveVec * Speed;
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            }
        }
        else  // 3번째 스크립트 클릭!
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                spacebarCount++;

                if (spacebarCount >= 4)
                {
                    spacebarActivated = true;
                }
            }

            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");

            bool hDown = Input.GetButtonDown("Horizontal");
            bool vDown = Input.GetButtonDown("Vertical");
            bool hUp = Input.GetButtonUp("Horizontal");
            bool vUp = Input.GetButtonUp("Vertical");

            if (hDown) // 좌 또는 우 방향키를 눌렀다면
                isHorizonMove = true; // 수평 이동 True
            else if (vDown) // 상 또는 하 방향키를 눌렀다면
                isHorizonMove = false; // 수평 이동 True    
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
    }

    void FixedUpdate()
    {
        // 수평 이동이라면 vs 수평 이동이 아니라면
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * Speed;

    }
}
