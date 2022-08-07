using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillMagicSystem
{
    public interface iBase
    {
        string Name { get; }
        string Description { get; }
        public Level Level { get; }

        public void WriteToTextFile();
        public PassiveAbility AddPassiveAbility();
        public ActiveAbillity AddActiveAbility();


    }

    public enum Level{ Rookie, Intermed, Master, }

}
