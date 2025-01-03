using UnityEngine;

public class SAM_Character : MonoBehaviour
{
    [SerializeField] SAM_StatSO statSO;
    public SAM_Stat stat { get; set; }

    private void Awake()
    {
        stat = new SAM_Stat(statSO);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
