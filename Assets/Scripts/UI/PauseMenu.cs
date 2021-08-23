using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public static bool gamePaused;

    public GameObject pauseMenuOverlay;

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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        gamePaused = false;
    }

    void pause() {
        pauseMenuOverlay.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        gamePaused = true;
    }

    public void quitGame() {
        Debug.Log("Quitting game.");
        Application.Quit();
    }
}
