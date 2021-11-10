using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class CheckpointController : MonoBehaviour
{
    [SerializeField] GameObject[] checkpoints; //do this in order
    [SerializeField] int currentCheckpoint = 0;
    AudioManager audioMan;

    public GameObject winPanel;//Adds a spot for winPanel to be called for

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject checkpoint in checkpoints)
        {
            checkpoint.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0.25f);
        }
        checkpoints[currentCheckpoint].GetComponent<Renderer>().material.color = new Color(255, 255, 0);

        try { audioMan = GetComponent<AudioManager>(); }
        catch { print("Warning: Audio Manager Component not attached to player!"); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Checkpoint"))
        {
            if (other.gameObject == checkpoints[currentCheckpoint])
            {                
                currentCheckpoint++;
                if(currentCheckpoint < checkpoints.Length)
                {
                    checkpoints[currentCheckpoint].GetComponent<Renderer>().material.color = new Color(255, 255, 0);
                    //do what logic you need to set for the new checkpoint ring: checkpoints[currentCheckpoint]
                    print("Current checkpoint is " + currentCheckpoint);

                    //audioMan.Play("");
                }
                else
                {
                    print("Player has completed the level");
                    //audioMan.Play("");

                    winPanel.gameObject.SetActive(true);//Activates win panel when completing last checkpoint
                    Time.timeScale = 0f;// stops game
                    Cursor.lockState = CursorLockMode.None;//Cursor is allowed to move around freely and not stuck in place
                    Cursor.visible = true;//Cursor is seen
                }
                other.gameObject.SetActive(false);
            }           
        }
    }
}
