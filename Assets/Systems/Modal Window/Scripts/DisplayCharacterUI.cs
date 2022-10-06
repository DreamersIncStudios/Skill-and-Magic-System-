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
        bool Command => Input.GetKey(KeyCode.M) || Input.GetKey(KeyCode.JoystickButton4);

        [SerializeField] Canvas getCanvas;
        public bool Displayed { get; private set; }
        InventoryBase Inventory => character.GetComponent<CharacterInventory>().Inventory;
        SkillSystemBase SkillInventory => character.GetComponent<CharacterInventory>().magicSkillSystem;
        [SerializeField] List<MenuButtons> menuItems;
        private void Awake()
        {
            Manager = UIManager.instance;
        }
      
        // Update is called once per frame
        void Update()
        {
            if (!character)
                character = FindObjectOfType<PlayerCharacter>();
            if (OpenCloseMenu)
            {
           
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

    
        }


        public void Clear() {
            foreach (Transform child in getCanvas.transform)
            {
                Destroy(child.gameObject);
            }

        }
        public void CloseCharacterMenu()
        {
           Clear();
            Displayed = false;
        }

       [SerializeField] BaseCharacter character;

       public  void CreateMenu()
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

       public void CreateSkillMagic() { 
            if(Manager==null)
                Manager = UIManager.instance;
          Clear();
            CreateSideMenu();

            SimpleSpellModal spellModal = Instantiate(Manager.MagicSkillWindow, getCanvas.transform).GetComponent<SimpleSpellModal>();
            spellModal.DisplaySpellsSkills(character, SkillInventory);

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