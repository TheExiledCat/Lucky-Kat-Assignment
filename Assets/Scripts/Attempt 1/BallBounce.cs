using System;
using System.Collections.Generic;
using UnityEngine;

public class BallBounce : MonoBehaviour
{
    Rigidbody rb;
    public event Action OnBounce;
    
    bool firstBounce = true;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        rb.useGravity = true;
        if (GameManager.GM.isFalling)
        {
            rb.useGravity = false;
        }
        if (rb.velocity.y ==-1)
        {
            
            
                Fall();
            
            
        }
       
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        
        
            Bounce();

        
    }
    void Bounce()
    {

        rb.velocity = Vector3.zero;
        OnBounce?.Invoke();
        if (!GameManager.GM.successfullJump)
            {
                rb.AddForce(Vector3.up * ConstantValues.JUMP_HEIGHT * 1000);
        }
       
            
        
    }
    void Fall()
    {

        rb.AddForce(Vector3.down * ConstantValues.JUMP_HEIGHT * 2000);
    }
}
