using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;//Will link player to timer text
    [SerializeField] private int startTime = 120;
    [SerializeField] private int levelNum;
    [SerializeField] private GameObject losePanel;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("timer");
    }

    private IEnumerator timer()
    {
        while (true)
        {
            timerText.text = "time: " + startTime;
            startTime--;

            if (startTime < 0)
            {
                losePanel.gameObject.SetActive(true);//Activates win panel when completing last checkpoint
                Time.timeScale = 0f;// stops game
                Cursor.lockState = CursorLockMode.None;//Cursor is allowed to move around freely and not stuck in place
                Cursor.visible = true;//Cursor is seen        
            }
            yield return new WaitForSeconds(1);
        }
    }

    public void SaveTime()
    {
        int value = PlayerPrefs.GetInt("level" + levelNum);

        if (value > startTime || value == 0)
        {
            PlayerPrefs.SetInt("level" + levelNum, startTime);
            PlayerPrefs.Save();
        }

        PlayerPrefs.SetInt("progress", levelNum);
        print(PlayerPrefs.GetInt("progress"));
        PlayerPrefs.Save();
    }
}
