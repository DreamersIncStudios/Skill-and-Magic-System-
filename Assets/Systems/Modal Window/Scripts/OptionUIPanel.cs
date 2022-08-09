using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using System;
using DreamersIncStudios.MoonShot;
using Core;
    namespace Dreamers.ModalWindows
{
    public class OptionUIPanel : MonoBehaviour
    {
        [Header("Header")]
        [SerializeField] Transform header;

        [Header("Content")]
        [SerializeField] Transform Content;
        [SerializeField] GameObject dropDownMenu;
        [SerializeField] GameObject smtCheckBox;
        [Header("Footer")]
        [SerializeField] Transform Footer;

        // Start is called before the first frame update
        void Start()
        {
            ResetGraphic();

            ShowGraphicsOptions();
        }

        public void ShowGraphicsOptions()
        {

            Resolution[] resolutions = Screen.resolutions;

            GameObject resolutionOption = Instantiate(dropDownMenu, dropDownMenu.transform.parent);
            TMP_Dropdown screenMenu = dropDownMenu.GetComponentInChildren<TMP_Dropdown>();
            dropDownMenu.GetComponentInChildren<TextMeshProUGUI>().text = "Screen Mode: ";
            screenMenu.ClearOptions();
            List<string> screenOptions = new List<string>();
            for (int i = 0; i < Enum.GetValues(typeof(FullScreenMode)).Length; i++)
            {
                screenOptions.Add(((FullScreenMode)i).ToString());
            }
            screenMenu.AddOptions(screenOptions);
            screenMenu.value = screenModeIndex;
            TMP_Dropdown menu = resolutionOption.GetComponentInChildren<TMP_Dropdown>();
            resolutionOption.GetComponentInChildren<TextMeshProUGUI>().text = "Screen Resolution: ";

            menu.ClearOptions();
            List<string> options = new List<string>();
            // Print the resolutions
            foreach (var res in resolutions)
            {
                options.Add(res.width + "x" + res.height);
            }
            menu.AddOptions(options);
            menu.value = screenResolutionIndex;
            menu.onValueChanged.AddListener(delegate { SetScreenResolution(menu.value); });
            screenMenu.onValueChanged.AddListener(delegate { SetScreenMode(screenMenu.value); });
            SetupSMT();

        }

        int screenModeIndex, screenResolutionIndex;
        bool vsyncOn = true;
        uint targetFPS;
        public void SetScreenMode(int index)
        {
            screenModeIndex = index;
        }
        public void SetScreenResolution(int index)
        {
            screenResolutionIndex = index;
        }

        public void SetGraphicsSettings()
        {
            Resolution[] resolutions = Screen.resolutions;
            Screen.SetResolution(resolutions[screenResolutionIndex].width, resolutions[screenResolutionIndex].height, (FullScreenMode)screenModeIndex, (int)targetFPS);
            QualitySettings.vSyncCount = vsyncOn ? 1 : 0;
            //Todo write to PlayerPrefs
        }
        public void ResetGraphic()
        {
            //Todo Load from PlayerPrefs if there is a value;
            Resolution[] resolutions = Screen.resolutions;
            screenResolutionIndex = Array.IndexOf(resolutions, Screen.currentResolution);
            screenModeIndex = (int)Screen.fullScreenMode;
            vsyncOn = QualitySettings.vSyncCount > 0;
        }

        public void Close()
        {
            Destroy(this.gameObject, 2.5f);
        }

        public void SetupSMT() {
            Toggle toggle = smtCheckBox.GetComponentInChildren<Toggle>();
            toggle.isOn = GameMaster.Instance.SMTOverride;
            toggle.onValueChanged.AddListener(delegate{
                GameMaster.Instance.SMTOverride = toggle.isOn;
                new SMTOptions(toggle.isOn);
            });
        }
    }
}