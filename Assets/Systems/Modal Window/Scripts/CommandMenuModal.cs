using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Dreamers.InventorySystem.Base;
using SkillMagicSystem;
using TMPro;
using Stats;
using UnityEngine.TextCore.Text;
using Dreamers.InventorySystem;

public class CommandMenuModal : MonoBehaviour
{
    public  GameObject ContentArea;
    public  Button ItemButton;

    public  void DisplayCommandMenu(SkillSystemBase skillSystemBase, BaseCharacter User) {
        foreach (BaseAbility ability in skillSystemBase.EquipSkillMagic) {
            if (ability.Trigger == TriggerTypes.OnCommand || ability.Trigger == TriggerTypes.OnTimer) 
            {
                Button item = Instantiate(ItemButton, ContentArea.transform);
                item.GetComponentInChildren<TextMeshProUGUI>().text = ability.Name;
                item.onClick.AddListener(() =>
                {
                    Debug.Log($"Open Targeting System for {ability.Name}");
                    List<BaseCharacter> targets = new List<BaseCharacter>();
                    switch (ability.AbilityTarget) {
                        case Targets.Self:
                            ability.Activate(User);
                            break;
                        case Targets.TeamMember:
                            PlayerCharacter[] PC = GameObject.FindObjectsOfType<PlayerCharacter>();
                            foreach (PlayerCharacter pc in PC) { 
                                targets.Add(pc);
                            }
                            break;
                        case Targets.Enemy:
                            EnemyCharacter[] enemy = GameObject.FindObjectsOfType<EnemyCharacter>();
                            foreach (EnemyCharacter pc in enemy)
                            {
                                targets.Add(pc);
                            }
                                break;
                        case Targets.Anyone:
                            PlayerCharacter[] PC2 = GameObject.FindObjectsOfType<PlayerCharacter>();
                            foreach (PlayerCharacter pc in PC2)
                            {
                                targets.Add(pc);
                            }
                            EnemyCharacter[] enemy2 = GameObject.FindObjectsOfType<EnemyCharacter>();
                            foreach (EnemyCharacter pc in enemy2)
                            {
                                targets.Add(pc);
                            }
                            break;
                    }
                    DisplayTargetMenu(targets, ability, User);
                });
            }
        }

    }

    public void DisplayCommandMenu(BaseCharacter user) {
        SkillSystemBase SkillInventory = user.GetComponent<CharacterInventory>().skillSystem;
        DisplayCommandMenu(SkillInventory, user);

    }

    public void DisplayTargetMenu(List<BaseCharacter> targets, BaseAbility ability, BaseCharacter user) {
        Debug.Log("Add transition dotween to smooth");
        foreach (Transform child in ContentArea.transform) {
            Destroy(child.gameObject);
        }
        foreach (var target in targets)
        {
            Button item = Instantiate(ItemButton, ContentArea.transform);
            item.GetComponentInChildren<TextMeshProUGUI>().text = target.Name;
            item.onClick.AddListener(() => {
                Debug.Log($"Cast Ability on  {ability.Name}");

            });
        }
        Button cancel = Instantiate(ItemButton, ContentArea.transform);
        cancel.GetComponentInChildren<TextMeshProUGUI>().text = "Cancel";
        cancel.onClick.AddListener(() => {
            Debug.Log($"Cast Ability on  {ability.Name}");
            Debug.Log("Add transition dotween to smooth");
            foreach (Transform child in ContentArea.transform)
            {
                Destroy(child.gameObject);
            }
            DisplayCommandMenu(user);
        });
    }

}
