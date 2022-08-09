using DreamersIncStudios.MoonShot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.LevelManager
{
    public interface iLevel
    {
        public GameMaster GM { get; }
      
        public void Init();

    }
}