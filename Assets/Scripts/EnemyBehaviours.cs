using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviours : MonoBehaviour
{
    Transform player;
    Rigidbody2D myRb;
    Collider2D myCollider;
    public bool onPlatform, onGround;
    public float yrange = 3, shootrange = 5, minChaseRange = 3, maxChaseRange = 15f,
        randomizeTimePeriod = 3, verticalRange = 5f, enemySpeed = 6f;
    public float glitchThroughTime = 1f;
    public LayerMask ground, platform;
    Animator anim;
    string primaryLayer = "Enemy";
    string secondaryLayer = "PlayerThroughWalls";

    [Header("Scene Refrences")]
    public Transform LevelTop;
    public Transform LevelBottom;

    float chaseRange;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        myRb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        StartCoroutine(RandomizeChase(randomizeTimePeriod));
        StartCoroutine(JumpLoop(randomizeTimePeriod));
        anim = GetComponent<Animator>();
        LevelBottom = GameObject.Find("LevelBottom").transform;
        LevelTop = GameObject.Find("LevelTop").transform;
        enemySpeed = Random.Range(enemySpeed, 1.8f * enemySpeed);
    }

    // Update is called once per frame
    void Update()
    {
        var p_x = player.position.x;
        var p_y = player.position.y;
        var x = transform.position.x;
        var y = transform.position.y;
        onGround = myCollider.IsTouchingLayers(ground);
        onPlatform = myCollider.IsTouchingLayers(platform);
        
        if (x > p_x + chaseRange)
        { 
            myRb.velocity = new Vector2(-enemySpeed, myRb.velocity.y);
            
        }
        if (x < p_x - chaseRange)
        {
            myRb.velocity = new Vector2(enemySpeed, myRb.velocity.y);     
        }
        if (x < p_x)
        {
            transform.eulerAngles = Vector3.up;
        }
        else
        {
            transform.eulerAngles = Vector3.up * 180;
        }
        if(myRb.velocity.x != 0)
        {
            anim.SetBool("isMove", true);
        }
        else
        {
            anim.SetBool("isMove", false);
        }
        
        if (transform.position.y < LevelBottom.transform.position.y)
        {
            PopThroughTop();
        }
    }

    IEnumerator RandomizeChase(float changePeriod) {
        while (true)
        {
            chaseRange = Random.Range(minChaseRange, maxChaseRange);
            yield return new WaitForSeconds(changePeriod);
        }
    }

    public void ResetLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(primaryLayer);
        anim.SetBool("Jump", false);
    }

    public void PopThroughTop()
    {
        gameObject.layer = LayerMask.NameToLayer(primaryLayer);
        transform.position = new Vector2(transform.position.x, LevelTop.position.y);
        anim.SetBool("Jump", false);
    }

    public IEnumerator JumpLoop(float changePeriod)
    {
        while (true)
        {
            var y = transform.position.y;
            var p_y = player.position.y;
            if (Mathf.Abs(y - p_y) >= verticalRange)
            {
                if (onPlatform)
                {
                    anim.SetBool("Jump", true);
                    gameObject.layer = LayerMask.NameToLayer(secondaryLayer);
                    Invoke("ResetLayer", glitchThroughTime);
                }
            }
            yield return new WaitForSeconds(changePeriod);
        }    
    }
}
