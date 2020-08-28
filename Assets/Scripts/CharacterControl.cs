using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterControl : MonoBehaviour
{
    public bool isGrounded;
    private Rigidbody rb;
    private GameManager gaman;
    private float jumpForce;
    public bool jumping;
    public bool removed;
    private Animator anim;
    public int id;
    private float fallMultiplier;


    private void FixedUpdate()
    {
        if(rb.velocity.y < 0 && jumping)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    private void Start()
    {
        
        gaman = FindObjectOfType<GameManager>();
        fallMultiplier = gaman.fallMultiplier;
        Camera cam = Camera.main;
        gaman.charpositions.Add(new Vector2(cam.WorldToScreenPoint(transform.position).x , transform.GetSiblingIndex()));
        anim = gameObject.GetComponent<Animator>();
        
        jumpForce = gaman.jumpForce;
        rb = GetComponent<Rigidbody>();
        Reset();

        id = transform.GetSiblingIndex();
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButton(0) && isGrounded)
        {
            
            Jump();
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor" && !isGrounded && jumping)
        {
            isGrounded = true;
            jumping = false;
            anim.SetTrigger("run");
        }
        if (collision.gameObject.tag == "StaticObject")
        {
            RemoveFromList();
            anim.SetTrigger("fall");
            transform.SetParent(collision.transform);
        }
    }

    public void RemoveFromList()
    {
        if (!removed)
        {
            gaman.OnePlayerLess();
            gaman.playersLeft.Remove(this.gameObject);
            removed = true;

        }
    }

    public void Jump()
    {
        isGrounded = false;
        if (!jumping)
        {

            jumping = true;
            
            rb.velocity = new Vector3(0, jumpForce, 0);
            anim.SetTrigger("jump");
        }
    }

    private void Reset()
    {
        jumping = false;
    }
}
