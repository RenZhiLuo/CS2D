using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class GameSettingPanel : MonoBehaviour
{
    public static GameSettingPanel instance;
    

    [SerializeField] private GameObject settingPanel;

    [SerializeField] private Button openBtn;
    [SerializeField] private Button closeBtn;


    #region Panel 1
    //view switch
    public Toggle[] viewToggle;
    //mouse flip
    public Toggle horizontalFlipToggle;
    public Toggle verticalFlipToggle;

    public Slider horizontalSensitivity;
    public Slider verticalSensitivity;
    #endregion

    #region Panel 2
    public Slider volumeSlide;
    #endregion

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        settingPanel.SetActive(false);

        openBtn.onClick.AddListener(() => { OpenSettingPanel(true); });
        closeBtn.onClick.AddListener(() => { OpenSettingPanel(false); });
    }


    private void OpenSettingPanel(bool value)
    {
        settingPanel.SetActive(value);
    }

}
