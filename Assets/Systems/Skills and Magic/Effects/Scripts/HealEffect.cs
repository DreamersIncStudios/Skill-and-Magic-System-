using Assets.Systems.Global.Function_Timer;
using DreamersInc.DamageSystem.Interfaces;
using Stats;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace SkillMagicSystem.AbilityEffects
{
    [CreateAssetMenu(fileName = "Healing Effect", menuName = "Magic and Skills/Heal Effect")]

    public class HealEffect : BaseEffect, IOnHitEffect, IOnKillEffect, IOnCommand, IOnTimeEffect, IOnPlayerDeath
    {

        [SerializeField] float range;
        [SerializeField] float intervalTime;
        public float Delay { get { return delay; } }
        [SerializeField] float delay;

        public override void Activate(BaseCharacter baseCharacter,int amount = 0, int chance =100) {
            base.Activate(baseCharacter,amount,chance);
            Debug.Log("Called at SO");
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
            if ( ActivateOnChance(chance)){
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

        

        void Heal(BaseCharacter baseCharacter, int Amount) {
            switch (GetTarget)
            {
                case Targets.Self:
                case Targets.Target:
                case Targets.TeamMember:

                    baseCharacter.TakeDamage(Amount, TypeOfDamage.Recovery, Element.Holy);

                    if (EffectVFX != null)
                    {
                        //Todo add position offset for VFX
                        var spawned = Instantiate(EffectVFX, baseCharacter.transform.position, Quaternion.identity).GetComponent<VisualEffect>(); // figure out how to postion 
                        spawned.transform.SetParent(baseCharacter.transform, false);
                        spawned.Play();
                        Destroy(spawned.gameObject, Duration);
                    }
                 
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
