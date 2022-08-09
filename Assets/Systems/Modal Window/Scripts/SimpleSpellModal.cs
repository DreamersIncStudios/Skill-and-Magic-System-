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
    
        public void DisplaySpellsSkills( BaseCharacter player , InventoryBase Inventory)
        {
         
            RefreshEquipped(player, Inventory);
            RefreshInInventory(player, Inventory);

        }

        public void RefreshEquipped( BaseCharacter player, InventoryBase Inventory) {
            foreach (Transform child in EquippedContentArea.transform)
            {
                Destroy(child.gameObject);
            }
            AbilityManager manager = player.GetComponent<AbilityManager>();
            List<BaseAbility> Equipped = manager.AddedAbilities;
            foreach (BaseAbility basic in Equipped)
            {
                Button item = Instantiate(ItemButton, EquippedContentArea.transform);
                item.GetComponentInChildren<TextMeshProUGUI>().text = basic.Name;
                item.onClick.AddListener(() => {
                    Debug.Log($"Remove {basic.Name} to player ability list");
                    manager.AddedAbilities.Remove(basic);
                    if (basic.Magic)
                        Inventory.Magics.Add((Magic)basic);
                    else
                        Inventory.Skills.Add((Skill)basic);
                    Destroy(item.gameObject);
                    RefreshInInventory(player, Inventory);
                });
            }
        }
        public void RefreshInInventory(BaseCharacter player, InventoryBase Inventory)
        {
            foreach (Transform child in InInventoryContentArea.transform)
            {
                Destroy(child.gameObject);
            }

            AbilityManager manager = player.GetComponent<AbilityManager>();
            List<BaseAbility> InInventory = new List<BaseAbility>();

            foreach (BaseAbility bases in Inventory.Magics)
            {
                InInventory.Add(bases);
            }
            foreach (BaseAbility bases in Inventory.Skills)
            {
                InInventory.Add(bases);
            }

            foreach (BaseAbility basic in InInventory)
            {
                Button item = Instantiate(ItemButton, InInventoryContentArea.transform);
                item.GetComponentInChildren<TextMeshProUGUI>().text = basic.Name;
                item.onClick.AddListener(() => {
                    Debug.Log($"Add {basic.Name} to player ability list");
                    if (basic.Magic)
                        Inventory.Magics.Remove((Magic)basic);
                    else
                        Inventory.Skills.Remove((Skill)basic);
                    manager.AddedAbilities.Add(basic);
                    Destroy(item.gameObject);
                    RefreshEquipped(player, Inventory);
                });
            }

        }
    }
}
