using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("Pause Menu")]
    public GameObject pauseMenu;

    public static bool isPaused;

    [Header("Main Menu")]
    [SerializeField] bool isMainMenu = false;
    //Level select
    [SerializeField] GameObject levelSelectPanel;
    [SerializeField] Button[] levelSelectButtons;
    //Help
    [SerializeField] GameObject helpPanel;
    [SerializeField] GameObject[] helpPanelPages;
    //Credits
    [SerializeField] GameObject creditsPanel;
    [SerializeField] GameObject[] creditsPanelPages;

    int helpPanelCurrentPage = 0;
    int creditsPanelCurrentPage = 0;

    public void loadLevel(string levelName) //migrate all the ui to this function
    {
        //later, add an if statement seeing if the level has been unlocked.
        SceneManager.LoadScene(levelName);
    }
    public void playGame(int SceneIndex)
    {
        SceneManager.LoadScene("Level 1");//Will switch between menu to level 1 
    }

    public void playLevel2(int SceneIndex)
    {
        SceneManager.LoadScene("Level 2");//Will switch between menu to level 1 
    }

    // Start is called before the first frame update
    void Start()
    {
        if(isMainMenu)
        {
            levelSelectPanel.SetActive(false);
            helpPanel.SetActive(false);
            creditsPanel.SetActive(false);
        }
        pauseMenu.SetActive(false);//When game starts pause menu will not start up as well
        //check to see if levels are unlocked. if not, disable specific buttons
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isMainMenu)//If Esc is press pause menu will pop up and pause game. frees up the cursor too.
        {
            if (isPaused)
            {
                ResumeGame();
                Cursor.lockState = CursorLockMode.Locked;//Cursor is stuck in place
                Cursor.visible = false;//Cursor is invisible 
            }
            else
            {
                PauseGame();
                Cursor.visible = true;//Cursor is seen
                Cursor.lockState = CursorLockMode.None;//Cursor can move around freely and not stuck in place
            }
        }
    }
    public void PauseGame()
    {
        pauseMenu.SetActive(true);//Turns on pause menu
        Time.timeScale = 0f;// stops game
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;//Cursor can move around freely and not stuck in place
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);//turns off pause menu
        Time.timeScale = 1f;//continues game
        isPaused = false;
    }

    void PauseManager()
    {
        if(!isPaused) //PauseGame()
        {
            pauseMenu.SetActive(true);//Turns on pause menu
            Time.timeScale = 0f;// stops game
            isPaused = true;
            Cursor.visible = true;//Cursor is seen
            Cursor.lockState = CursorLockMode.None;//Cursor can move around freely and not stuck in place
        }
        else //ResumeGame()
        {
            pauseMenu.SetActive(false);//turns off pause menu
            Time.timeScale = 1f;//continues game
            isPaused = false;
            Cursor.lockState = CursorLockMode.Locked;//Cursor is stuck in place
            Cursor.visible = false;//Cursor is invisible 
        }
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
    public void LevelSelect()
    {
        if (levelSelectPanel.activeSelf == true)
        {
            levelSelectPanel.SetActive(false);
        }
        else
        {
            levelSelectPanel.SetActive(true);
            helpPanel.SetActive(false);
            creditsPanel.SetActive(false);
        }
    }
    //Logic for main menu Help Buttons
    public void HelpButton()
    {
        if(helpPanel.activeSelf == true)
        {
            helpPanel.SetActive(false);
        }
        else
        {
            levelSelectPanel.SetActive(false);
            helpPanel.SetActive(true);
            creditsPanel.SetActive(false);
        }
    }
    public void HelpButtonTurnPage(bool isForward)
    {
        helpPanelPages[helpPanelCurrentPage].SetActive(false);
        if (isForward)
        {
            if(helpPanelCurrentPage < helpPanelPages.Length - 1)
            {
                helpPanelCurrentPage++;
            }          
        }
        else
        {
            if(helpPanelCurrentPage > 0)
            {
                helpPanelCurrentPage--;
            }           
        }
        helpPanelPages[helpPanelCurrentPage].SetActive(true);
    } 
    //Logic for main menu Credits Buttons
    public void CreditsButton()
    {
        if(creditsPanel.activeSelf == true)
        {
            creditsPanel.SetActive(false);
        }
        else
        {
            levelSelectPanel.SetActive(false);
            helpPanel.SetActive(false);
            creditsPanel.SetActive(true);
        }
    }
    public void CreditButtonTurnPage(bool isForward) //something that I thought of for the pause menu
    {
        creditsPanelPages[creditsPanelCurrentPage].SetActive(false);
        if (isForward)
        {
            if(creditsPanelCurrentPage < creditsPanelPages.Length - 1)
            {
                creditsPanelCurrentPage++;
            }           
        }
        else
        {
            if(creditsPanelCurrentPage > 0)
            {
                creditsPanelCurrentPage--;
            }
        }
        creditsPanelPages[creditsPanelCurrentPage].SetActive(true);
    }
}
