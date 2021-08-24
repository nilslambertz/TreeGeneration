using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public static bool gamePaused;

    public GameObject pauseMenuOverlay;

    public GameObject buttonContainer;

    public GameObject presetContainer;

    public GameObject debugOverlay;

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (gamePaused) {
                resume();
            } else {
                pause();
            }
        }
    }

    public void resume() {
        pauseMenuOverlay.SetActive(false);
        buttonContainer.SetActive(true);
        presetContainer.SetActive(false);
        debugOverlay.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        gamePaused = false;
    }

    void pause() {
        debugOverlay.SetActive(false);
        buttonContainer.SetActive(true);
        presetContainer.SetActive(false);
        pauseMenuOverlay.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        gamePaused = true;
    }

    public void togglePresetMenu() {
        buttonContainer.SetActive(!buttonContainer.activeSelf);
        presetContainer.SetActive(!presetContainer.activeSelf);
    }

    public void quitGame() {
        Debug.Log("Quitting game.");
        Application.Quit();
    }
}
