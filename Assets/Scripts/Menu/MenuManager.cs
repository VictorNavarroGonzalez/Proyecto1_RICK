using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject newGameMenu;
    public GameObject rickText;

    void Start() {
        // To start without the menu.
        Resume();
    }

    void Update() {

        #region BUTTON BEHAVIOURS
        if (InputManager.ButtonStart) {

            InputManager.ButtonStart = false;

            if (mainMenu.activeSelf) Resume(); 
            else Pause();

        }

        // Resume the game if ButtonB is pressed.
        if (InputManager.ButtonB) {

            InputManager.ButtonB = false;
            if (mainMenu.activeSelf) Resume();

        }

        // To prevent changing character  and jumping from the menu.
        if (InputManager.ButtonY) InputManager.ButtonY = false;
        if (InputManager.ButtonA) InputManager.ButtonA = false;
        #endregion

        // Change background colors according to RICK character.
        if (PlayerState.Character == PlayerState.MyCharacter.CIRCLE) {
            mainMenu.GetComponent<Image>().color = new Color32(255, 255, 255, 130);
            newGameMenu.GetComponent<Image>().color = new Color32(255, 255, 255, 130);
        }
        else if (PlayerState.Character == PlayerState.MyCharacter.SQUARE) {
            mainMenu.GetComponent<Image>().color = new Color32(0, 0, 0, 130);
            newGameMenu.GetComponent<Image>().color = new Color32(0, 0, 0, 130);
        }
        
    }

    // All of these functions are self-explanatory.

    public void Resume() {
        mainMenu.SetActive(false);
        rickText.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Pause() {
        mainMenu.SetActive(true);
        rickText.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Exit() {
        Application.Quit();
    }

    public void LoadConfirmation() {
        newGameMenu.SetActive(true);
        newGameMenu.GetComponent<ButtonSelector>().SelectButton();
        mainMenu.SetActive(false);
    }

    public void LoadMainMenu() {
        mainMenu.SetActive(true);
        mainMenu.GetComponent<ButtonSelector>().SelectButton();
        newGameMenu.SetActive(false);
    }
    
}
