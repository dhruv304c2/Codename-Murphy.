using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public ParticleSystem laser;
    public float timeBetweenShots = 0.2f;
    Animator anim;
    public AudioClip LaserSound;
    public float LaserVolume=0.5f;
    public AudioClip ErrorSound;
    public float ErrorVolume = 0.5f;
    ControlRandomizer randomizer;

    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        randomizer = GetComponent<ControlRandomizer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetButtonDown(randomizer.inputTag))
        {

            anim.SetLayerWeight(1, 1f);
            if (timer >= timeBetweenShots)
            {
                AudioSource.PlayClipAtPoint(LaserSound, Camera.main.transform.position, LaserVolume);
                laser.Emit(1);
                timer = 0;
            }
        }
        foreach( string tag in randomizer.InputTags)
        {
            if (Input.GetButtonDown(tag) && tag != randomizer.tag)
            {
                anim.SetLayerWeight(1, 1f);
                AudioSource.PlayClipAtPoint(ErrorSound, Camera.main.transform.position, ErrorVolume);
            }
        }
        AnimationReset();     
    }
  
    public void AnimationReset()
    {
        anim.SetLayerWeight(1, Mathf.Lerp(anim.GetLayerWeight(1), 0, 0.1f));
    }
}


