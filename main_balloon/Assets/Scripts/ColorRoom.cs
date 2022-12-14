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

        UnityEngine.Debug.Log("Collision with: " + this.name);


        if (other.name == "PaintSplatter")
        {
            while (i < numCollisionEvents)
            {
                GameObject paint = null;
                if (gameObject.tag == "Floor")
                {
                    Vector3 pos = new Vector3(collisionEvents[i].intersection[0], 0.0001f, collisionEvents[i].intersection[2]);

                    paint = PoolManager.Instance.GetPoolObject(PoolObjectType.Paint, pos, transform.localRotation.eulerAngles);
                    CheckNearbyObjects("PaintResidue", pos, paint.GetComponent<MeshRenderer>().bounds.size.x);
                } else if (gameObject.tag == "Walls")
                {
                    Vector3 pos = collisionEvents[i].intersection;
                    paint = PoolManager.Instance.GetPoolObject(PoolObjectType.Paint, pos, transform.localRotation.eulerAngles);
                    CheckNearbyObjects("PaintResidue", pos, paint.GetComponent<MeshRenderer>().bounds.size.x);
                } else
                {
                    return;
                }

                if (paint != null)
                {
                    paint.SetActive(true);
                }

                // GameObject paintSplat = Instantiate(paint, pos, paintRotation);
                i++;
            }
        }
        else if (other.name == "PaintSplatterBlueSpheres")
        {
            while (i < numCollisionEvents)
            {
                Vector3 pos = collisionEvents[i].intersection;

                GameObject paint = PoolManager.Instance.GetPoolObject(PoolObjectType.BluePaint, pos, transform.localRotation.eulerAngles);
                CheckNearbyObjects("PaintResidue", pos, paint.GetComponent<SpriteRenderer>().bounds.size.x);

                if (paint != null)
                {
                    //Debug.Log("Paint rot");
                    // paint.transform.position = pos;
                    // paint.transform.rotation = Quaternion.Euler(transform.localRotation.eulerAngles + paint.transform.localRotation.eulerAngles);
                    paint.SetActive(true);
                }

                // GameObject paintSplat = Instantiate(paint, pos, paintRotation);
                i++;
            }
        }
        else if (other.name == "ConfettiExplosion_new(Clone)")
        {
            while (i < numCollisionEvents)
            {
                Vector3 pos = new Vector3(collisionEvents[i].intersection[0], 0.0001f, collisionEvents[i].intersection[2]);

                GameObject confetti = PoolManager.Instance.GetPoolObject(PoolObjectType.Confetti, pos, new Vector3(0, 0, 0));

                // Get random color from array of colors and set material to it
                int rnd = UnityEngine.Random.Range(0, colors.Length);
                var confettiRenderer = confetti.GetComponent<Renderer>();
                confettiRenderer.material.color = colors[rnd];

                CheckNearbyObjects("ConfettiResidue", pos, confetti.GetComponent<MeshRenderer>().bounds.size.x);

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
                CheckNearbyObjects("WaterResidue", pos, water.GetComponent<MeshRenderer>().bounds.size.x);

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

    void CheckNearbyObjects(string balloonResTag, Vector3 point, float objectSize)
    {

        string[] residueTagArray = { "PaintResidue", "WaterResidue", "ConfettiResidue" };
        List<string> residueTagList = new List<string>(residueTagArray);
        residueTagList.Remove(balloonResTag);

        foreach (var residueTag in residueTagList)
        {
            GameObject[] balloonResidues = GameObject.FindGameObjectsWithTag(residueTag);
            foreach (var balloonResidue in balloonResidues)
            {
                var dist = Vector3.Distance(balloonResidue.transform.position, point);
                if (dist < (objectSize))
                {
                    PoolManager.Instance.ReleasePoolObject(balloonResidue, GetObjectsPoolObjectType(balloonResidue));
                }
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
