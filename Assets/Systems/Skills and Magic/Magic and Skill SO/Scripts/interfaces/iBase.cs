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




    }
    public interface extra{
        //public void WriteToTextFile();
        //public PassiveAbility AddPassiveAbility();
        //public ActiveAbillity AddActiveAbility();

    }


    public class BaseAbility : ScriptableObject,iBase {
    
         public string Name { get { return _name; } private set { _name = value; } }
        [SerializeField] string _name;
        public string Description { get { return description; } private set { description = value; } }
        [SerializeField] string description;
        public Level Level { get { return level; } private set { level = value; } }
        [SerializeField] Level level;
        public bool Magic;
    }


    public enum Level{ Rookie, Intermed, Master, }

}
