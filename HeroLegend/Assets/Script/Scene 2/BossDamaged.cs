using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamaged : MonoBehaviour
{
    public GameManager gameManager;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "PlayerSkill" || LayerMask.LayerToName(collision.gameObject.layer) == "Item")
        {
            SkillRemove skillRemove = collision.gameObject.GetComponent<SkillRemove>();
            skillRemove.DeActive();
        } else if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            if (collision.gameObject.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
            {
                collision.gameObject.GetComponent<SkillRemove>().Active();
                anim.SetTrigger("hit");
                gameManager.BossHealthDown();
            }
            else
            {
                collision.gameObject.GetComponent<SkillRemove>().DeActive();
            }
        }
    }
}
