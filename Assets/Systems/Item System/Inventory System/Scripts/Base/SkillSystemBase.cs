using System.Collections.Generic;
using SkillMagicSystem;

namespace Dreamers.InventorySystem.Base
{
    [System.Serializable]
    public class SkillSystemBase {
        public List<Magic> MagicInventory;
        public List<Skill> SkillInventory;

        public List<BaseAbility> EquipSkillMagic;
    }
}