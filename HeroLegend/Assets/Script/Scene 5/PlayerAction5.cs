using UnityEngine;

public class PlayerAction5 : MonoBehaviour
{
    Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            AttackonTitan();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            ThreeMatchProcess();
        }
    }

    void AttackonTitan()
    {
        anim.SetBool("isCompleted", true);
        anim.SetBool("isSkilled", true);
    }
    void ThreeMatchProcess()
    {
        anim.SetBool("isCompleted", false);
        anim.SetBool("isSkilled", false);
    }
}
