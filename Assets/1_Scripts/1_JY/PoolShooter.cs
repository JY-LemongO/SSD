using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolShooter : MonoBehaviour
{
    [Header("Prefab For Basic Pooling")]
    [SerializeField] private TestBulletParent parentBulletPrefab;    

    [Header("Settings")]
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _shootDelay;
    [SerializeField] private int _spawnBulletParentCount;
    [SerializeField] private int _spawnBulletChildCount;

    private List<TestBulletParent> _bulletParentList = new();

    private Coroutine _coroutine;
    private WaitForSeconds _delay;

    private void Awake()
    {
        _delay = new WaitForSeconds(_shootDelay);
    }

    private void Update()
    {
        InifiniteRotate();

        if (Input.GetMouseButtonDown(0))
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(Co_ShootParentBullets());
        }
        if (Input.GetMouseButtonDown(1))
            ShootChildBullets();
    }

    private void InifiniteRotate()
    {
        transform.Rotate(Vector3.up * _rotateSpeed * Time.deltaTime);
    }

    private IEnumerator Co_ShootParentBullets()
    {
        Debug.Log($"{GetType()}:: Co_ShootParentBullets");
        for (int i = 0; i < _spawnBulletParentCount; i++)
        {            
            TestBulletParent bulletParentClone = PoolManager.Instance.Get(parentBulletPrefab, parentBulletPrefab.name);
            bulletParentClone.Setup(this, transform.position, transform.rotation);
            _bulletParentList.Add(bulletParentClone);
            yield return _delay;
        }        
    }

    private void ShootChildBullets()
    {
        Debug.Log($"{GetType()}:: ShootChildBullets");        

        foreach (var parentBullet in _bulletParentList)
            parentBullet.ShootChildBullets(_shootDelay, _spawnBulletChildCount);
    }

    public void RemoveParentBulletFromList(TestBulletParent parentBullet) => _bulletParentList.Remove(parentBullet);
}
