using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRoom : MonoBehaviour
{
    public GameObject confetti;
    public GameObject paint;
    public GameObject water;
    public List<ParticleCollisionEvent> collisionEvents;

    private ParticleSystem part;
    private Color[] colors = { new Color(1, 0, 0.8416281f, 1), new Color(1, 0.9679406f, 0, 1), new Color(0.1362005f, 1, 0, 1), new Color(0, 1, 0.9706755f, 1), new Color(0.6878676f, 0, 1, 1), new Color(1, 0.3245357f, 0, 1) };
    private Quaternion paintRotation;

    // Start is called before the first frame update
    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
        paintRotation = Quaternion.Euler(transform.localRotation.eulerAngles + paint.transform.localRotation.eulerAngles);
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
                GameObject paintSplat = Instantiate(paint, pos, paintRotation);
                i++;
            }
        } else if(other.name == "ConfettiExplosion 1(Clone)")
        {
            while (i < numCollisionEvents)
            {
                Vector3 pos = collisionEvents[i].intersection;
                // Get random color from array of colors and set material to it
                int rnd = UnityEngine.Random.Range(0, colors.Length);
                GameObject confettiSquare = Instantiate(confetti, new Vector3(pos[0], 0.0001f, pos[2]), Quaternion.identity);
                var confettiRenderer = confettiSquare.GetComponent<Renderer>();
                confettiRenderer.material.color = colors[rnd];
                i++;
            }
        } else if(other.name == "WaterSplatterSpheres") {

        } while (i < numCollisionEvents)
            {
                Vector3 pos = collisionEvents[i].intersection;
                // Get random color from array of colors and set material to it
                GameObject waterSplat = Instantiate(water, new Vector3(pos[0], 0.0001f, pos[2]), water.transform.rotation);
                i++;
            }
    }


}
