using Dreamers.Global;
using Dreamers.InventorySystem.Base;
using Dreamers.ModalWindows;
using SkillMagicSystem;
using Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dreamers.InventorySystem.UISystem
{
    public class DisplayCharacterUI : MonoBehaviour
    {
        UIManager Manager;
        [SerializeField] GameObject baseUI;
        bool OpenCloseMenu => Input.GetKeyUp(KeyCode.I) || Input.GetKeyUp(KeyCode.JoystickButton7);
        bool OpenCloseMagic => Input.GetKeyUp(KeyCode.P) || Input.GetKeyUp(KeyCode.JoystickButton9);
        bool Command => Input.GetKeyUp(KeyCode.M) || Input.GetKeyUp(KeyCode.JoystickButton9);

        [SerializeField] Canvas getCanvas;
        public bool Displayed { get; private set; }
        InventoryBase Inventory => character.GetComponent<CharacterInventory>().Inventory;
        SkillSystemBase SkillInventory => character.GetComponent<CharacterInventory>().skillSystem;
        [SerializeField] List<MenuButtons> menuItems;
        private void Awake()
        {
            Manager = UIManager.instance;
        }
      
        // Update is called once per frame
        void Update()
        {
            if (OpenCloseMenu)
            {
                if (!character)
                    character = FindObjectOfType<PlayerCharacter>();
                if (!Displayed)
                {
                    OpenCharacterMenu(Inventory);
                    baseUI.SetActive(false); //TODO add fade out 
                }
                else
                {
                    CloseCharacterMenu();
                    baseUI.SetActive(true);//TODO add fade In

                }
            }
            if (OpenCloseMagic)
            {
                if (!character)
                    character = FindObjectOfType<PlayerCharacter>();
                if (!Displayed)
                {
                    Displayed = true;
                    CreateSkillMagic();
                    baseUI.SetActive(false); //TODO add fade out 
                }
                else
                {
                    Displayed = false;
                    CloseCharacterMenu();
                    baseUI.SetActive(true);//TODO add fade In

                }
            }
            if (Command)
            {
                if (!character)
                    character = FindObjectOfType<PlayerCharacter>();
                if (!Displayed)
                {
                    Displayed = true;
                    CreateCommandMenu(character);
                }
                else
                {
                    Displayed = false;
                    CloseCharacterMenu();

                }
            }
        }



        public void CloseCharacterMenu()
        {
            foreach (Transform child in getCanvas.transform)
            {
                Destroy(child.gameObject);
            }

            Displayed = false;
        }

       [SerializeField] BaseCharacter character;

        void CreateMenu()
        {
            Manager = UIManager.instance;
            CreateSideMenu();
            CharacterStatModal characterStat = Instantiate(Manager.StatsWindow, getCanvas.transform).GetComponent<CharacterStatModal>();
            characterStat.ShowAsCharacterStats(character);
            ItemModalWindow items = Instantiate(Manager.InventoryWindow, getCanvas.transform).GetComponent<ItemModalWindow>();
            items.ShowAsCharacterInventory(character.GetComponent<CharacterInventory>());
        }
        public void OpenCharacterMenu(InventoryBase inventory)
        {

            CreateMenu();
            Displayed = true;
        }

        void CreateSkillMagic() { 
            Manager = UIManager.instance;
            SimpleSpellModal spellModal = Instantiate(Manager.MagicSkillWindow, getCanvas.transform).GetComponent<SimpleSpellModal>();
            List<BaseAbility> inInventory = new List<BaseAbility>();

            spellModal.DisplaySpellsSkills(character, SkillInventory);

        }
        void CreateCommandMenu(BaseCharacter baseCharacter)
        {
            if (Manager == null)
            {
                Manager = UIManager.instance;
            }
            CommandMenuModal spellModal = Instantiate(Manager.CommandMenuWindow, baseUI.transform).GetComponent<CommandMenuModal>();

            spellModal.DisplayCommandMenu( SkillInventory, baseCharacter);

        }

        void CreateSideMenu() {
            ModalMenu menu = Instantiate(Manager.ModalMenuPrefab, getCanvas.transform).GetComponent<ModalMenu>();
            menu.DisplayMenu("Still In Dev", menuItems);
        
        }
        public void OpenOptions() {
            OptionUIPanel optionUI = Instantiate(Manager.optionsWindow, getCanvas.transform).GetComponent<OptionUIPanel>();
        }
    }
}