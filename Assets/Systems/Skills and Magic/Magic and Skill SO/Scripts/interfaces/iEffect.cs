using Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillMagicSystem.AbilityEffects
{
    public interface iEffect 
    {
        public TriggerTypes GetTrigger { get; }
        public Targets GetTarget { get; }

        public GameObject EffectVFX { get; }
        public float Duration { get; }
        public void Activate(BaseCharacter baseCharacter, int amount, int chance);

    }
    public interface IOnHitEffect { 
        public void OnHit(BaseCharacter baseCharacter, int amount, int chance);
        bool ActivateOnChance(int chance);
    }
    public interface IOnKillEffect
    {
        public void OnKill(BaseCharacter baseCharacter, int amount, int chance);
         bool ActivateOnChance(int chance);
    }
    public interface IOnCommand { 
        public void OnCommand(BaseCharacter baseCharacter, int Amount);
    }
    public interface IOnEequip { 
        public void OnEquip(BaseCharacter baseCharacter);
    }
    public interface IOnPlayerDeath { 
        public void OnPlayerDeath(BaseCharacter baseCharacter,int amount, int chance);
        bool ActivateOnChance(int chance);
    }

    public interface IOnTimeEffect {
        public void OnTimer(BaseCharacter baseCharacter , int amount, float interval);
        public void CancelTimer();
         bool ActivateOnChance(int chance);
    }
    public interface IOnGetHit { 
        public void OnGetHit(BaseCharacter baseCharacter);
        bool ActivateOnChance(int chance);
    }


}

