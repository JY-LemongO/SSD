using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance => _instance;
    private static UIManager _instance;

    [field: SerializeField] public Transform OpenedUITrs { get; private set; }
    [field: SerializeField] public Transform ClosedUITrs { get; private set; }

    public int CurrentChildCount => transform.childCount;

    [SerializeField] private List<UIBase> _openedUIList = new();
    [SerializeField] private List<UIBase> _closedUIList = new();

    private UIBase _frontUI;

    private void Awake()
    {
        if (!_instance)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            DestroyImmediate(gameObject);
    }

    // 이미 최초 1회 이상 Open된 경우, openedUIList나 closedUIList 에서 찾아서 보내준다.
    public bool IsAlreadySpawned<T>(out T ui) where T : UIBase
    {
        ui = null;

        foreach (var openedUI in _openedUIList)
        {
            if (openedUI is T)
            {
                Debug.Log($"{typeof(T).Name} UI가 이미 Open되어있습니다.");

                ui = openedUI as T;
                SetFrontUI(ui);
                return true;
            }
        }

        foreach (var closedUI in _closedUIList)
        {
            if (closedUI is T)
            {
                Debug.Log($"{typeof(T).Name} UI가 이미 Close되어있습니다.");
                ui = closedUI as T;

                _closedUIList.Remove(closedUI);
                _openedUIList.Add(closedUI);

                closedUI.transform.SetParent(OpenedUITrs);
                closedUI.gameObject.SetActive(true);
                return true;
            }
        }

        Debug.Log($"{typeof(T).Name} UI가 Open 된 적 없습니다.");

        return false;
    }

    public T OpenPopupUI<T>(string path = null) where T : UIBase
    {
        if (string.IsNullOrEmpty(path))
            path = typeof(T).Name;

        T popup = null;

        if (!IsAlreadySpawned(out popup))
        {
            T uiRef = Resources.Load<T>($"1_JY/Prefabs/{path}");

            popup = Instantiate(uiRef, OpenedUITrs);
            popup.gameObject.SetActive(true);
            popup.name = path;

            _openedUIList.Add(popup);
            _frontUI = popup;
        }

        if (popup == null)
        {
            Debug.LogError($"{path}이름을 가진 UI가 없습니다.");
            return null;
        }

        return popup;
    }

    public T CreateItemUI<T>(Transform parent, string path = null) where T : UIBase
    {
        T item = null;

        return item;
    }

    public void SetFrontUI(UIBase ui)
    {
        _openedUIList.Remove(ui);
        _openedUIList.Add(ui);
        _frontUI = ui;

        Debug.Log(CurrentChildCount);
        ui.transform.SetSiblingIndex(CurrentChildCount);
    }

    public void CloseFrontUI()
    {
        if (_frontUI == null)
        {
            Debug.Log("Front UI가 없습니다.");
            return;
        }

        CloseUI(_frontUI);
    }

    public void CloseUI(UIBase ui, bool isItem = false)
    {
        ui.transform.SetParent(ClosedUITrs);
        ui.gameObject.SetActive(false);

        _openedUIList.Remove(ui);
        _closedUIList.Add(ui);

        if (_frontUI == ui)
        {
            if (_openedUIList.Count > 0)
                _frontUI = _openedUIList[_openedUIList.Count - 1];
            else
                _frontUI = null;
        }
    }

    public void CloseAllUI()
    {
        foreach (var ui in _openedUIList)
        {
            ui.transform.SetParent(ClosedUITrs);
            ui.gameObject.SetActive(false);

            _closedUIList.Add(ui);
        }
        _openedUIList.Clear();

        _frontUI = null;
    }

    public void Dispose()
    {
        _openedUIList.Clear();
        _closedUIList.Clear();
    }
}
