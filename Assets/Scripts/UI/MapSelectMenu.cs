using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelectMenu : MonoBehaviour
{
    public MainMenu mainMenu;
    public GameObject objectiveMessage;
    private Text objectiveMessageText;
    
    public ToggleSpriteSwap cloudMapToggle;
    public ToggleSpriteSwap planetMapToggle;

    
    
    //question mark makes var nullable
    private bool? difficulty;
    
    public bool? Difficulty => difficulty;

    public Toggle easyToggle;
    public Toggle hardToggle;

    private void Awake()
    {
        difficulty = false;
        objectiveMessageText = objectiveMessage.GetComponent<Text>();
    }


    private void Start()
    {
        cloudMapToggle.onValueChanged += ChangeToUpInTheClouds;
        planetMapToggle.onValueChanged += ChangeToDistantPlanet;
    }

    private void ChangeToUpInTheClouds(bool isOn)
    {
        bool isPlanetToggleOn = planetMapToggle.IsOn;

        if (!isOn && !isPlanetToggleOn)
        {
            mainMenu.SetSceneToLoad(null);
            return;
        }
        
        if (isOn && isPlanetToggleOn)
            planetMapToggle.IsOn = false;
        
        mainMenu.SetSceneToLoad("UpInTheClouds");
    }

    private void ChangeToDistantPlanet(bool isOn)
    {
        bool isCloudToggleOn = cloudMapToggle.IsOn;

        if (!isOn && !isCloudToggleOn)
        {
            mainMenu.SetSceneToLoad(null);
            return;
        }
        
        if (isOn && isCloudToggleOn)
            cloudMapToggle.IsOn = false;
        
        mainMenu.SetSceneToLoad("DistantPlanet");
    }
    
    public void HardToggleChange(bool isOn)
    {
        bool isEasyToggleOn = easyToggle.isOn;
        
        switch (isOn)
        {
            case false when isEasyToggleOn:
                return;
            //toggle was turned off and other toggle is also off.
            case false:
                objectiveMessageText.text = "PLEASE SELECT A DIFFICULTY";
                difficulty = null;
                return;
        }

        objectiveMessageText.text = "OBJECTIVE: KILL 20 ENEMIES";
        difficulty = true;
            
        if (isEasyToggleOn)
        {
            easyToggle.isOn = false;

        }
    }
    
    public void EasyToggleChange(bool isOn)
    {
        bool isHardToggleOn = hardToggle.isOn;
        
        switch (isOn)
        {
            case false when isHardToggleOn:
                return;
            case false:
                objectiveMessageText.text = "PLEASE SELECT A DIFFICULTY";
                difficulty = null;
                return;
        }
        
        objectiveMessageText.text = "OBJECTIVE: KILL 10 ENEMIES";
        difficulty = false;
        

        if (isHardToggleOn)
        {
            hardToggle.isOn = false;
        }
    }

    public void CloseMenu()
    {
        mainMenu.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
