using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathStarMove : MonoBehaviour
{
     public Transform target;
     public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 1.5f, 0), step);
    }
}
