using System.Collections;
using UnityEngine;

public class TestBulletChild : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private void Update()
    {
        MoveForward();
    }

    public void Setup(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;

        StartCoroutine(Co_CountDownToDisable());
    }

    private void MoveForward()
    {
        transform.position += transform.forward * _moveSpeed * Time.deltaTime;
    }

    private IEnumerator Co_CountDownToDisable()
    {
        float current = 0f;

        while (current < 5f)
        {
            current += Time.deltaTime;
            yield return null;
        }

        PoolManager.Instance.Return(this, name);
    }
}
