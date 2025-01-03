using UnityEngine;

public class SAM_PoolObject : MonoBehaviour
{
    // 랜덤 위치 생성
    // n초 지나고 다시 풀로

    private float x, y, z;
    private float spawnRange = 20f;

    private float spawnTime = 3f;
    private float elapsedTime;

    void Start()
    {
        x = Random.Range(-spawnRange, spawnRange);
        y = Random.Range(-spawnRange, spawnRange);
        z = Random.Range(-spawnRange, spawnRange);
        transform.position = new Vector3(x, y, z);
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= spawnTime)
        {
            elapsedTime = 0;
            Disappear();
        }
    }

    private void Disappear()
    {
        SAM_PoolManager.Instance.ReturnObject(gameObject.name, gameObject);
    }
}
