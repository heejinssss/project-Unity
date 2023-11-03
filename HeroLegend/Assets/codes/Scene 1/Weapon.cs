using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;
    Player player;

    float timer;

    void Awake()
    {
        player = GameManager.instance.player;
    }

    private bool isButtonPressed = false;

    void Update()
    {
        // if (!GameManager.instance.isLive)
        //     return;
        switch (id) {
            case 0:
    
                // if (Input.GetKey(KeyCode.LeftControl)) {
                //     Fire();
                // }

                if (Input.GetKey(KeyCode.LeftControl))
                {
                    if (!isButtonPressed)
                    {
                        Fire();
                        isButtonPressed = true;
                    }
                }
                else
                {
                    isButtonPressed = false;
                }
                break;
            default:
                break;
        }


    }

    // public void LevelUp(float damage, int count)
    // {
    //     this.damage = damage;
    //     this.count += count;

    //     if (id == 0)
    //         Batch();

    //     // player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    // }

    public void Init()
    {
        // ItemData data
        // name = "Weapon." + data.itemId;
        // transform.parent = player.transform;
        // transform.localPosition = Vector3.zero;

        // id = data.itemId;
        // damage = data.baseDamage * Character.Damage;
        // count = data.baseCount + Character.Count;

        // for (int index=0; index < GameManager.instance.pool.prefabs.Length; index++) {
        //     if (data.projectile == GameManager.instance.pool.prefabs[index]) {
        //         prefabId = index;
        //         break;
        //     }
        // }

        switch (id) {
            case 0:
                // speed = 150 * Character.WeaponSpeed;
                speed = 1f;
                // Batch();
                break;
            default:
                // speed = 0.5f * Character.WeaponRate;
                break;
        }
        // Hand hand = player.hands[(int)data.itemType];
        // hand.spriter.sprite = data.hand;
        // hand.gameObject.SetActive(true);


        // player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    // void Batch() 
    // {
    //     for (int index=0; index < count; index++) {
    //         Transform noise;

    //         if (index < transform.childCount){
    //             noise = transform.GetChild(index);
    //         }
    //         else {
    //             noise = GameManager.instance.pool.Get(prefabId).transform;
    //             noise.parent = transform;
    //         }

    //         noise.localPosition = Vector3.zero;
    //         noise.localRotation = Quaternion.identity;

    //         Vector3 rotVec = Vector3.forward * 360 * index / count;
    //         noise.Rotate(rotVec);
    //         noise.Translate(noise.up * 1.5f, Space.World);

    //         noise.GetComponent<Noise>().Init(damage, Vector3.zero);

    //         // AudioManager.instance.PlaySfx(AudioManager.Sfx.Melee);

    //         // -1 = Infinity Per
    //     }

    // }

    void Fire() 
    {
        if (!player.scanner.nearestTarget)

            return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform noise = GameManager.instance.pool.Get(prefabId).transform;
        noise.position = transform.position;
        noise.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        noise.GetComponent<Noise>().Init(damage, count, dir);


    }
}
