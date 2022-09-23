using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float cubeSize = 0.2f;
    public int cubesInRow = 5;
    public Vector3 swordDir;

    float cubesPivotDistance;
    Vector3 cubesPivot;

    public GameObject balloon;

    // Start is called before the first frame update
    void Start()
    {
        //calculate pivot distance
        cubesPivotDistance = cubeSize * cubesInRow / 2;
        //use this value to create pivot vector
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Blade") {

            // postion and rotation information from the ballon, specifically the midpoint of the balloon

            Vector3 bPos = this.transform.position; 
            Vector3 swPos = other.ClosestPoint(bPos); //closest point from the sword
            Vector3 SwordDir = (bPos - swPos).normalized; //normalized vector between balloon and closest sword point
        
            explode(SwordDir);
        }
    }

    public void explode(Vector3 SwordDir) {
        //make object disappear
        gameObject.SetActive(false);

        //loop 3 times to create 5x5x5 pieces in x,y,z coordinates
        for (int x = 0; x < cubesInRow; x++) {
            for (int y = 0; y < cubesInRow; y++) {
                for (int z = 0; z < cubesInRow; z++) {
                    createPiece(x, y, z, SwordDir);
                }
            }
        }
    }

    void createPiece(int x, int y, int z, Vector3 SwordDir) {
        //create piece
        GameObject piece;

        
        piece = Instantiate(balloon, new Vector3(0, 0, 0), Quaternion.identity);

        //set piece position and scale
        piece.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

        //add rigidbody, set mass and add force in direction of sword hit
        Rigidbody body = piece.AddComponent<Rigidbody>();
        body.mass = cubeSize;
        body.AddForce(SwordDir*50f+ Random.onUnitSphere * 25.0f);

    }
}
