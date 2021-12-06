using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHp;
    public int HP = 4;
    EnemyCounter enemyCounter;
    public ParticleSystem SmokeParticles;
    public GameObject Drop;
    public Transform DropPoint;
    public ParticleSystem Explosion,Laser;
    public float DropChance = 0.4f;
    public AudioClip ExplosionSound;
    public float ExplosionVolume=1;
    public AudioClip HitSound;
    public float HitVolume;

    private void Start()
    {
        enemyCounter = FindObjectOfType<EnemyCounter>();
        HP = maxHp;
    }

    public void Damage()
    {
        HP--;
        AudioSource.PlayClipAtPoint(HitSound, Camera.main.transform.position, HitVolume);
        if (HP <= maxHp/2)
        {
            SmokeParticles.Play();
        }
        if (HP <= 0)
        {
            GetComponent<Animator>().Play("EnemyDie");
            Destroy(GetComponent<EnemyBehaviours>());
            Destroy(GetComponent<CapsuleCollider2D>());
            Destroy(Laser);
            float roll = Random.Range(0f, 3f);
            if (roll <= DropChance)
            {
                var d = Instantiate(Drop);
                d.transform.position = DropPoint.position;
            }
            enemyCounter.CountDown();
            AudioSource.PlayClipAtPoint(ExplosionSound, Camera.main.transform.position, ExplosionVolume);
            Invoke("PostAnimation", 1.5f);
        }
    }

    public void PostAnimation()
    {
        Explosion.Play();
        Explosion.transform.parent = null;
        Invoke("DeleteRemains", 2f);
        Destroy(gameObject);
    }

    public void DeleteRemains()
    {
        Destroy(Explosion.gameObject);
    }
}
