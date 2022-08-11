using Stats;
using UnityEngine;

namespace SkillMagicSystem.AbilityEffects
{
    public abstract class BaseEffect : ScriptableObject, iEffect {
        public TriggerTypes GetTrigger { get { return trigger; } }
        [SerializeField] TriggerTypes trigger;
        public Targets GetTarget { get { return target; } }
        [SerializeField] Targets target;
        public float Duration { get { return duration; } }
        [SerializeField] float duration = 3;
        public GameObject EffectVFX { get { return effectVFX; } }
        [SerializeField] GameObject effectVFX;
        public virtual void Activate(BaseCharacter baseCharacter, int amount =0 , int chance=100) { }

        public bool ActivateOnChance(int chance)
        {

            int rndNum = Random.Range(0, 100);
            return chance >= rndNum;

        }
    }
}
