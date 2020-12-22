using System;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    Rigidbody rb;
    public event Action OnBounce;
    enum state
    {
        UP,
        DOWN
    }
    state Movement=state.DOWN;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {


        if (rb.velocity.y <= 0)
        {
            Movement = state.DOWN;
        }
    }
    public void Bounce()
    {
        if (Movement == state.DOWN)
        {
            print("Boing");
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * ConstantValues.JUMP_HEIGHT * 250);
            Movement = state.UP;
            OnBounce?.Invoke();
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.gameObject.CompareTag(ConstantValues.ZONE_TAG))
        {
            Bounce();
        }
            


    }
    
}
