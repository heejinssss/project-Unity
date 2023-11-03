using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    // ��ų ������
    public GameObject water; 
    public GameObject fire;

    Animator anim;

    public float launchSpeed = 10.0f; // ��ų ������ �ӵ� 
    public float delay = 0.5f;     // ��Ÿ ������ �ð� (��)

    private bool isCooldown = false;

    private void Start()
    {
        Transform playerTransform = this.transform;
        anim = playerTransform.Find("UnitRoot").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isCooldown)
        {
            CreateProjectile(fire);
            StartCoroutine(Cooldown()); // ��Ÿ ������ ����
            
        } else if(Input.GetKeyDown(KeyCode.X) && !isCooldown) {
            CreateProjectile(water);
            StartCoroutine(Cooldown()); // ��Ÿ ������ ����
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            Debug.Log("������ �߻�");
            Rigidbody2D objectRb = collision.gameObject.GetComponent<Rigidbody2D>();
            objectRb.AddForce(-transform.right * launchSpeed, ForceMode2D.Impulse);
        }
    }

    void CreateProjectile(GameObject projectilePrefab)
    {
        anim.SetTrigger("Skill");
        // ������Ÿ���� ���� ��ġ�� �÷��̾� ������ ���� �ű�
        Vector3 startPosition = transform.position - transform.right * 0.7f + transform.up * 0.5f;

        // �������κ��� ���ο� ������Ÿ�� ������Ʈ ����
        GameObject projectile = Instantiate(projectilePrefab, startPosition, transform.rotation);

        // ������Ÿ�Ϸκ��� ������ٵ� 2D ������Ʈ ������
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        // ������Ÿ�� �߻�
        rb.AddForce(-transform.right * launchSpeed, ForceMode2D.Impulse);
    }

    IEnumerator Cooldown()
    {
        isCooldown = true; // ��Ÿ ������ ���� ��ٿ� Ȱ��ȭ
        yield return new WaitForSeconds(delay); // ��ٿ� �ð� ���� ��� (0.5�ʷ� ����)
        isCooldown = false; // ��ٿ� ��Ȱ��ȭ
    }
}
