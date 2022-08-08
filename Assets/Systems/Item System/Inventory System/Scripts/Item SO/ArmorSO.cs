using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stats;
using Dreamers.InventorySystem.Base;
using Dreamers.InventorySystem.Interfaces;
using Unity.Entities;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Dreamers.InventorySystem.SO
{

    public class ArmorSO : ItemBaseSO, IEquipable, IArmor
    {
        #region variables
        [SerializeField] Quality quality;
        public Quality Quality { get { return quality; } }

        [SerializeField] private GameObject _model;
        public GameObject Model { get { return _model; } }
        [SerializeField] private bool _equipToHuman;
        public bool EquipToHuman { get { return _equipToHuman; } }
        public bool Equipped { get; private set; }

        [SerializeField] private HumanBodyBones _equipBone;
        public HumanBodyBones EquipBone { get { return _equipBone; } }
        [SerializeField] private ArmorType _armorType;
        public ArmorType ArmorType { get { return _armorType; } }
        [SerializeField] private uint _levelRqd;
        public uint LevelRqd { get { return _levelRqd; } }

        [SerializeField] private List<StatModifier> _modifiers;
        public List<StatModifier> Modifiers { get { return _modifiers; } }

        [SerializeField] private float maxDurable;
        public float MaxDurability { get { return maxDurable; } }
        public float CurrentDurablity { get; set; }
        [SerializeField] private bool breakable;
        public bool Breakable { get { return breakable; } }
        [SerializeField] private bool _upgradeable;
        public bool Upgradeable { get { return _upgradeable; } }

        public int SkillPoints { get; set; }
        public int Exprience { get; set; }
        GameObject armorModel;

        public void Equip(BaseCharacter player)
        {
            if (player.Level >= LevelRqd)
            {
                if (Model != null)
                {
                    armorModel = _model = Instantiate(Model);
                    // Consider adding and enum as all character maybe not be human 
                    if (EquipToHuman)
                    {
                        Transform bone = player.GetComponent<Animator>().GetBoneTransform(EquipBone);
                        if (bone)
                        {
                            armorModel.transform.SetParent(bone);
                        }

                    }
                    else
                    {
                        armorModel.transform.SetParent(player.transform);

                    }

                }
              ModCharacterStats(player, Modifiers, true);

            }
        }

        #endregion
        /// <summary>
        /// Equip Item in Inventory to Another Character
        /// </summary>
        /// <param name="characterInventory"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        public bool EquipItem(CharacterInventory characterInventory, BaseCharacter player)
        {
            EquipmentBase Equipment = characterInventory.Equipment;

            if (player.Level >= LevelRqd)
            {
                if (Equipment.EquippedArmor.TryGetValue(this.ArmorType, out _))
                {
                    Equipment.EquippedArmor[this.ArmorType].Unequip(characterInventory, player);
                }
                Equipment.EquippedArmor[this.ArmorType] = this;

                if (Model != null)
                {
                    armorModel = _model = Instantiate(Model);
                    // Consider adding and enum as all character maybe not be human 
                    if (EquipToHuman)
                    {
                        Transform bone = player.GetComponent<Animator>().GetBoneTransform(EquipBone);
                        if (bone)
                        {
                            armorModel.transform.SetParent(bone);
                        }

                    }
                    else
                    {
                        armorModel.transform.SetParent(player.transform);

                    }

                }
                ModCharacterStats(player, Modifiers, true);

                characterInventory.Inventory.RemoveFromInventory(this);
                player.StatUpdate();
                return Equipped = true;
            }
            else { Debug.LogWarning("Level required to Equip is " + LevelRqd + ". Character is currently level " + player.Level);
                return Equipped =false;
            }
        }
        /// <summary>
        /// Equip Item to Self
        /// </summary>
        /// <param name="characterInventory"></param>
        /// <returns></returns>
        public bool EquipItem(CharacterInventory characterInventory)
        {
            return EquipItem(characterInventory, characterInventory.GetComponent<BaseCharacter>());
        }

        /// <summary>
        /// Unequip item from character and return to target inventory
        /// </summary>
        /// <param name="characterInventory"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        public bool Unequip(CharacterInventory characterInventory, BaseCharacter player)
        {
            EquipmentBase Equipment = characterInventory.Equipment;
            characterInventory.Inventory.AddToInventory(this);
            Destroy(armorModel);
            ModCharacterStats(player, Modifiers, false);
            Equipment.EquippedArmor.Remove(this.ArmorType);
            Equipped = false;
            return true;
        }

        /// <summary>
        /// Unequip item from self and return inventory
        /// </summary>
        /// <param name="characterInventory"></param>
        /// <returns></returns>
        public bool Unequip(CharacterInventory characterInventory)
        {
            return Unequip(characterInventory, characterInventory.GetComponent<BaseCharacter>());
        }


        public override void Convert(Entity entity, EntityManager dstManager)
        { }




        public override void Use(CharacterInventory characterInventory,  BaseCharacter player)
        {
            throw new System.NotImplementedException();
        }



        // override object.Equals
        public  bool Equals(ItemBaseSO obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            if (obj.Type != Type)
                return false;

            // TODO: write your implementation of Equals() here

            ArmorSO Armor = (ArmorSO)obj;

            return ItemID == Armor.ItemID  && ItemName == Armor.ItemName && Value == Armor.Value && Modifiers.SequenceEqual( Armor.Modifiers) &&
                Exprience == Armor.Exprience && LevelRqd == Armor.LevelRqd;
        }

        public async void ModCharacterStats(BaseCharacter character, List<StatModifier> Modifiers, bool Add)
        {
            int MP = 1;
            if (!Add)
            {
                MP = -1;
            }
            foreach (StatModifier mod in Modifiers)
            {

                switch (mod.Stat)
                {
                    case AttributeName.Level:
                        Debug.LogWarning("Level Modding is not allowed at this time. Please contact Programming is needed");
                        break;
                    case AttributeName.Strength:
                        character.GetPrimaryAttribute((int)AttributeName.Strength).BuffValue += mod.BuffValue * MP;
                        break;
                    case AttributeName.Vitality:
                        character.GetPrimaryAttribute((int)AttributeName.Vitality).BuffValue += mod.BuffValue * MP;
                        break;
                    case AttributeName.Awareness:
                        character.GetPrimaryAttribute((int)AttributeName.Awareness).BuffValue += mod.BuffValue * MP;
                        break;
                    case AttributeName.Speed:
                        character.GetPrimaryAttribute((int)AttributeName.Speed).BuffValue += mod.BuffValue * MP;
                        break;
                    case AttributeName.Skill:
                        character.GetPrimaryAttribute((int)AttributeName.Skill).BuffValue += mod.BuffValue * MP;
                        break;
                    case AttributeName.Resistance:
                        character.GetPrimaryAttribute((int)AttributeName.Resistance).BuffValue += mod.BuffValue * MP;
                        break;
                    case AttributeName.Concentration:
                        character.GetPrimaryAttribute((int)AttributeName.Concentration).BuffValue += mod.BuffValue * MP;
                        break;
                    case AttributeName.WillPower:
                        character.GetPrimaryAttribute((int)AttributeName.WillPower).BuffValue += mod.BuffValue * MP;
                        break;
                    case AttributeName.Charisma:
                        character.GetPrimaryAttribute((int)AttributeName.Charisma).BuffValue += mod.BuffValue * MP;
                        break;
                    case AttributeName.Luck:
                        character.GetPrimaryAttribute((int)AttributeName.Luck).BuffValue += mod.BuffValue * MP;
                        break;
                }
            }
            await Task.Delay(TimeSpan.FromSeconds(5));
            character.StatUpdate();

        }

    }


    
}