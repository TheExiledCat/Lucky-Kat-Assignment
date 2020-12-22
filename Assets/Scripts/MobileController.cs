using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileController : MonoBehaviour // controls the pole with mobile controls or arrow keys on keyboard
{
    [SerializeField] Vector2 startPos;
        float swipeDist;
    Vector3 initialRotation;
    // Start is called before the first frame update
    void Start()
    {
        GameScript.GS.OnDeath += Disable;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touches.Length > 0)//check if the screen is touched
        {
            Touch t = Input.GetTouch(0);
            if (t.phase ==TouchPhase.Began)//is it the first time pressing the screen in one phase? acts like an Input.getkeydown
            {
                //check where the swipe started
                startPos = t.position;
                initialRotation = transform.localEulerAngles;
                print(startPos);
            }
            //the speed of the swipe, multiplied by delta time to improve frame sync
            swipeDist = t.deltaPosition.x*ConstantValues.POLE_ROTATION_SPEED*t.deltaTime   ;

            
        }
        else
        {
            swipeDist = 0;
        }
        transform.localEulerAngles+= Vector3.up * swipeDist;
        transform.localEulerAngles += Vector3.up * Input.GetAxis("Horizontal")*Time.deltaTime*100;//for testing
    }
    void Disable()
    {
        this.enabled = false;
    }
}
