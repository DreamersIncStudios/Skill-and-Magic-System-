using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DreamersInc.Utils;

namespace SkillMagicSystem
{
    public class AbilityManager : MonoBehaviour
    {
        public List<BaseAbility> AddedAbilities;

        public List<PassiveAbility> passiveAbilities;
        public List<ActiveAbillity> activeAbillities;

        public Action OnHit;
        public Action OnGotHit;
        public Action OnKillEnemy;
        public Action OnPlayerTeamDeath;
        public GridGeneric<MagicGridObject> grid;
        private int width, height;
        private float cellsize;
        // Start is called before the first frame update
        void Start()
        {
            AddedAbilities = new List<BaseAbility>();
        }

        public void SetupGrid(int width = 15, int height = 10, float cellsize = 5f)
        {
            grid = new GridGeneric<MagicGridObject>(width, height, cellsize, (GridGeneric<MagicGridObject> g, int x, int y) => new MagicGridObject(g, x, y)
            );
            this.width = width;
            this.height = height;
            this.cellsize = cellsize;

        }


        public void OnEnemyNPCDeath() { 
            OnKillEnemy.Invoke();
        }
        public void OnPlayerDeath() {
            OnPlayerTeamDeath.Invoke();
        }
        public void OnHitEnemy() {
            OnHit.Invoke();
        }
        public void OnGotHitPlayer() {
            OnGotHit.Invoke();
        }
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.B)) {
                activeAbillities[0].OnAbilityTrigger.Invoke();
            }
        }

    }

 
}
