using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //make sense of all of this
    private string sceneToLoad;
    public GameObject optionsMenu;
    public GameObject mapMenu;
    public GameObject loadingScreen;
    
    public GameObject startButton;
    public GameObject quitButton;
    public GameObject mapMenuButton;
    public GameObject optionsMenuButton;

    private Button[] buttons;
    public Button[] Buttons => buttons;
    
    public GameObject difficultyWarning;
    public GameObject mapWarning;
    private bool canRun;

    private MapSelectMenu mapSelectMenu;
    
    private void Awake()
    {
        sceneToLoad = null;
        canRun = true;
        mapSelectMenu = mapMenu.GetComponent<MapSelectMenu>();
        buttons = new[]
        {
            optionsMenuButton.GetComponent<Button>(),
            mapMenuButton.GetComponent<Button>(),
            startButton.GetComponent<Button>(),
            quitButton.GetComponent<Button>()
        };
    }

    private void OnEnable()
    {
        if (mapWarning.activeSelf) mapWarning.SetActive(false);
        if(difficultyWarning.activeSelf) difficultyWarning.SetActive(false);
        canRun = true;
    }

    public void SetSceneToLoad(string scene)
    {
        sceneToLoad = scene;
    }

    public void StartGame()
    {
        if (sceneToLoad != null && mapSelectMenu.Difficulty != null)
        {
            loadingScreen.SetActive(true);
            StartCoroutine(WaitForLoad());
            SceneManager.LoadScene(sceneToLoad);
            //pop up loading screen and load scene according to difficulty 
        }
        else if (sceneToLoad == null)
        {
            if(canRun) StartCoroutine(Fade(mapWarning));
        }
        else
        {
            if(canRun) StartCoroutine(Fade(difficultyWarning));
        }

    }
    
    IEnumerator WaitForLoad()
    {
        yield return new WaitForSeconds(2f);
        if(loadingScreen.activeSelf) loadingScreen.SetActive(false);
    }

    private IEnumerator Fade(GameObject obj)
    {
        canRun = false;
        obj.SetActive(true);
        yield return new WaitForSeconds(2f);
        obj.SetActive(false);
        canRun = true;
    }

    public void OpenMapMenu()
    {
        gameObject.SetActive(false);
        mapMenu.SetActive(true);
    }
    
    public void OpenOptions()
    {
        optionsMenu.SetActive(true);
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
