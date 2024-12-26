using System.Collections;
using UnityEngine;

public class TestBulletParent : MonoBehaviour
{
    [SerializeField] private TestBulletChild childBulletPrefab;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _moveSpeed;

    private Vector3 forwardVect;
    private PoolShooter _context;
    private Coroutine _coroutine;

    private void Update()
    {
        InfiniteRotate();
        MoveForward();
    }    

    public void Setup(PoolShooter context, Vector3 position, Quaternion rotation)
    {
        _context = context;
        transform.position = position;
        transform.rotation = rotation;
        forwardVect = transform.forward;

        StartCoroutine(Co_MoveForwardAndReturnToPool());
    }

    public void ShootChildBullets(float shootDelay, int spawnBulletChildCount)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(Co_ShootChildBullets(shootDelay, spawnBulletChildCount));
    }

    private void InfiniteRotate()
    {
        transform.Rotate(Vector3.up * _rotateSpeed * Time.deltaTime);
    }

    private void MoveForward()
    {
        transform.position += forwardVect * _moveSpeed * Time.deltaTime;
    }

    private IEnumerator Co_ShootChildBullets(float shootDelay, int spawnBulletChildCount)
    {
        for(int i = 0; i < spawnBulletChildCount; i++)
        {
            TestBulletChild bulletChild = PoolManager.Instance.Get(childBulletPrefab, childBulletPrefab.name);
            bulletChild.Setup(transform.position, transform.rotation);
            yield return new WaitForSeconds(shootDelay);            
        }        
    }

    private IEnumerator Co_MoveForwardAndReturnToPool()
    {
        float current = 0f;

        while(current < 5f)
        {
            current += Time.deltaTime;
            yield return null;
        }

        StopAllCoroutines();
        _context.RemoveParentBulletFromList(this);
        PoolManager.Instance.Return(this, name);
    }
}
