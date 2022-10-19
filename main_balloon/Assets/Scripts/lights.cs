using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lights : MonoBehaviour
{
    private GameObject roomLight;

    // Start is called before the first frame update
    void Start()
    {
        roomLight = GameObject.FindWithTag("Light");
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("discoBall2(Clone)")){ // turns light of if disco ball is activated
            roomLight.SetActive(false);
        }

        else{
            roomLight.SetActive(true); // light is turned on
        }
    }
}
