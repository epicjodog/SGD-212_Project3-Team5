using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject pauseMenu;

    public static bool isPaused;
    public void playGame(int SceneIndex)
    {
        SceneManager.LoadScene("Level 1");//Will switch between menu to level 1 
    }
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);//When game starts pause menu will not start up as well
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//If Esc is press pause menu will pop up and pause game. frees up the cursor too.
        {
            if (isPaused)
            {
                ResumeGame();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                PauseGame();
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
    public void PauseGame()
    {
        pauseMenu.SetActive(true);//Turns on pause menu
        Time.timeScale = 0f;// stops game
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);//turns off pause menu
        Time.timeScale = 1f;//continues game
        isPaused = false;
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
