using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Dreamers.InventorySystem;
using SkillMagicSystem;
using TMPro;
using Stats;
using System;
//using UnityStandardAssets.CrossPlatformInput;

namespace Dreamers.ModalWindows
{
    public class QuickAccessMenu : MonoBehaviour
    {
        public RectTransform Base;
        public GameObject ContentArea;
        public GameObject ButtonPrefab;
        CharacterInventory inventory;
        PlayerCharacter character;
        bool casted;

        bool casting => Input.GetAxis("Target Trigger") > .3f && !casted; //TODO rename Target Trigger
        bool shown = false;
   

        private void Update()
        {
            if(!shown && casting)
                DisplayQuickAccessMenu();

            if (shown && !casting)
            {
                HideQuickAccesMenu();
                casted = false;
            }
        }

        public void DisplayQuickAccessMenu() {
            if (!inventory)
            {
                var Player = GameObject.FindGameObjectWithTag("Player");
                inventory = Player?.GetComponent<CharacterInventory>();
                character =Player?.GetComponent<PlayerCharacter>();
            }
            Base.DOAnchorPosY(-250, .75f);
            shown = true;
        }
        public void HideQuickAccesMenu() { 
            Base.DOAnchorPosY(-800, .75f);
            shown = false;
            DisplayBase();

        }
        public void DisplaySpells() {
             ClearContentArea();
            foreach (Magic spell in inventory.magicSkillSystem.EquippedMagic)
            {
                Button buttonSpell = Instantiate(ButtonPrefab, ContentArea.transform).GetComponent<Button>();
                TextMeshProUGUI spellText = buttonSpell.GetComponentInChildren<TextMeshProUGUI>();
                spellText.text = spell.Name;
                buttonSpell.onClick.AddListener(() =>
                {
                    if (spell.CanCast(character))
                        Debug.Log($"Casting spell {spell.Name}");
                    else
                        Debug.Log($"Can not cast spell {spell.Name}");

                });
            }
            CreateBackButton();
            Debug.Log(inventory.magicSkillSystem.EquippedMagic.Count);
        }
        public void DisplayItems() {
             ClearContentArea();
            CreateBackButton();

        }
        public void DisplayAbilities() {
             ClearContentArea();
            Debug.Log(inventory.magicSkillSystem.EquippedSkill.Count);
            CreateBackButton();
        }
        public void DisplaySummons() { 
            ClearContentArea();
            CreateBackButton();
        }
        public void DisplayBase() { 
            ClearContentArea();
            Button spellsButton = Instantiate(ButtonPrefab, ContentArea.transform).GetComponent<Button>();
            spellsButton.GetComponentInChildren<TextMeshProUGUI>().text = "Spells";
            spellsButton.onClick.AddListener(DisplaySpells);

            Button itemsButton = Instantiate(ButtonPrefab, ContentArea.transform).GetComponent<Button>();
            itemsButton.GetComponentInChildren<TextMeshProUGUI>().text = "Items";
            itemsButton.onClick.AddListener(DisplayItems);

            Button skillsButton = Instantiate(ButtonPrefab, ContentArea.transform).GetComponent<Button>();
            skillsButton.GetComponentInChildren<TextMeshProUGUI>().text = "Abilities";
            skillsButton.onClick.AddListener(DisplayAbilities);

            Button summonsButton = Instantiate(ButtonPrefab, ContentArea.transform).GetComponent<Button>();
            summonsButton.GetComponentInChildren<TextMeshProUGUI>().text = "Summons";
            summonsButton.onClick.AddListener(DisplaySummons);

        }
        void ClearContentArea() { 

            foreach (Transform child in ContentArea.transform)
            {
                Destroy(child.gameObject);
            }
        
        }
        void CreateBackButton() {
            Button backButton = Instantiate(ButtonPrefab, ContentArea.transform).GetComponent<Button>();
            TextMeshProUGUI backText = backButton.GetComponentInChildren<TextMeshProUGUI>();
            backText.text = "Back";
            backButton.onClick.AddListener(DisplayBase);
        }

    }
}
