using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Stats;
using Dreamers.InventorySystem.Base;
using Dreamers.InventorySystem;

namespace SkillMagicSystem
{
    public class SimpleSpellModal : MonoBehaviour
    {

        public GameObject EquippedContentArea;
        public GameObject InInventoryContentArea;
        public Button ItemButton;
    
        public void DisplaySpellsSkills( BaseCharacter player , SkillSystemBase Inventory)
        {
         
            RefreshEquipped(player, Inventory);
            RefreshInInventory(player, Inventory);

        }

        public void RefreshEquipped( BaseCharacter player, SkillSystemBase Inventory) {
            foreach (Transform child in EquippedContentArea.transform)
            {
                Destroy(child.gameObject);
            }
            foreach (Magic basic in Inventory.EquippedMagic)
            {
                Button item = Instantiate(ItemButton, EquippedContentArea.transform);
                item.GetComponentInChildren<TextMeshProUGUI>().text = basic.Name;
                item.onClick.AddListener(() => {
                    Debug.Log($"Remove {basic.Name} to player ability list");
                    Inventory.EquippedMagic.Remove(basic);
                        Inventory.MagicInventory.Add(basic);
                    Destroy(item.gameObject);
                    RefreshInInventory(player, Inventory);
                });
            }
            foreach (Skill basic in Inventory.EquippedSkill)
            {
                Button item = Instantiate(ItemButton, EquippedContentArea.transform);
                item.GetComponentInChildren<TextMeshProUGUI>().text = basic.Name;
                item.onClick.AddListener(() => {
                    Debug.Log($"Remove {basic.Name} to player ability list");
                    Inventory.EquippedSkill.Remove(basic);
                        Inventory.SkillInventory.Add((Skill)basic);
                    Destroy(item.gameObject);
                    RefreshInInventory(player, Inventory);
                });
            }
        }
        public void RefreshInInventory(BaseCharacter player, SkillSystemBase Inventory)
        {
            foreach (Transform child in InInventoryContentArea.transform)
            {
                Destroy(child.gameObject);
            }

        
            List<BaseAbility> InInventory = new List<BaseAbility>();

            foreach (BaseAbility bases in Inventory.MagicInventory)
            {
                InInventory.Add(bases);
            }
            foreach (BaseAbility bases in Inventory.SkillInventory)
            {
                InInventory.Add(bases);
            }

            foreach (BaseAbility basic in InInventory)
            {
                Button item = Instantiate(ItemButton, InInventoryContentArea.transform);
                item.GetComponentInChildren<TextMeshProUGUI>().text = basic.Name;
                item.onClick.AddListener(() => {
                    if (basic.CanAdd(player.Level))
                    {
                        Debug.Log($"Add {basic.Name} to player ability list");
                        if (basic.Magic)
                        {
                            Inventory.MagicInventory.Remove((Magic)basic);
                            Inventory.EquippedMagic.Add((Magic)basic);

                        }
                        else
                        {
                            Inventory.SkillInventory.Remove((Skill)basic);
                            Inventory.EquippedSkill.Add((Skill)basic);
                        }
                        Destroy(item.gameObject);
                        RefreshEquipped(player, Inventory);
                    }
                    else {
                        Debug.Log($"Unable to equipped {basic.Name} to player ability list. Player doesn't meet reqd level {basic.ReqdLevel}" +
                            $" This is be implemented as Modal Window in future");
                    }
                });
            }

        }
    }
}
