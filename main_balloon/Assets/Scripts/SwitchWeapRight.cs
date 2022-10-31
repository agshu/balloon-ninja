using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SwitchWeapRight : MonoBehaviour
{
    public InputActionReference toggleReference = null;

    public GameObject glove;
    public GameObject cactus;
    public GameObject sword;
    public GameObject magnet;
    private void Awake() 
    {
        toggleReference.action.started += Toggle;
    }

    private void Start() 
    {
        cactus.SetActive(false);
        glove.SetActive(false);
        magnet.SetActive(false);
    }

    private void OnDestroy() 
    {
        toggleReference.action.started -= Toggle;
    }    


    private void Toggle(InputAction.CallbackContext context) 
    {   
        if(glove.activeSelf == true) {
            glove.SetActive(false);
            cactus.SetActive(true);
        } else if (cactus.activeSelf == true) {
            cactus.SetActive(false);
            sword.SetActive(true);
        } else if (sword.activeSelf == true) {
            sword.SetActive(false);
            magnet.SetActive(true);
        } else if (magnet.activeSelf == true) {
            magnet.SetActive(false);
            glove.SetActive(true);
        }
    }
}
