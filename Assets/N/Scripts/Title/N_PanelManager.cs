using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class N_PanelManager : MonoBehaviour
{
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject settingMenu;
    [SerializeField] GameObject devicesMenu;
    [SerializeField] GameObject backToStartButton;
    [SerializeField] GameObject[] devices_toggles;
    // 0, 1, 2, =>  keyboard, procon, handle
    public static int N_selectedDevice = 0;
    //めんどくさいのでクソコード
    //もっとパネル増えたらちゃんと書きます
    void Start()
    {
        BackToStartMenu();
    }
    public void Settings_OnClick()
    {
        startMenu.SetActive(false);
        settingMenu.SetActive(true);
        devicesMenu.SetActive(false);
        backToStartButton.SetActive(true);
    }
    public void Devices_Onclick()
    {
        startMenu.SetActive(false);
        settingMenu.SetActive(false);
        devicesMenu.SetActive(true);
        backToStartButton.SetActive(true);
    }
    public void Devices_decide_OnClick()
    {
        for (int i = 0; i < devices_toggles.Length; i++)
        {
            if (devices_toggles[i].GetComponent<UnityEngine.UI.Toggle>().isOn)
            {
                N_selectedDevice = i;
                break;
            }
        }
        BackToStartMenu();
    }
    public void BackToStartMenu()
    {
        startMenu.SetActive(true);
        settingMenu.SetActive(false);
        devicesMenu.SetActive(false);
        backToStartButton.SetActive(false);
    }

}

//こんにちは