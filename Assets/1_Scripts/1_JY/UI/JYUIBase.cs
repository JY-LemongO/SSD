using UnityEngine;

public abstract class UIBase : MonoBehaviour
{
    public string CodeName => _codeName;

    private string _codeName;

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        _codeName = GetType().Name;
    }

    public virtual void Setup() { }

    protected abstract void Dispose();
    private void OnDisable() => Dispose();
}
