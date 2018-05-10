using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject newGameMenu;

    void Start() {
        //mainMenu.SetActive(false);
    }

    void Update() {
        if (InputManager.ButtonStart) {

            InputManager.ButtonStart = false;

            if (mainMenu.activeSelf) Resume(); 
            else Pause();

        }
    }

    public void Resume() {
        mainMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Pause() {
        mainMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Exit() {
        Application.Quit();
    }

    public void LoadConfirmation() {
        mainMenu.SetActive(false);
        newGameMenu.SetActive(true);
    }

    public void LoadMainMenu() {
        mainMenu.SetActive(true);
        newGameMenu.SetActive(false);
    }
    
}
