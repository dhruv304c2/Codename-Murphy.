using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers : MonoBehaviour
{
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;
    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        EnemyHealth eh = other.GetComponent<EnemyHealth>();
        PlayerHealth ph = other.GetComponent<PlayerHealth>();
        int i = 0;

        while (i < numCollisionEvents)
        {
            if (eh)
            {
                eh.Damage();
            }
            if (ph)
            {
                ph.Damage();
            }
            i++;
        }
    }
}
