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
    AudioManager audioMan; //for the main menu

    public void loadLevel(string levelName) //migrate all the ui to this function
    {        
        //later, add an if statement seeing if the level has been unlocked.
        StartCoroutine(LevelTransition(levelName));
        audioMan.Play("Select");
    }

    private IEnumerator LevelTransition(string levelName)
    {
        float offset = 1 - AudioListener.volume;
        for (float vol = AudioListener.volume; vol >= 0; vol -= 0.1f) //lowers the volume for transition
        {
            AudioListener.volume = vol;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(offset); //always has a second to transition

        SceneManager.LoadScene(levelName);
    }

    public void playGame(int SceneIndex)
    {
        SceneManager.LoadScene("Level 1");//Will switch between menu to level 1 
        print("Warning: will be depricated, use loadLevel instead!");
    }

    public void playLevel2(int SceneIndex)
    {
        SceneManager.LoadScene("Level 2");//Will switch between level 1 to level 2 
        print("Warning: will be depricated, use loadLevel instead!");
    }
    public void playLevel3(int SceneIndex)
    {
        SceneManager.LoadScene("Level 3");//Will switch between level 2 to level 3 
        print("Warning: will be depricated, use loadLevel instead!");
    }

    // Start is called before the first frame update
    void Start()
    {
        //maybe make the playerprefs here? player progress and volume
        AudioListener.volume = 1; //this should be changed when we implement a volume slider
        if(isMainMenu)
        {
            if (PlayerPrefs.HasKey("Progress") == false)
            {
                PlayerPrefs.SetInt("Progress", 0);
            }
            levelSelectPanel.SetActive(false);
            helpPanel.SetActive(false);
            creditsPanel.SetActive(false);
            audioMan = GetComponent<AudioManager>();
            audioMan.Play("Music");
            //locking levels, uncomment when ready
            /*levelSelectButtons[1].interactable = false;
            levelSelectButtons[2].interactable = false;
            if (PlayerPrefs.HasKey("Progress"))
            {
                if (PlayerPrefs.GetInt("Progress") > 1)
                {
                    levelSelectButtons[1].interactable = true;
                }
                if (PlayerPrefs.GetInt("Progress") > 2)
                {
                    levelSelectButtons[2].interactable = true;
                }
            }*/
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
            audioMan.Play("Back");
            levelSelectPanel.SetActive(false);
        }
        else
        {
            audioMan.Play("Select");
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
            audioMan.Play("Back");
            helpPanel.SetActive(false);
        }
        else
        {
            audioMan.Play("Select");
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
                audioMan.Play("MiniButton");
            }          
        }
        else
        {
            if(helpPanelCurrentPage > 0)
            {
                helpPanelCurrentPage--;
                audioMan.Play("MiniButton");
            }           
        }
        helpPanelPages[helpPanelCurrentPage].SetActive(true);
    } 
    //Logic for main menu Credits Buttons
    public void CreditsButton()
    {
        if(creditsPanel.activeSelf == true)
        {
            audioMan.Play("Back");
            creditsPanel.SetActive(false);
        }
        else
        {
            audioMan.Play("Select");
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
                audioMan.Play("MiniButton");
            }           
        }
        else
        {
            if(creditsPanelCurrentPage > 0)
            {
                creditsPanelCurrentPage--;
                audioMan.Play("MiniButton");
            }
        }
        creditsPanelPages[creditsPanelCurrentPage].SetActive(true);
    }
    public void OnResetButtonClick()
    {
        //resets progress
    }
    public void OnMuteSFXButtonClick()
    {
        //sets sfx volume to 0
    }
    public void OnMuteMusicButtonClick()
    {
        //sets music volume to 0
    }
}
