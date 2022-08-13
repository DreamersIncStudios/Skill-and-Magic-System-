using Assets.Systems.Global.Function_Timer;
using DreamersInc.DamageSystem.Interfaces;
using Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace SkillMagicSystem.AbilityEffects
{
    [CreateAssetMenu(fileName = "Elemental Effect", menuName = "Magic and Skills/Elemental Effect")]
    public class ElementalEffect : BaseEffect,IOnHitEffect,IOnCommand, IOnTimeEffect
    {
        public Element GetElement { get { return element; } }
        [SerializeField] Element element;
        public float Delay { get { return delay; } }
        [SerializeField] float delay;
        public int ChanceForStatusChange { get { return chanceForStatusChange; } }
        [SerializeField] int chanceForStatusChange;
        [SerializeField] int durationOfStatusChange;
        [SerializeField] float range;

        public void OnHit(BaseCharacter baseCharacter, int amount, int chance)
        {
            if (ActivateOnChance(chance))
            {
                DoElementalDamage(baseCharacter, amount);
            }
            if (StatusChange())
            {
                bool check = new bool();
                switch (GetElement) {
                    case Element.Fire:
                        check = baseCharacter.SetAlteredStatus(AlteredStatus.Burnt);
                        break;
                    case Element.Ice:
                        check = baseCharacter.SetAlteredStatus(AlteredStatus.Frostbite);
                        break;
                    case Element.Holy:
                        check = baseCharacter.SetAlteredStatus(AlteredStatus.Blinded);
                        break;
                    case Element.Wind:
                        check = baseCharacter.SetAlteredStatus(AlteredStatus.Electrocuted);
                        break;
                }

                if (check)
                {
                    Debug.Log("Status Effect applied to character IE Burned Frostbite Etc etc ");
                    //TODO add material Change or VFX
                    OnTimer(baseCharacter, 4, 4);
                    CancelTimer(400);
                }
            }
        }


        public void OnCommand(BaseCharacter baseCharacter, int Amount)
        {
            DoElementalDamage((BaseCharacter)baseCharacter, Amount);
            //Todo Add Delay For animation VFX;
        }
        FunctionTimer intervalTimer;

        public void OnTimer(BaseCharacter baseCharacter, int amount, float interval)
        {
            intervalTimer = FunctionTimer.Create(() =>
            {
                DoElementalDamage(baseCharacter, amount);

            }
             , interval, GetElement.ToString(), true);
        }

        public void CancelTimer() //Todo add more description
        {
            FunctionTimer.StopTimer(GetElement.ToString());

        }
        public void CancelTimer(float delay) //Todo add more description
        {
            FunctionTimer.Create(() =>
            {
                FunctionTimer.StopTimer(GetElement.ToString());
            }, delay, "Cancel Timer Delay");

        }


        void DoElementalDamage(BaseCharacter baseCharacter, int Amount)
        {
            switch (GetTarget)
            {
                case Targets.Self:
                case Targets.Enemy:
                case Targets.TeamMember:

                    baseCharacter.TakeDamage(Amount, TypeOfDamage.Magic, GetElement);

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
                            character.TakeDamage(Amount, TypeOfDamage.Magic, GetElement);
                        }
                    }
                    break;
            }

        }

        public bool StatusChange() {
            int rndNum = Random.Range(0, 100);
            return ChanceForStatusChange >= rndNum;
        }
    }
}
