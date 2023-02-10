using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class OptionsMenu : MonoBehaviour
{
    private MainMenu mainMenu;
    private Button[] buttons;
    private bool activate;

    private Scene currentScene;
    private void Awake()
    {
        
        currentScene  = SceneManager.GetActiveScene();
        
        if (currentScene.name.Equals("TitleScreen"))
        {
            mainMenu = GameObject.Find("Home01_V1Blue").GetComponent<MainMenu>();
            buttons = mainMenu.Buttons;
            activate = true;
        }
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
        if(activate)
        {
            foreach (var button in buttons)
            {
                button.interactable = true;
            }
        }
    }
    
}
