using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DefaultNamespace;

public class PauseMenu : MonoBehaviour
{

    public static bool gamePaused;

    public GameObject pauseMenuOverlay;

    public GameObject buttonContainer;

    public GameObject presetContainer;

    public GameObject optionsContainer;

    public GameObject debugOverlay;

    private List<TreePreset> presetList;

    void Start()
    {
        PresetParameters.initialisePresets();
        updatePresetList();

        pauseMenuOverlay.SetActive(false);
        buttonContainer.SetActive(true);
        presetContainer.SetActive(false);
        optionsContainer.SetActive(false);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                resume();
            }
            else
            {
                pause();
            }
        }
    }

    private void updatePresetList()
    {
        presetList = PresetParameters.getPresetList();
    }

    public void resume()
    {
        pauseMenuOverlay.SetActive(false);
        optionsContainer.SetActive(false);
        presetContainer.SetActive(false);
        buttonContainer.SetActive(true);
        debugOverlay.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        gamePaused = false;
    }

    void pause()
    {
        debugOverlay.SetActive(false);
        presetContainer.SetActive(false);
        optionsContainer.SetActive(false);
        pauseMenuOverlay.SetActive(true);
        buttonContainer.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        gamePaused = true;
    }

    public void openPresetMenu()
    {
        buttonContainer.SetActive(false);
        optionsContainer.SetActive(false);
        presetContainer.SetActive(true);

    }

    public void openOptionsMenu()
    {
        buttonContainer.SetActive(false);
        presetContainer.SetActive(false);
        optionsContainer.SetActive(true);

    }

    public void backToMenu()
    {
        presetContainer.SetActive(false);
        optionsContainer.SetActive(false);
        buttonContainer.SetActive(true);

    }
    public void quitGame()
    {
        Debug.Log("Quitting game.");
        Application.Quit();
    }
}
