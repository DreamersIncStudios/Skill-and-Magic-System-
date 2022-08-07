using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
namespace SkillMagicSystem.AbilityEffects
{

    [CreateAssetMenu(fileName = "Effect", menuName = "Magic and Skills/Effect")]

    public class EffectsSO: ScriptableObject
    {
        public Trigger trigger;
        public Target target;
        public Effects ActionEffect;
        public GameObject effectVFX;
        public int Amount;
        public float duration = 3;
        public void Heal() {
            switch (target) 
            {
                case Target.Self:
            if (effectVFX != null)
            {
                var spawned = Instantiate(effectVFX).GetComponent<VisualEffect>(); // figure out how to postion 
                spawned.Play();
                Destroy(spawned.gameObject, duration);
            }
                    break;
        }
            Debug.Log($"Heal {target.ToString()} for {Amount}");
        }
    }

    public enum Effects { Heal, Drain, Damage, Modify, Steal,  }
    public enum Target { Self, TeamMember, Target, AOE, Direction}
}
