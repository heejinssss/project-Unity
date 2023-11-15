using UnityEngine;

public class EnemyAction5 : MonoBehaviour
{
    Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
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
