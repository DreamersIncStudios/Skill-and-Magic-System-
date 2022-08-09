using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SkillMagicSystem
{
    public interface Ability
    {
        string Name { get; }
    }


    [Serializable]
    public class PassiveAbility : Ability
    {

        public PassiveAbility(string name,  Action onHit = null, Action onGotHit = null, Action OnKillEnemy = null, Action onPlayerDeath = null)
        {
            Name = name;
            this.OnGotHit += onGotHit;
            this.OnHit += onHit;
            this.OnKillEnemy += OnKillEnemy;
            this.OnPlayerTeamDeath += onPlayerDeath;
        }
        public string Name { get; private set; }

        public Action OnHit;
        public Action OnGotHit;
        public Action OnKillEnemy;
        public Action OnPlayerTeamDeath;


        public void AddModStat() { }
        public void RemoveModStat() { }

    }

    [Serializable]
    public class ActiveAbillity : Ability
    {
        public ActiveAbillity(string name, Action OnAbilityTrigger, float chance)
        {
            Name = name;
            this.chance = chance;
            this.OnAbilityTrigger += OnAbilityTrigger;
        }

        public string Name { get; private set; }
        public Action OnAbilityTrigger;
        public float chance { get; private set; }

    }
    public enum TriggerTypes { OnCommand,OnHit,OnGetHit, OnKill, OnPlayerDeath, OnTimer, OnEquip}
}
