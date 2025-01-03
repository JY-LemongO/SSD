using UnityEngine;

public class JYStatManager : MonoBehaviour
{
    public static JYStatManager Instance => _instance;
    private static JYStatManager _instance;
    
    [SerializeField] private JYStat statATKSO;
    [SerializeField] private JYStat statDEFSO;
    
    public JYStat StatATK => statATK;
    public JYStat StatDEF => statDEF;

    private JYStat statATK;
    private JYStat statDEF;

    private void Awake()
    {
        if (!_instance)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            DestroyImmediate(gameObject);

        statATK = statATKSO.Clone() as JYStat;
        statDEF = statDEFSO.Clone() as JYStat;
    }
}
