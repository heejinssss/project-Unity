using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEat1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("ItemEat"), true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("speakereat") || collision.CompareTag("bulleteat") || collision.CompareTag("boomeat") || collision.CompareTag("hearteat"))
        {

            HandleItemCollection(collision.tag);
            
            // 해당 아이템을 비활성화
            collision.gameObject.SetActive(false);
        }
    }

    void HandleItemCollection(string tag)
    {
        // 각 아이템에 따른 처리를 추가
        switch (tag)
        {
            case "speakereat":
                // "speakereat" 아이템에 대한 처리
                break;
            case "bulleteat":
                // "bulleteat" 아이템에 대한 처리
                break;
            case "boomeat":
                // "boomeat" 아이템에 대한 처리
                break;
            case "hearteat":
                // "hearteat" 아이템에 대한 처리
                break;
            // 추가적인 태그에 대한 처리를 필요에 따라 추가
        }
    }


}
