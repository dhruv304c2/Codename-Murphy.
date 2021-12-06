using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int HP = 100;
    int DamagePerShot = 3;
    public GameObject DeathScreen;
    public Slider slider;
    public Image fill;
    public Color safe;
    public Color not_safe;
    public int safeLimit = 20;
    Animator anim;
    public AudioClip PlayerHit, PlayerDie;
    public float HitVolume = 0.4f, DieVolume = 1f;
    public GameObject MusicPlayer;
    public void Start()
    {
        slider.maxValue = HP;
        slider.value = HP;
        anim = GetComponent<Animator>(); 
    }
    public void Damage()
    {
        HP-= DamagePerShot;
        AudioSource.PlayClipAtPoint(PlayerHit, Camera.main.transform.position, HitVolume);
        slider.value = HP;
        if (HP <= 20)
        {
            fill.color = not_safe;
        }
        else
        {
            fill.color = safe;
        }
        if (HP <= 0)
        {
            anim.Play("PlayerDie");
            //Destroy(GetComponent<PlayerMovement>());
            Destroy(GetComponent<PlayerShooting>());
            Destroy(MusicPlayer.GetComponent<AudioSource>());
            Invoke("PostAnimation", 2f);     
        }
    }

    public void GainHP(int inc)
    {
        HP += inc;
        slider.value = HP;
        if (HP <= 20)
        {
            fill.color = not_safe;
        }
        else
        {
            fill.color = safe;
        }
    }

    void PostAnimation()
    {
        DeathScreen.SetActive(true);
    }
}
