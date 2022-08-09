using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Dreamers.Global;
using DG.Tweening;
using DreamersIncStudios.MoonShot;
using Dreamers.ModalWindows;

public class MainMenuManager : MonoBehaviour
{
    public GameMaster GM { get; set; }
    public bool CreateMenuMain => GM.State == GameStates.TitleScreen && Input.GetButtonUp("Submit");
    public MainMenu mainMenu;
    public GameObject StartIcon;
    public GameObject UICanvas;
    public UIManager UIM;
    GameObject main;
    CanvasGroup menuGroup;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameMaster.Instance;
        GM.State = GameStates.TitleScreen;
        UIM = UIManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (CreateMenuMain)
        {
            //mainMenu.GetMainMenu(StartIcon.transform.parent.gameObject);
            main = CreateMainMenu();
            GM.State = GameStates.InMenu;
            Destroy(StartIcon);
        }
    }
    [SerializeField] List<SelectionIcons> gameModes;
    [SerializeField] List<CharacterSelect> characterSelection;
    GameObject CreateMainMenu()
    {
        GameObject menuPanel = Instantiate(UIM.ModalMenuPrefab, UICanvas.transform);
        menuGroup = menuPanel.GetComponent<CanvasGroup>();
        RectTransform rect = menuPanel.GetComponent<RectTransform>();
        //Dotween
        rect.anchoredPosition = new Vector2(-1350, 0);
        rect.DOAnchorPos(new Vector2(584, 0), 3);

        ModalMenu menu = menuPanel.GetComponent<ModalMenu>();
        List<MenuButtons> buttons = new List<MenuButtons>();
        UnityEvent newGame = new UnityEvent();
        newGame.AddListener(() => {
            ModalWindow window = Instantiate(UIM.ModalWindowPrefab, UICanvas.transform).GetComponent<ModalWindow>();
            RectTransform windowRect = window.GetComponent<RectTransform>();
            windowRect.anchoredPosition = new Vector2(1200, 0);
            windowRect.DOAnchorPos(new Vector2(0, 0), 3);
            menuGroup.DOFade(0, 2.5f);
            UnityEvent cancel = new UnityEvent();
            cancel.AddListener(() => {
                menuGroup.DOFade(1, 2.5f);
            });
            window.ShowAsSelection("Select Game Mode", gameModes, cancel);
        });
        buttons.Add(new MenuButtons()
        {
            text = "New Game",
            actionToTake = newGame
        });
        UnityEvent cont = new UnityEvent();
        cont.AddListener(() => { LoadMostRecentSave(); });

        buttons.Add(new MenuButtons()
        {
            text = "Continue",
            actionToTake = cont
        });

        buttons.Add(new MenuButtons()
        {
            text = "Load Saved Game"
        });
        UnityEvent openOptions = new UnityEvent();
        openOptions.AddListener(() => {
            OptionUIPanel optionUI = Instantiate(UIM.optionsWindow, UICanvas.transform).GetComponent<OptionUIPanel>();

        });
        buttons.Add(new MenuButtons()
        {
            text = "Options",
            actionToTake = openOptions
        });

        buttons.Add(new MenuButtons()
        {
            text = "Credits"
        });

        UnityEvent Exit = new UnityEvent();
        Exit.AddListener(() => {
            menuGroup.DOFade(0.0f, 2.5f);

            UnityEvent exit = new UnityEvent();
            exit.AddListener(() => { Application.Quit(); });

            ModalWindow exitPrompt = Instantiate(UIM.ModalWindowPrefab, UICanvas.transform).GetComponent<ModalWindow>();
            UnityEvent cancel = new UnityEvent();
            cancel.AddListener(() => {
                Debug.Log("IDK maybe at something");
                menuGroup.DOFade(1.0f, 2.5f);

            });
            exitPrompt.ShowAsPrompt("Are you sure you wish to Exit?", null, "Do you wish to exist to the Desktop? Any unsaved changes and progress will be lost.", exit, cancel);

        });
        menu.DisplayMenu("Main Menu", buttons, "Exit To Desktop", Exit);
        return menuPanel;
    }

    public void StartNewStory()
    {
        Debug.Log("Story Mode Not Available yet");
        //Instance modal Window Go Back 
    }

    public void LaunchTowerDestory()
    {

        ModalSelectionWindow selectionWindow = Instantiate(UIM.ModalSelecetionPrefab, UICanvas.transform).GetComponent<ModalSelectionWindow>();
        UnityEvent back = new UnityEvent();
        back.AddListener(() => {
            menuGroup.DOFade(1, 2.5f);

        });
        selectionWindow.ShowAsCharacterSelection(characterSelection, back);
    }

    void LoadMostRecentSave()
    {
    }
}
