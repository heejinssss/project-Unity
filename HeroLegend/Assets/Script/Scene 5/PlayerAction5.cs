using System.Collections;
using UnityEngine;

public class PlayerAction5 : MonoBehaviour
{
    // 싱글톤 인스턴스 추가
    public static PlayerAction5 Instance { get; private set; }

    Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Awake()
    {
        // 싱글톤 인스턴스 초기화
        Instance = this;
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.S))
    //    {
    //        AttackonTitan();
    //    }

    //    if (Input.GetKeyDown(KeyCode.A))
    //    {
    //        ThreeMatchProcess();
    //    }
    //}

    // 10초간 특별 능력 활성화
    public void EnableSpecialAbilityFor10Seconds()
    {
        AttackonTitan();
        StartCoroutine(ResetAbilitiesAfter10Seconds());
    }

    private IEnumerator ResetAbilitiesAfter10Seconds()
    {
        yield return new WaitForSeconds(10);
        ThreeMatchProcess();
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
