using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;//Will link player to timer text
    [SerializeField] private int startTime = 120;
    [SerializeField] private int levelNum;
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
            yield return new WaitForSeconds(1);
        }
    }

    public void SaveTime()
    {
        PlayerPrefs.SetString("level" + levelNum, timerText.text);
        PlayerPrefs.Save();

        PlayerPrefs.SetInt("progress", levelNum);
        print(PlayerPrefs.GetInt("progress"));
        PlayerPrefs.Save();
    }
}
