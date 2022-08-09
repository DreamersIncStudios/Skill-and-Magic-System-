using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillMagicSystem.AbilityEffects;
using Random = UnityEngine.Random;
using Unity.Mathematics;

namespace SkillMagicSystem
{

    [CreateAssetMenu(fileName = "Magic", menuName = "Magic and Skills/Magic")]
    public class Magic : BaseAbility
    {
        
        public float chance { get; private set; }
        public List<EffectsSO> effects;

        public void AddModStat() { }
        public void RemoveModStat() { }
        public Action OnHit;
        public Action OnGotHit;
        public Action OnKillEnemy;
        public Action OnPlayerTeamDeath;
        public Action tempAction;
        public int2 GridSize;
        public Shape GridShape;
        public Color MapColor { get; private set; }
        public bool Rotatable { get; private set; }
        public Dir dir;

        public PassiveAbility AddPassiveAbility()
        {
            foreach (EffectsSO effect in effects) {
                tempAction = delegate {
                    int rndNum= Random.Range(0, 100);
                    if (chance>= rndNum) {
                        Debug.Log("DO Stuff");
                    }
                };
                switch (effect.trigger)
                {
                    case TriggerTypes.OnHit:
                        OnHit += tempAction;
                        break;
                    case TriggerTypes.OnGetHit:
                        OnGotHit += tempAction;
                        break;
                    case TriggerTypes.OnKill:
                        OnKillEnemy += tempAction;
                        break;
                    case TriggerTypes.OnPlayerDeath:
                        OnPlayerTeamDeath += tempAction;
                        break;
                }
            }
            var temp = new PassiveAbility(Name, OnHit, OnGotHit, OnKillEnemy, OnPlayerTeamDeath);

            return temp;
        }

        Action activeEffects;
        public ActiveAbillity AddActiveAbility()
        {

            foreach (EffectsSO effect in effects)
            {
                if (effect.trigger == TriggerTypes.OnCommand)
                {
                    switch (effect.ActionEffect)
                    {
                        case Effects.Heal:
                            activeEffects += effect.Heal;
                            break;
                    }
                }
            }
            var temp = new ActiveAbillity(Name, activeEffects, .9f);
            return temp;
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
