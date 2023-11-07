using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    public Transform player;



    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        // if (!GameManager.instance.isLive)
        //     return;

        Vector3 distance_box = player.position - transform.position;
        if (distance_box[0] < 0)
        {
            Spawn();
        }
    }
        void Spawn() 
    {
        GameObject enemy = GameManager.instance.pool.Get(2);
        enemy.transform.position = spawnPoint[0].position;
        // enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

[System.Serializable]
public class SpawnData
{
    public int spriteType;
    public int health;
}
