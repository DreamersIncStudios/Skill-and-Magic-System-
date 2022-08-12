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
        public void Deavctivate(BaseCharacter Target, int amount);
        public bool ActivateOnChance(int chance);
      
    }
    public interface IOnHitEffect { 
        public void OnHit(BaseCharacter Target, int amount, int chance);
 
    }
    public interface IOnKillEffect
    {
        public void OnKill(BaseCharacter Target, int amount, int chance);
 
    }
    public interface IOnCommand { 
        public float Delay { get; }
        public void OnCommand(BaseCharacter Target, int Amount);
    }
    public interface IOnEequip { 
        public void OnEquip(BaseCharacter Target);
        public void OnUnequip(BaseCharacter Target);
    }
    public interface IOnPlayerDeath { 
        public void OnPlayerDeath(BaseCharacter Target,int amount, int chance);
   
    }

    public interface IOnTimeEffect {
        public void OnTimer(BaseCharacter Target , int amount, float interval);
        public void CancelTimer();
     
    }
    public interface IOnGetHit { 
        public void OnGetHit(BaseCharacter Target);
  
    }
    public interface IOnCommandHit
    {
        public float Delay { get; }
        public void OnCommandHit(BaseCharacter Target, int Amount);
    
    }
    public interface IOnCommandTimer
    {
        public float Duration { get; }
        public void OnCommandAddTimer(BaseCharacter Target, int Amount);
        public void CancelTimerCommand();
   
    }
    public enum Targets { Self, TeamMember, Target, AOE, Projectile }
    public enum TriggerTypes { OnCommand, OnHit, OnGetHit, OnKill, OnPlayerDeath, OnTimer, OnEquip, OnCommandTimer, OnCommandOnHit }

}

