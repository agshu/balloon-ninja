using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PSPlaneIntersection : MonoBehaviour
{
    public GameObject confetti;
    public ParticleSystem PSystem;
    public List<ParticleCollisionEvent> collisionEvents;
    //private ParticleCollisionEvent[] CollisionEvents;
    private int maxCollisions = 10;

    void Start()
    {
        //PSystem = ParticleSystem.GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        //CollisionEvents = new ParticleCollisionEvent[8];
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = PSystem.GetCollisionEvents(other, collisionEvents);
        UnityEngine.Debug.Log("numcollisionevents " + numCollisionEvents);
        int i = 0;

        while (i < numCollisionEvents)
        {
            Vector3 pos = collisionEvents[i].intersection;
            GameObject confettiSquare = Instantiate(confetti, new Vector3(pos[0], 0.1f, pos[2]), Quaternion.identity);
            i++;
        }
        UnityEngine.Debug.Log("i " + i);
        //int counter = 0;
        //int collCount = PSystem.GetSafeCollisionEventSize();
        //if (collCount > collisionEvents.Count)
        //    collisionEvents = new List<ParticleCollisionEvent>();

        //int numCollisionEvents = PSystem.GetCollisionEvents(other, collisionEvents);
        //UnityEngine.Debug.Log("num coll events " + numCollisionEvents);

        //for (int i = 0; i < numCollisionEvents; i++)
        //{
        //        counter++;
        //        InstantiateEffect();
        //        Instantiate(confetti, new Vector3(collisionEvents[i].intersection[0], 0.1f, collisionEvents[i].intersection[2]), confetti.transform.rotation);
        //        UnityEngine.Debug.Log(counter);
        //    }
        //    //UnityEngine.Debug.Log("hej: " + collisionEvents[i].intersection);

        //}
    }
}



//// Start is called before the first frame update
//void Start()
//{
//    PSystem = GetComponent<ParticleSystem>();
//    CollisionEvents = new ParticleCollisionEvent[8];
//}

//public void OnParticleCollision(GameObject other) 
//{
//    UnityEngine.Debug.Log("collided");
//    //int collCount = PSystem.GetSafeCollisionEventSize();

//    //if (collCount > CollisionEvents.Length)
//    //    CollisionEvents = new ParticleCollisionEvent[collCount];

//    //int eventCount = PSystem.GetCollisionEvents(other, CollisionEvents);

//    //for (int i = 0; i < eventCount; i++)
//    //{
//    //    UnityEngine.Debug.Log("hej: " + CollisionEvents[i].intersection);
//    //    Instantiate(confetti, CollisionEvents[i].intersection, confetti.transform.rotation);

//    //}
//}

//public void OnParticleCollision(GameObject other)
//{
//    int counter = 0;
//    UnityEngine.Debug.Log("collided");
//    int collCount = PSystem.GetSafeCollisionEventSize();

//    if (collCount > CollisionEvents.Length)
//        CollisionEvents = new ParticleCollisionEvent[collCount];

//    int eventCount = PSystem.GetCollisionEvents(other, CollisionEvents);

//    for (int i = 0; i < eventCount; i++)
//    {
//        if (counter < maxCollisions)
//        {
//            //UnityEngine.Debug.Log("hej: " + CollisionEvents[i].intersection);
//            Instantiate(confetti, CollisionEvents[i].intersection, confetti.transform.rotation);
//            counter++;
//        }

//    }
//    UnityEngine.Debug.Log("counter " + counter);
//}


