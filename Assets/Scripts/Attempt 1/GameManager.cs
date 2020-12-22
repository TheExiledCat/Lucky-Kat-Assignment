using System;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LevelManager))]
public class GameManager : MonoBehaviour
{
    public static GameManager GM = null;
    public event Action OnScore;
    DiscManager dm;
    GameObject currentDisc;
    public bool successfullJump = false;
    public bool isFalling = false;
    private void Awake()//create singleton game manager
    {
        if (GM == null)
        {
            GM = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        dm = GetComponent<DiscManager>();
    }
    GameObject Ball;
    
    // Start is called before the first frame update
    void Start()
    {
        Ball = GameObject.FindGameObjectWithTag(ConstantValues.PLAYER_TAG);
        Ball.GetComponent<BallBounce>().OnBounce += CheckScore;
        dm.OnSet += CheckScore;
    }

    // Update is called once per frame
    void Update()
    {



        successfullJump = false;


    }
    void CheckScore()
    {

        currentDisc = dm.GetTopDisc();
        if (!isFalling)
        {
            if (currentDisc.transform.childCount == 0)
            {
                Vector3 diff = Ball.transform.position - currentDisc.transform.position;
                diff.x = 0;
                diff.y = 0;
                float angle = Vector3.SignedAngle(currentDisc.transform.right, diff, Vector3.up);
                angle *= -1;
                if (angle > 0 && angle < ConstantValues.GAP)
                {
                    Score();
                    return;
                }
                print("Bounce");
            }
            else
            {
                if (CheckSplits())
                {
                    Score();
                }
                
            }
        }
        
       
       
    }
    bool CheckSplits()
    {
        foreach(Transform t in currentDisc.transform)
        {
            Vector3 diff = Ball.transform.position - t.transform.position;
            diff.x = 0;
            diff.y = 0;
            float angle = Vector3.SignedAngle(t.transform.right, diff, Vector3.up);
            angle *= -1;
            print(angle);
            if (angle > 0 && angle < ConstantValues.GAP)
            {
                return true;
            }
            
        }
        return false;
    }
    void Score()
    {
        successfullJump = true;
        OnScore?.Invoke();
    }
}
