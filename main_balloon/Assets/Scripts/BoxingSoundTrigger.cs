 using UnityEngine;
 using System.Collections;
 using UnityEngine.Audio;
 
  //Add this Script Directly to The Death Zone
 public class BoxingSoundTrigger : MonoBehaviour
 {   // Add your Audi Clip Here;
     // This Will Configure the  AudioSource Component; 
     // MAke Sure You added AudioSouce to death Zone;
     private AudioSource BoxHit;
     void Start ()   
     {
     }        
 
     void OnTriggerEnter ()  //Plays Sound Whenever collision detected
     {
        BoxHit = GetComponent<AudioSource>();
        BoxHit.time = 0.1f;
        BoxHit.Play();
     }
          // Make sure that deathzone has a collider, box, or mesh.. ect..,
          // Make sure to turn "off" collider trigger for your deathzone Area;
          // Make sure That anything that collides into deathzone, is rigidbody;
 }