using System.Collections.Generic;
using System.Security.Cryptography;
using NUnit.Framework;
using UnityEngine;

public class SAM_UIManager : SAM_Singleton<SAM_UIManager> // UI매니저는 마지막에 Awake되게
{
    // 딕셔너리가 나으려나
    private Dictionary<string, SAM_UIBase> _UIStorage = new Dictionary<string, SAM_UIBase>();
            
    protected override void Init()
    {
        _dontDestroyOnLoad = true;
        ClearAllUI();

        base.Init();
    }

    public void RegisterUI(string UIName)//UI 이름이랑, 프리팹?
    {
        if (!_UIStorage.ContainsKey(UIName))
        {
            GameObject UIObject = SAM_ResourceManager.Instance.Instantiate($"{UIName}");

            UIObject.name = UIName;

            _UIStorage.Add(UIName, UIObject.GetComponent<SAM_UIBase>());
        }        
    }

    public void ShowUI(string UIName)
    {
        if (_UIStorage.ContainsKey(UIName))
            _UIStorage[UIName].ShowUI();
        else
            Debug.Log($"UI load failed: {UIName}");
    }

    public void HideUI(string UIName)
    {
        if (_UIStorage.ContainsKey(UIName))
            _UIStorage[UIName].HideUI();
        else
            Debug.Log($"UI load failed: {UIName}");
    }
    
    public void ShowAllUI()
    {
        foreach(KeyValuePair<string, SAM_UIBase> ui in _UIStorage)
        {
            ui.Value?.ShowUI();
        }
    }

    public void HideAllUI()
    {
        foreach (KeyValuePair<string, SAM_UIBase> ui in _UIStorage)
        {
            ui.Value?.HideUI();
        }
    }

    private void ClearAllUI()
    {
        _UIStorage.Clear();
    }
    // 보여주고
    // 숨기고
    // 음..
   
}
