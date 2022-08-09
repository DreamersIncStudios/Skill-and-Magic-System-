using DreamersIncStudios.MoonShot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.LevelManager
{
    public class LevelManager : MonoBehaviour, iLevel
    {
        public GameMaster GM => GameMaster.Instance;
        public float offset;
        public void Init()
        {
            #region Instantiate Character

           GameObject player = Instantiate( GM.PlayerOptions[GM.GetPlayerChoice]);
            player.transform.position = new Vector3(20, offset, 35);

            #endregion

        }

        // Start is called before the first frame update
        void Start()
        {
            Init();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
