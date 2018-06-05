using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DefaultValues;


public class MenuManager : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject newGameMenu;
    public GameObject rickText;

    void Start() {
        // To start without the menu.
        Resume();

        GameObject player = GameObject.Find("Player");
        player.transform.position = Levels.Current;
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
            if (newGameMenu.activeSelf) LoadMainMenu();

        }

        // To prevent changing character  and jumping from the menu.
        if (mainMenu.activeSelf || newGameMenu.activeSelf) {
            if (InputManager.ButtonY) InputManager.ButtonY = false;
            if (InputManager.ButtonA) InputManager.ButtonA = false;
        }
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
        newGameMenu.SetActive(false);
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

    public void LoadLevelSelector() {
        newGameMenu.SetActive(true);
        newGameMenu.GetComponent<ButtonSelector>().SelectButton();
        mainMenu.SetActive(false);
    }

    public void LoadMainMenu() {
        mainMenu.SetActive(true);
        newGameMenu.SetActive(false);
        mainMenu.GetComponent<ButtonSelector>().SelectButton();
        newGameMenu.SetActive(false);
    }

    public void LoadLevel(int level) {

        switch (level) {
            case 1:
                SceneManager.LoadScene("CIRCLE", LoadSceneMode.Single);
                Levels.Current = Levels.C01;
                break;

            case 2:
                SceneManager.LoadScene("CIRCLE", LoadSceneMode.Single);
                Levels.Current = Levels.C02;
                break;

            case 3:
                SceneManager.LoadScene("CIRCLE", LoadSceneMode.Single);
                Levels.Current = Levels.C03;
                break;

            case 4:
                SceneManager.LoadScene("SQUARE", LoadSceneMode.Single);
                Levels.Current = Levels.S01;
                break;

            case 5:
                SceneManager.LoadScene("SQUARE", LoadSceneMode.Single);
                Levels.Current = Levels.S02;
                break;

            case 6:
                SceneManager.LoadScene("SQUARE", LoadSceneMode.Single);
                Levels.Current = Levels.S03;
                break;

            case 7:
                SceneManager.LoadScene("BOTH", LoadSceneMode.Single);
                Levels.Current = Levels.B01;
                break;

            case 8:
                SceneManager.LoadScene("BOTH", LoadSceneMode.Single);
                Levels.Current = Levels.B02;
                break;

            case 9:
                SceneManager.LoadScene("BOTH", LoadSceneMode.Single);
                Levels.Current = Levels.B03;
                break;
        }

    }

    
    
}
