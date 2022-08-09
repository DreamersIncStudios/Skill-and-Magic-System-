using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using Core;
using System.ComponentModel;
using UnityEngine.Events;
using Stats;
using UnityEngine.SceneManagement;

namespace DreamersIncStudios.MoonShot
{
    public sealed class GameMaster : MonoBehaviour
    {
        public UnityEvent OnGameStart;
      

        public static GameMaster Instance;
        public GameStates State { get { return state; } set {
                if (value != state) {
                    state = value;
                    OnChangeGameState();
                }
            } }
        [SerializeField] GameStates state = GameStates.TitleScreen;
        public GameObject[] PlayerOptions;
        public GameObject Player;//{ get; private set; }
        public int GetPlayerChoice = new int();
        public bool SMTOverride;
        public CameraControls CamerasToControl;
        public int ActiveSaveNumber { get; set; }
    //    public InputSettings InputSettings = new InputSettings();
        public Quality Setting;

        Language GetLanguage;

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            }
            if (Instance != this)
            {
                Destroy(this.gameObject);
            }
            DontDestroyOnLoad(this.gameObject);
#if !UNITY_EDITOR
            Application.targetFrameRate = 360;
#endif
            //#if UNITY_STANDALONE_WIN

            //            InputSettings.TargetPlatform = PlatformOptions.PC;

            //#endif
            //#if UNITY_XBOXONE
            //        InputSettings.TargetPlatform = PlatformOptions.XBOX;
            //#endif
            //#if UNITY_PS4
            //       InputSettings.TargetPlatform = PlatformOptions.PC;
            //#endif

            //InputSettings.Controller = true;
            //InputSettings.SetUp();

        }

        public uint DayNumber  = 0;
        private void Start()
        {

        }
        public bool CreateMenuMain => State == GameStates.TitleScreen && Input.GetButtonUp("Submit");


        public void Update()
        {
          
        }

        public void SetupNewGame()
        {
            State = GameStates.Playing;

        }
        public void GetSaves() { }
        public void LoadSaves(int SaveNumber) { }

        public void SelectCharacter(int Choice) {
            GetPlayerChoice = Choice;
        }


        void OnEnable()
        {
            //if (PlayerPrefs.HasKey("SMTOverride"))
            //{
            //    SMTOverride = PlayerPrefs.GetInt("SMTOverride") == 0;
            //}
            if (PlayerPrefs.HasKey("Language"))
            {
                var json = PlayerPrefs.GetString("Language");
                GetLanguage = JsonUtility.FromJson<Language>(json);
            }
            new SMTOptions(SMTOverride);

        }
         
        void OnDisable()
        {

            PlayerPrefs.SetInt("SMTOverride", SMTOverride ? 0 : 1);

            var LangugaeSave = JsonUtility.ToJson(GetLanguage);
            PlayerPrefs.SetString("GetLanguage", LangugaeSave);
        }

        public void SetQualitySetting()
        {
            QualitySettings.SetQualityLevel(Setting.QualityLevel);
            QualitySettings.vSyncCount = Setting.VsyncCount;

        }
        void OnChangeGameState() {
            switch (State) {
                case GameStates.TitleScreen:
                case GameStates.WaitingToStartLevel:
                case GameStates.Paused:
                    SetCharactersInPlay(false);

                    break;

                case GameStates.Playing:
                    SetCharactersInPlay(true);
                break;
            }
        }
        void SetCharactersInPlay(bool inPlay) {
            BaseCharacter[] Get = GameObject.FindObjectsOfType<BaseCharacter>();
            for (int i = 0; i < Get.Length; i++)
            {
                Get[i].InPlay = inPlay;
            }
        }
    }
    public enum GameStates {
        TitleScreen,
        InMenu,
        Paused,
        Playing,
        InventoryMenu,
        Load,
        WaitingToStartLevel,
        Game_Over
    }

    public enum Language { English, Spanish }
    [System.Serializable]
    public struct CameraControls {
        public CinemachineVirtualCameraBase Main, Follow, Target;

    }
    [System.Serializable]
    public struct Quality
    {
        public int QualityLevel;
        public int VsyncCount;
        public Language _language;

    }
    
    
   

}
