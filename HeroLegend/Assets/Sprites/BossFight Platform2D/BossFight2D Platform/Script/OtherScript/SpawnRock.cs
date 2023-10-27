using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRock : MonoBehaviour
{
    public Transform[]  spawnTransform;
    public Rigidbody2D  rockPrefabFirst;
    public Rigidbody2D  rockPrefabSecond;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void SpawnRockEffect()
    {
        StartCoroutine("SongEffectSpawn");
    }

    IEnumerator SongEffectSpawn()
    {
        yield return new WaitForSeconds(0.6f);
        Rigidbody2D cloneRockPrefabFirst;
        cloneRockPrefabFirst = Instantiate(rockPrefabFirst, spawnTransform[Random.Range(0, 4)].transform.position, transform.rotation);
        yield return new WaitForSeconds(0.3f);
        Rigidbody2D cloneRockPrefabSecond;
        cloneRockPrefabSecond = Instantiate(rockPrefabSecond, spawnTransform[Random.Range(0, 4)].transform.position, transform.rotation);
    }
}
