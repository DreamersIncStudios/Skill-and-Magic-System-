using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillMagicSystem.AbilityEffects;
using Random = UnityEngine.Random;
using Unity.Mathematics;
using Stats;

namespace SkillMagicSystem
{

    [CreateAssetMenu(fileName = "Magic", menuName = "Magic and Skills/Magic")]
    public class Magic : BaseAbility
    {
        
        public int chance { get; private set; }
        public int Amount { get; private set; }

        public List<BaseEffect> Effects;


        public void AddModStat() { }
        public void RemoveModStat() { }

        public int2 GridSize;
        public Shape GridShape;
        public Color MapColor { get; private set; }
        public bool Rotatable { get; private set; }
        public Dir dir;


        public void Activate(BaseCharacter baseCharacter) {
            foreach (BaseEffect effect in Effects) { 
             effect.Activate(baseCharacter,Amount,chance);
            }
        }

        public void AddToGrid() {  //Todo Add Later
        
        }
        public void RemoveFromGrid() { }
        public void DisplayUI() { }
        public void WriteToTextFile()
        {
            throw new System.NotImplementedException();
        }
    }
}
