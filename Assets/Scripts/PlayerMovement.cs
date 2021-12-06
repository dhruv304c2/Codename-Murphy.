using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Atributes")]
    public float playerSpeed = 1;
    public float glitchThroughTime = 1f;
    public bool onPlatform;
    public bool onGround;

    [Header("Required")]
    public LayerMask platformLayer;
    public LayerMask groundLayer;
    [Header("Scene Refrences")]
    public Transform LevelTop;
    public Transform LevelBottom;
    public GameObject PauseMenue;

    Collider2D myCollider;
    string primaryLayer = "Player";
    string secondaryLayer = "PlayerThroughWalls";
    Rigidbody2D myRb;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();
        myRb = GetComponent<Rigidbody2D>();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        onPlatform = myCollider.IsTouchingLayers(platformLayer);
        onGround = myCollider.IsTouchingLayers(groundLayer);

        if (Input.GetKeyDown(KeyCode.Escape)) PauseMenue.SetActive(true);

        //Horizontal movement...
        Vector3 worldPosition;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        myRb.velocity = new Vector2(playerSpeed * Input.GetAxis("Horizontal"), myRb.velocity.y);
        if (transform.position.x < worldPosition.x)
        {
            transform.eulerAngles = Vector3.up;
        }
        else if (transform.position.x > worldPosition.x)
        {
            transform.eulerAngles = Vector3.up * 180;
        }
        if (transform.eulerAngles == Vector3.up && myRb.velocity.x < 0.1f)
        {
            anim.SetBool("MoveBack", true);
        }
        else if (transform.eulerAngles == Vector3.up * 180 && myRb.velocity.x > 0.1f)
        {
            anim.SetBool("MoveBack", true);
        }
        else
        {
            anim.SetBool("MoveBack", false);
        }
        if (myRb.velocity.x != 0 && (onPlatform||onGround))
        {
            anim.SetBool("isMove", true);
        }
        else
        {
            anim.SetBool("isMove", false);
        }

        //Falling through platforms

        if (Input.GetButtonDown("Jump") && (onGround||onPlatform))
        {
            if (onPlatform)
            {
                anim.SetBool("Jump", true);
                gameObject.layer = LayerMask.NameToLayer(secondaryLayer);
                Invoke("ResetLayer", glitchThroughTime);
            }
        }
        if(transform.position.y < LevelBottom.transform.position.y)
        {
            PopThroughTop();
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
}