using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamaged : MonoBehaviour
{
    public int hp = 100;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerSkill"))
        {
            SkillRemove skillRemove = collision.gameObject.GetComponent<SkillRemove>();
            skillRemove.DeActive();

            // anim.SetTrigger("hit");
        } else if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            collision.gameObject.GetComponent<SkillRemove>().Active();
            anim.SetTrigger("hit");
            hp -= 1;
        }
    }
}
