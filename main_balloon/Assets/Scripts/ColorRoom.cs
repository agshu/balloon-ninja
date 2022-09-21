using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRoom : MonoBehaviour
{
    public ParticleSystem part;
    public GameObject confetti;
    public List<ParticleCollisionEvent> collisionEvents;

    private Color[] colors = { new Color(1, 0, 0.8416281f, 1), new Color(1, 0.9679406f, 0, 1), new Color(0.1362005f, 1, 0, 1), new Color(0, 1, 0.9706755f, 1), new Color(0.6878676f, 0, 1, 1), new Color(1, 0.3245357f, 0, 1) };

    // Start is called before the first frame update
    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        part = other.GetComponent<ParticleSystem>();
        int numCollisionEvents = part.GetCollisionEvents(gameObject, collisionEvents);
        int i = 0;

        while (i < numCollisionEvents)
        {
            Vector3 pos = collisionEvents[i].intersection;
            
            // Get random color from array of colors and set material to it
            int rnd = UnityEngine.Random.Range(0, colors.Length);
            GameObject confettiSquare = Instantiate(confetti, new Vector3(pos[0], 0.01f, pos[2]), Quaternion.identity);
            var confettiRenderer = confettiSquare.GetComponent<Renderer>();
            confettiRenderer.material.color = colors[rnd];
            i++;
        }
    }


}
