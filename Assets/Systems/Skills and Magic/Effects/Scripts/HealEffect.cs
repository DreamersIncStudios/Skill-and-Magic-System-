using Assets.Systems.Global.Function_Timer;
using DreamersInc.DamageSystem.Interfaces;
using Stats;
using System.Collections.Generic;
using UnityEngine;
namespace SkillMagicSystem.AbilityEffects
{

    [CreateAssetMenu(fileName = "Healing Effect", menuName = "Magic and Skills/Heal Effect")]

    public class HealEffect : ScriptableObject, iEffect, IOnHitEffect, IOnKillEffect, IOnCommand, IOnTimeEffect, IOnPlayerDeath
    {
        public TriggerTypes GetTrigger { get { return trigger; } }
        [SerializeField]TriggerTypes trigger;
        public Targets GetTarget { get { return target; } }
        [SerializeField] Targets target;
        
        public float Duration { get { return duration; } }
        [SerializeField] float duration = 3;
        public GameObject EffectVFX { get { return effectVFX; } }
        [SerializeField] GameObject effectVFX;
        [SerializeField] float range;
        [SerializeField] float intervalTime;

        public void Activate(BaseCharacter baseCharacter,int amount = 0, int chance =100) {
            switch (GetTrigger) {
                case TriggerTypes.OnCommand:
                    OnCommand(baseCharacter,amount);
                    break;
                case TriggerTypes.OnHit:
                    OnHit(baseCharacter, amount, chance);
                    break;
                case TriggerTypes.OnKill:
                    OnKill(baseCharacter,amount,chance);
                    break;
                case TriggerTypes.OnTimer:
                    OnTimer(baseCharacter, amount, intervalTime);
                    break;
                case TriggerTypes.OnPlayerDeath:
                    OnPlayerDeath(baseCharacter,amount,chance);
                    break;

            }
        
        }

        public void OnHit(BaseCharacter baseCharacter, int amount, int chance)
        {
            if (ActivateOnChance(chance)){
               Heal(baseCharacter,amount);
            }
        }

        public void OnKill(BaseCharacter baseCharacter, int amount, int chance)
        {
            if (ActivateOnChance(chance))
            {
                Heal(baseCharacter, amount);

            }
        }

        public void OnCommand(BaseCharacter baseCharacter, int Amount)
        {
            Heal(baseCharacter, Amount);

        }
        FunctionTimer intervalTimer;
        public void OnTimer(BaseCharacter baseCharacter, int amount, float interval)
        {
            intervalTimer = FunctionTimer.Create(() =>
            {
                Heal(baseCharacter, amount); 
                
            }
                , interval, "Heal",true);
        }
        public void CancelTimer() {
            FunctionTimer.StopTimer("Heal");
        }
        public void OnPlayerDeath(BaseCharacter baseCharacter, int amount, int chance)
        {
            if (ActivateOnChance(chance))
            {
                Heal(baseCharacter, amount);
            }
        }

        public bool ActivateOnChance( int chance) {
            int rndNum = Random.Range(0, 100);
            return chance >= rndNum;
          
        }

        void Heal(BaseCharacter baseCharacter, int Amount) {
            switch (GetTarget)
            {
                case Targets.Self:
                case Targets.Target:
                case Targets.TeamMember:

                    baseCharacter.TakeDamage(Amount, TypeOfDamage.Recovery, Element.Holy);
                    break;
                case Targets.AOE:
                    var Cols = Physics.OverlapSphere(baseCharacter.gameObject.transform.position, range);
                    foreach (Collider coll in Cols)
                    {
                        if (coll.GetComponent<BaseCharacter>())
                        {
                            var character = coll.GetComponent<BaseCharacter>();
                            character.TakeDamage(Amount, TypeOfDamage.Recovery, Element.Holy);
                        }
                    }
                    break;
            }

        }


    }
}
