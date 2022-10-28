using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilenceSound : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject speaker;
    public bool switchedOn;
    // Start is called before the first frame update
    void Start()
    {
        speaker = GameObject.FindWithTag("Floor");
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("discoBall2(Clone)")){ // turns light of if disco ball is activated
            speaker.GetComponent<AudioSource>().Pause();
        } else if (GameObject.Find("deathstarPrefab(Clone)")){ // turns light of if disco ball is activated
            speaker.GetComponent<AudioSource>().Pause();
        }
        else {
            speaker.GetComponent<AudioSource>().UnPause(); // light is turned on
        }
    }
}
