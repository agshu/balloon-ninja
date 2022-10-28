using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lights : MonoBehaviour
{
    private GameObject roomLight1;
    private GameObject roomLight2;

    // Start is called before the first frame update
    void Start()
    {
        roomLight1 = GameObject.FindWithTag("Light1");
        roomLight2 = GameObject.FindWithTag("Light2");
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("discoBall2(Clone)")){ // turns light of if disco ball is activated
            roomLight1.SetActive(false);
            roomLight2.SetActive(false);
        } else if (GameObject.Find("deathstarPrefab(Clone)")){ // turns light of if disco ball is activated
            roomLight1.SetActive(false);
            roomLight2.SetActive(false);
        } else if (GameObject.Find("DeathStarExplosion(Clone)")){ // turns light of if disco ball is activated
            roomLight1.SetActive(false);
            roomLight2.SetActive(false);
        }

        else {
            roomLight1.SetActive(true);
            roomLight2.SetActive(true); // light is turned on
        }
    }
}
