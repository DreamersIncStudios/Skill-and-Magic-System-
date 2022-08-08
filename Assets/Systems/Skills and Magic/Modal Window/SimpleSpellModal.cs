using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SkillMagicSystem
{
    public class SimpleSpellModal : MonoBehaviour
    {
        public GameObject Parent;

        public GameObject EquippedContentArea;
        public GameObject InInventoryContentArea;
        public Button ItemButton;

        public void DisplaySpellsSkills(List<iBase> Equipped, List<iBase> InInventory)
        {
            Parent.SetActive(true);
            foreach ( iBase basic in Equipped)
            {
                Button item = Instantiate(ItemButton, EquippedContentArea.transform);
                item.GetComponentInChildren<TextMeshProUGUI>().text = $"{basic.Name} \n {basic.Description}";
            }
            foreach (iBase basic in InInventory)
            {
                Button item = Instantiate(ItemButton, InInventoryContentArea.transform);
                item.GetComponentInChildren<TextMeshProUGUI>().text = basic.Name;
            }
        }

    }
}
