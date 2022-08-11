using Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillMagicSystem.AbilityEffects
{
    public class StatusChangeEffect : BaseEffect, IOnCommandTimer, IOnHitEffect, IOnTimeEffect, IOnCommandHit
    {

        public float Delay { get { return delay; } }
        [SerializeField] float delay;


        public void CancelTimer()
        {
            throw new System.NotImplementedException();
        }
        public void CancelTimerCommand()
        {
            throw new System.NotImplementedException();
        }
        public void OnCommandAddTimer(BaseCharacter baseCharacter, int Amount)
        {
            throw new System.NotImplementedException();
        }

        public void OnCommandHit(BaseCharacter baseCharacter, int Amount)
        {
            throw new System.NotImplementedException();
        }

        public void OnHit(BaseCharacter baseCharacter, int amount, int chance)
        {
            throw new System.NotImplementedException();
        }

        public void OnTimer(BaseCharacter baseCharacter, int amount, float interval)
        {
            throw new System.NotImplementedException();
        }
    }
}
