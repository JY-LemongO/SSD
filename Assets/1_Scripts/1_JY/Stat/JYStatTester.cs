using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JYStatTester : MonoBehaviour
{
    public JYEnemyTest _testEnemey;

    [Header("Initializer")]
    public Button _startAttackBtn;

    [Header("Player Stat Infos")]
    public TMP_Text _atkValueText;
    public TMP_Text _defValueText;

    [Header("Equip Item Buttons")]
    public Button _equipSword1Btn;
    public Button _equipSword2Btn;    
    public Button _equipArmor1Btn;
    public Button _equipArmor2Btn;    
    public Button _equipRing1Btn;
    public Button _equipRing2Btn;    

    [Header("Unequip Item Buttons")]
    public Button _unequipSword1Btn;
    public Button _unequipSword2Btn;    
    public Button _unequipArmor1Btn;
    public Button _unequipArmor2Btn;    
    public Button _unequipRing1Btn;
    public Button _unequipRing2Btn;

    [Header("Enemy")]
    public Slider _enemyHPSlider;
    public TMP_Text _enemyHPValueText;

    private bool _isStartAttack = false;

    private void Start()
    {
        _atkValueText.text = $"{(int)JYStatManager.Instance.StatATK.Value}";
        _defValueText.text = $"{(int)JYStatManager.Instance.StatDEF.Value}";

        JYStatManager.Instance.StatATK.onStatValueChanged += (value) => _atkValueText.text = $"{(int)value}";
        JYStatManager.Instance.StatDEF.onStatValueChanged += (value) => _defValueText.text = $"{(int)value}";
        _testEnemey.onEnemyHPChanged += (value) => _enemyHPValueText.text = $"{(int)value}/{(int)_testEnemey.MaxHP}";
        _testEnemey.onEnemyHPChanged += (value) => _enemyHPSlider.value = value / _testEnemey.MaxHP;

        EventSubscribe();
    }

    private void EventSubscribe()
    {
        _startAttackBtn.onClick.AddListener(OnStartAttack);

        _equipSword1Btn.onClick.AddListener(() => OnEquipSwordBtn(0));
        _equipSword2Btn.onClick.AddListener(() => OnEquipSwordBtn(1));
        _equipArmor1Btn.onClick.AddListener(() => OnEquipArmorBtn(0));
        _equipArmor2Btn.onClick.AddListener(() => OnEquipArmorBtn(1));
        _equipRing1Btn.onClick.AddListener(() => OnEquipRingBtn(0));
        _equipRing2Btn.onClick.AddListener(() => OnEquipRingBtn(1));        
    }

    public void OnStartAttack()
    {
        if (_isStartAttack) return;

        _isStartAttack = true;
        StartCoroutine(Co_StartAttackEnemey());
    }

    public void OnEquipSwordBtn(int index)
    {
        switch (index)
        {
            case 0:
                JYStatManager.Instance.StatATK.SetBonusValue("Sword", 10);
                break;
            case 1:
                JYStatManager.Instance.StatATK.SetBonusValue("Sword", 20);
                break;            
        }        
    }

    public void OnEquipArmorBtn(int index)
    {
        switch (index)
        {
            case 0:
                JYStatManager.Instance.StatDEF.SetBonusValue("Armor", 10);
                break;
            case 1:
                JYStatManager.Instance.StatDEF.SetBonusValue("Armor", 20);
                break;            
        }
    }

    public void OnEquipRingBtn(int index)
    {
        switch (index)
        {
            case 0:
                JYStatManager.Instance.StatATK.SetBonusValue("Ring", 5);
                JYStatManager.Instance.StatDEF.SetBonusValue("Ring", 5);
                break;
            case 1:
                JYStatManager.Instance.StatATK.SetBonusValue("Ring", 10);
                JYStatManager.Instance.StatDEF.SetBonusValue("Ring", 10);
                break;            
        }
    }

    public void OnUnequipSwordBtn(int index)
    {
        switch (index)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }

    public void OnUnequipArmorBtn(int index)
    {
        switch (index)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }

    public void OnUnequipRingBtn(int index)
    {
        switch (index)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }

    private IEnumerator Co_StartAttackEnemey()
    {
        while (true)
        {
            _testEnemey.GetDamage(JYStatManager.Instance.StatATK.Value);

            yield return new WaitForSeconds(0.5f);

        }
    }
}
