using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SkillMagicSystem.AbilityEffects;
using Random = UnityEngine.Random;
using Unity.Mathematics;
using Stats;

namespace SkillMagicSystem
{
    [CreateAssetMenu(fileName = "Skill", menuName ="Magic and Skills/Skills")]
    public class Skill : BaseAbility
    {

        public int chance { get; private set; }
        public int Amount { get; private set; }

        public List<BaseEffect> Effects;

        public void AddModStat() { }
        public void RemoveModStat() { }

        public void Activate(BaseCharacter baseCharacter)
        {
            foreach (BaseEffect effect in Effects)
            {
                effect.Activate(baseCharacter, Amount, chance);
            }
        }

        public int2 GridSize;
        public Shape GridShape;
        public Color MapColor { get; private set; }
        public bool Rotatable { get; private set; }
        public Dir dir;


       
        
        public void WriteToTextFile()
        {
            throw new System.NotImplementedException();
        }
    }
}
