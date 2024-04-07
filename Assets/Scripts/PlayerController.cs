using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private bool doubleJump;
    private bool groundCheck;
    private Rigidbody rb; 
    private int count;
    private float movementX;
    private float movementY;
    private float movementZ;
    public float speed = 0;     
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    // Start is called before the first frame update
    void Start()
    {
        winTextObject.SetActive(false);
        SetCountText();
        count = 0; 
        rb = GetComponent <Rigidbody>(); 
        doubleJump = true;
        
    }
    
    private void FixedUpdate() 
    {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);  
    }

    void OnTriggerEnter(Collider other) 
    {
          if (other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
       
        }
    }

    void OnJump()
    {
        Vector3 jump = new Vector3(0.0f, 200.0f, 0.0f);
        if (groundCheck)
        {
            groundCheck = false;
            rb.AddForce(jump);
        }

        if (doubleJump && !groundCheck)
        {
            doubleJump = false;
            rb.AddForce(jump);
        }
    }

    void OnMove (InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x; 
        movementY = movementVector.y;
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.transform.tag == "Ground")
        {
            groundCheck = true;
            doubleJump = true;
        }
    }
    
    void SetCountText() 
    {
       countText.text =  "Count: " + count.ToString();
        if (count >= 9)
        {
           winTextObject.SetActive(true);
        }
    }
}