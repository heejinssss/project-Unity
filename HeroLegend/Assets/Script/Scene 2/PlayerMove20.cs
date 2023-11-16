using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove20 : MonoBehaviour
{
    float h;
    float v;
    bool isHorizonMove;
    bool isOpen;
    bool explanationClose;
    Vector3 dirVec;
    Rigidbody2D rigid;
    Animator anim;
    GameObject scanObject;

    public float Speed;
    public GameManager2_0 gameManager;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 이동 값
        h = gameManager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        v = gameManager.isAction ? 0 : Input.GetAxisRaw("Vertical");

        // 버튼 클릭 체크 
        bool hDown = gameManager.isAction ? false : Input.GetButtonDown("Horizontal");
        bool vDown = gameManager.isAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = gameManager.isAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = gameManager.isAction ? false : Input.GetButtonUp("Vertical");

        /// 수평이동 체크 
        if (hDown)
        {
            isHorizonMove = true;
        } else if (vDown) 
        {
            isHorizonMove = false;
        } else if (hUp || vUp)
        {
            isHorizonMove = h != 0;
        }

        // 애니메이션 컨트롤 
        if (anim.GetInteger("hAxisRaw") != (int) h)
        {
            anim.SetBool("isChanged", true);
            anim.SetInteger("hAxisRaw", (int)h);
        } else if (anim.GetInteger("vAxisRaw") != (int) v)
        {
            anim.SetBool("isChanged", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
        {
            anim.SetBool("isChanged", false);
        }

        // 방향 
        if (vDown && v == 1) // 상 
        {
            dirVec = Vector3.up;
        } else if (vDown && v == -1)
        {
            dirVec = Vector3.down;
        } else if (hDown && h == -1)
        {
            dirVec = Vector3.left;
        } else if (hDown && h == 1)
        {
            dirVec = Vector3.right;
        }

        // Scan Object
        if (Input.GetButtonDown("Jump") && scanObject != null)
        {
            AudioManager2.instance.PlaySfx(AudioManager2.Sfx.Scan);
            if (scanObject.CompareTag("Item"))
            {
                if (isOpen)
                {
                    isOpen = false;
                } else
                {
                    isOpen = true;   
                }
                gameManager.OpenBook(isOpen);
            } else
            {
                gameManager.Action(scanObject);
            }
        } else if (Input.GetButtonDown("Jump"))
        {
            if (!explanationClose)
            {
                explanationClose = true;
                gameManager.CloseExplanation();
            }
        }

        
    }

    private void FixedUpdate()
    {
        // 이동 
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity  = moveVec * Speed;
        if (rigid.velocity != Vector2.zero)
        {
            AudioManager2.instance.PlayWalk();
        }

        // Ray
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0,1,0));
        RaycastHit2D rayhit = Physics2D.Raycast(rigid.position, dirVec, 1.2f, LayerMask.GetMask("Object"));

        if(rayhit.collider != null)
        {
            scanObject = rayhit.collider.gameObject;
        } else
        {
            scanObject = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            gameManager.SetActiveButton(true);
        }
    }
}
