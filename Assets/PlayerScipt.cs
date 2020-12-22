using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScipt : MonoBehaviour
{
    Rigidbody rb;
    public event Action OnDeath;
    Bouncer b;
    Vector3 initialPos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        OnDeath += GameScript.GS.Die;
        b = GetComponent<Bouncer>();
        initialPos = transform.position;
        GameScript.GS.OnDeath += Respawn;
        GameScript.GS.OnWin += Respawn;
    }

    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag(ConstantValues.ZONE_TAG))
        {
            if (GameScript.GS.CheckCombo())
            {
                b.Bounce();
            }
            else
            {

                Die();
            }
        }
    }
    void Respawn()
    {
        transform.position = initialPos;
        Enable();
    }
    void Enable()
    {

        rb.isKinematic = false;
        GetComponent<Bouncer>().enabled = true;
    }
    void Disable()
    {
        rb.isKinematic = true;
        GetComponent<Bouncer>().enabled = false;
    }
    void Die()
    {
        Disable();
        OnDeath?.Invoke();
    }
}
