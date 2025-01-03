using UnityEngine;

public class SAM_GameManager : SAM_Singleton<SAM_GameManager>
{
    [SerializeField] public SAM_Character character;

    protected override void Init()
    {
        _dontDestroyOnLoad = true;
        base.Init();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
