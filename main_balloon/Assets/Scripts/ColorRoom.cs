// using System;
using System.Collections;
using System.Collections.Generic;
// using System.Collections.Specialized;
using UnityEngine;

public class ColorRoom : MonoBehaviour
{
    //public GameObject confetti;
    //public GameObject paint;
    //public GameObject water;
    public List<ParticleCollisionEvent> collisionEvents;

    private ParticleSystem part;
    private Color[] colors = { new Color(1, 0, 0.8416281f, 1), new Color(1, 0.9679406f, 0, 1), new Color(0.1362005f, 1, 0, 1), new Color(0, 1, 0.9706755f, 1), new Color(0.6878676f, 0, 1, 1), new Color(1, 0.3245357f, 0, 1) };
    //private Quaternion paintRotation;

    // Start is called before the first frame update
    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();

        //paintRotation = Quaternion.Euler(transform.localRotation.eulerAngles + paint.transform.localRotation.eulerAngles);
    }

    void OnParticleCollision(GameObject other)
    {
        part = other.GetComponent<ParticleSystem>();
        int numCollisionEvents = part.GetCollisionEvents(gameObject, collisionEvents);
        int i = 0;


        if (other.name == "PaintSplatterSpheres")
        {
            while (i < numCollisionEvents)
            {
                Vector3 pos = collisionEvents[i].intersection;

                GameObject paint = PoolManager.Instance.GetPoolObject(PoolObjectType.Paint, pos, transform.localRotation.eulerAngles);
                CheckNearbyObjects(pos, paint.GetComponent<SpriteRenderer>().bounds.size.x);

                if (paint != null)
                {
                    Debug.Log("Paint rot");
                    // paint.transform.position = pos;
                    // paint.transform.rotation = Quaternion.Euler(transform.localRotation.eulerAngles + paint.transform.localRotation.eulerAngles);
                    paint.SetActive(true);
                }

                // GameObject paintSplat = Instantiate(paint, pos, paintRotation);
                i++;
            }
        }
        else if (other.name == "ConfettiExplosion 1(Clone)")
        {
            while (i < numCollisionEvents)
            {
                Vector3 pos = collisionEvents[i].intersection;

                GameObject confetti = PoolManager.Instance.GetPoolObject(PoolObjectType.Confetti, new Vector3(pos[0], 0.0001f, pos[2]), new Vector3(0, 0, 0));

                // Get random color from array of colors and set material to it
                int rnd = UnityEngine.Random.Range(0, colors.Length);
                var confettiRenderer = confetti.GetComponent<Renderer>();
                confettiRenderer.material.color = colors[rnd];

                CheckNearbyObjects(pos, confetti.GetComponent<MeshRenderer>().bounds.size.x);

                if (confetti != null)
                {
                    //confetti.transform.position = new Vector3(pos[0], 0.0001f, pos[2]);
                    confetti.SetActive(true);
                }
                // GameObject confettiSquare = Instantiate(confetti, new Vector3(pos[0], 0.0001f, pos[2]), Quaternion.identity);

                i++;
            }
        }
        else if (other.name == "WaterSplatterSpheres")
        {
            while (i < numCollisionEvents)
            {
                Vector3 pos = collisionEvents[i].intersection;

                GameObject water = PoolManager.Instance.GetPoolObject(PoolObjectType.Water, new Vector3(pos[0], 0.0001f, pos[2]), new Vector3 (0,0,0));
                CheckNearbyObjects(pos, water.GetComponent<SpriteRenderer>().bounds.size.x);

                if (water != null)
                {
                    // water.transform.position = new Vector3(pos[0], 0.0001f, pos[2]);
                    water.SetActive(true);
                }

                // GameObject waterSplat = Instantiate(water, new Vector3(pos[0], 0.0001f, pos[2]), water.transform.rotation);
                i++;
            }
        }
    }

    void CheckNearbyObjects(Vector3 point, float objectSize)
    {
        GameObject[] balloonResidues = GameObject.FindGameObjectsWithTag("BalloonResidue");

        foreach (var balloonResidue in balloonResidues)
        {
            var dist = Vector3.Distance(balloonResidue.transform.position, point);
            if (dist < (objectSize))
            {
                PoolManager.Instance.ReleasePoolObject(balloonResidue, GetObjectsPoolObjectType(balloonResidue));
                //Destroy(balloonResidue);
            }
        }
    }

    private PoolObjectType GetObjectsPoolObjectType(GameObject ob)
    {
        if (ob.transform.parent.name == "PaintContainer")
            return PoolObjectType.Paint;
        else if (ob.transform.parent.name == "ConfettiContainer")
            return PoolObjectType.Confetti;
        else
            return PoolObjectType.Water;
    }

   


}
