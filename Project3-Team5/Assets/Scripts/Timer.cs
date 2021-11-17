using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;//Will link player to timer text
    private float startTime;
    [SerializeField] private int levelNum;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time - startTime;

        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");

        timerText.text = minutes + ":" + seconds;
    }

    public void SaveTime()
    {
        PlayerPrefs.SetString("level" + levelNum, timerText.text);
        print(PlayerPrefs.GetString("level" + levelNum));
        PlayerPrefs.SetInt("progress", levelNum);
        print(PlayerPrefs.GetInt("progress"));
        PlayerPrefs.Save();
    }
}
