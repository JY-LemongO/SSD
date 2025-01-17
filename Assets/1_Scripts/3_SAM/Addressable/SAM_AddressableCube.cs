using UnityEngine;

public class SAM_AddressableCube : MonoBehaviour
{
    private float x, y, z;
    private float spawnRange = 10f;
    void Start()
    {
        x = Random.Range(-spawnRange, spawnRange);
        y = Random.Range(-spawnRange, spawnRange);
        z = Random.Range(-spawnRange, spawnRange);
        transform.position = new Vector3(x, y, z);
    }
}
