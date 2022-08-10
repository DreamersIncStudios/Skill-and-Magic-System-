using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Dreamers.InventorySystem.Base;
using SkillMagicSystem;
using TMPro;
using Stats;

public class CommandMenuModal : MonoBehaviour
{
    public  GameObject ContentArea;
    public  Button ItemButton;

    public  void DisplayCommandMenu(SkillSystemBase skillSystemBase, BaseCharacter baseCharacter) {
        foreach (BaseAbility ability in skillSystemBase.EquipSkillMagic) {
            if (ability.Trigger == TriggerTypes.OnCommand || ability.Trigger == TriggerTypes.OnTimer) 
            {
                Button item = Instantiate(ItemButton, ContentArea.transform);
                item.GetComponentInChildren<TextMeshProUGUI>().text = ability.Name;
                item.onClick.AddListener(() =>
                {

                    ability.Activate(baseCharacter);
                 
                });
            }
        }

    }
}
