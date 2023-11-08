using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedItem : MonoBehaviour
{
    public int id;
    public GameObject[] itemPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Coroutine으로 아이템 생성
    public void DropItem(Vector3 spawnPosition)
    {
        StartCoroutine(DropItemCoroutine(spawnPosition));
        int randomItemIndex = Random.Range(0, itemPrefabs.Length);
        Debug.Log(randomItemIndex);
        GameObject spawnedItem = Instantiate(itemPrefabs[randomItemIndex], spawnPosition, Quaternion.identity);
    }

    IEnumerator DropItemCoroutine(Vector3 spawnPosition)
    {

        yield return new WaitForSeconds(0.5f);        
        // 이후 추가 동작을 수행할 수 있습니다.
    }

    // Update is called once per frame
    void Update()
    {
        // 다른 코드...
    }
}
