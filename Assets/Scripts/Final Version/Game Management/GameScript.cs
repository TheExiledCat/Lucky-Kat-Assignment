using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameScript : MonoBehaviour // manages the gameplay and win conditions, singleton
{
    public static GameScript GS;
    public event Action<int> OnScore;
    public event Action OnDeath;
    public event Action OnStart;
    public event Action OnWin;
    [SerializeField]GameObject currentDisc, NextDisc;
    GameObject player;
    [SerializeField] int points = 0;
    public int combo = 0;
    private void Awake()//initiates singleton
    {
        if (GS == null)
        {

            GS = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        player = GameObject.FindGameObjectWithTag(ConstantValues.PLAYER_TAG);
        player.GetComponent<Bouncer>().OnBounce += ResetCombo;
        
    }
    private void Start()
    {
        player.GetComponent<Bouncer>().OnBounce += AudioManager.AM.Bounce;
        OnStart?.Invoke();
    }
    public void SetTopDiscs(List<GameObject> l)
    {

        currentDisc = l[0];
        if (l.Count > 1)
        {

            NextDisc = l[1];
        }
        else
        {
            Win();
        }
        foreach (Transform t in currentDisc.transform)
        {
            if(t.GetComponent<MeshCollider>())
            t.GetComponent<MeshCollider>().enabled=true;
        }

    }
    public void Die()
    {
        OnDeath?.Invoke();
        points = 0;
        combo = 0;
    }
    public bool CheckCombo()
    {
        if (combo >= 3)
        {
            return true;
        }
        return false;
    }
    public void ResetCombo()//when the ground is touched by the ball the combo resets
    {
        if (CheckCombo())
        {
            points++;
            OnScore?.Invoke(points);
        }
        combo = 0;
        
    }
    void Score()//upon going trough a whole
    {
        points++;
        OnScore?.Invoke(points);
        combo++;
    }
    void Win()
    {
        print("GG");
        points = 0;
        LevelGenerator.LG.CheckLevel();
        OnWin?.Invoke();
        combo = 0;
        
    }
    public void OnGameEnd()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        Destroy(AudioManager.AM.gameObject);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {

        if (player && currentDisc)
        {
            if (player.transform.position.y < currentDisc.transform.position.y)
            {
                Score();
            }
        }
        
    }
}
