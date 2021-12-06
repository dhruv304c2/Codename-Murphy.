using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUP : MonoBehaviour
{

    public int ResotreHealth = 5;
    public AudioClip PickUpSound;
    public float PickupVolume = 0.8f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var ph = other.GetComponent<PlayerHealth>();
        if (ph)
        {
            if(ph.HP != 100) ph.GainHP(ResotreHealth);
            AudioSource.PlayClipAtPoint(PickUpSound, Camera.main.transform.position, PickupVolume);
            Destroy(gameObject);
        }
    }
}
