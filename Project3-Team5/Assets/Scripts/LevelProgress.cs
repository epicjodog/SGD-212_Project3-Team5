using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgress : MonoBehaviour
{
    private int progInt = 0;
    private string time1 = "0";
    private string time2 = "0";
    private string time3 = "0";

    public Text time1Text;
    public Text time2Text;
    public Text time3Text;

    public Button level2;
    public Button level3;

    public static LevelProgress instance;

    public void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        if (PlayerPrefs.HasKey("progress"))
        {
            progInt = PlayerPrefs.GetInt("progress");
        }

        if (PlayerPrefs.HasKey("level1"))
        {
            time1 = PlayerPrefs.GetString("level1");
        }
        if (PlayerPrefs.HasKey("level2"))
        {
            time2 = PlayerPrefs.GetString("level2");
        }
        if (PlayerPrefs.HasKey("level3"))
        {
            time3 = PlayerPrefs.GetString("level3");
        }
        ShowTimes();
    }

    public void ShowTimes()
    {
        time1Text.text = "Time: " + time1;
        time2Text.text = "Time: " + time2;
        time3Text.text = "Time: " + time3;
        progInt = PlayerPrefs.GetInt("progress");

        print(progInt);
        switch (progInt)
        {
            case 0: level2.interactable = false;
                level3.interactable = false;
                break;
            case 1: level2.interactable = true;
                level3.interactable = false;
                break;
            case 2: level2.interactable = true;
                level3.interactable = true;
                break;
            default://in case the value doesn't work.
                level2.interactable = true;
                level3.interactable = true;
                break;
        }
    }
}
