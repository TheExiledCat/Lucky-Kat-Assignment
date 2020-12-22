using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelGenerator : MonoBehaviour//singleton,holds the data of the levels, im making the first 3 levels of the game, which are all preprogrammed and hard coded.
{
    int level = 0;
    [SerializeField] List<LevelData> levels;
    public event Action<List<LevelData>> OnSave;
    public event Action OnLoad;
    public event Action<int> OnLevelUp;
    public event Action OnGameEnd;
    DiscSpawner ds;
    public static LevelGenerator LG = null;
    private void Awake()//initiates singleton
    {
        if (LG == null)
        {

            LG = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        GameScript.GS.OnStart += Initiate;
        GameScript.GS.OnWin += NextLevel;
        GameScript.GS.OnDeath += RestartLevel;
        OnGameEnd += GameScript.GS.OnGameEnd;

        OnGameEnd += GameScript.GS.OnGameEnd;
       // GameScript.GS.OnWin += CheckLevel;
    }
    void RestartLevel()
    {
        ds.SpawnLevel(levels[level - 1]);
        OnLevelUp?.Invoke(level);
    }
    void Initiate()
    {
        levels = new List<LevelData>();
        ds = GetComponent<DiscSpawner>();
        Load();
        print(levels.Count);
        if (levels.Count == 0)
        {

            CreateLevelData();
            Save();
        }


        NextLevel();
    }
    public void CheckLevel()
    {
        if (level > levels.Count)
        {
            OnGameEnd?.Invoke();
        }
    }
    void CreateLevelData()//creates the leveldata, can be easily filled with levels or just randomness
    {
        #region level 1
        
        levels.Add(new LevelData(new List<Disc>(), Color.gray));
        for (int i = 0; i < 3; i++)
        {
            Disc discToAdd = new Disc(UnityEngine.Random.Range(0, 360), 1);
            levels[0].AddDisc(discToAdd);
        }
        levels[0].AddDisc(new Disc(UnityEngine.Random.Range(0, 360), 3));
        for (int i = 0; i < 3; i++)
        {
            levels[0].AddDisc(new Disc(UnityEngine.Random.Range(0, 360), i + 1));
        }
        for (int i = 0; i < 5; i++)
        {
            levels[0].AddDisc(new Disc(UnityEngine.Random.Range(0, 360), 2, 1));
        }

        levels[0].AddDisc(new Disc(UnityEngine.Random.Range(0, 360), 1, 2));
        for (int i = 0; i < 4; i++)
        {
            levels[0].AddDisc(new Disc(UnityEngine.Random.Range(0, 360), 2,2));
        }
        for (int i = 0; i < 2; i++)
        {
            Disc discToAdd = new Disc(UnityEngine.Random.Range(0, 360), 1,3);
            levels[0].AddDisc(discToAdd);
        }

        levels[0].AddDisc(new Disc(UnityEngine.Random.Range(0, 360), 3));

        levels[0].AddDisc(new Disc(UnityEngine.Random.Range(0, 360),0,0));//EndDisc

        #endregion
        #region level 2
        levels.Add(new LevelData(new List<Disc>(), Color.yellow));
        for (int i = 0; i < 19; i++)
        {
            Disc discToAdd = new Disc(i*10, 1);
            levels[1].AddDisc(discToAdd);
        }

        levels[1].AddDisc(new Disc(UnityEngine.Random.Range(0, 360), 0, 0));//EndDisc
        #endregion
        #region level 3
        levels.Add(new LevelData(new List<Disc>(), Color.green));

        levels[2].AddDisc(new Disc(UnityEngine.Random.Range(0, 360), 1));
        for (int i = 0; i < 19; i++)
        {
            Disc discToAdd = new Disc(UnityEngine.Random.Range(0, 360), (int)Mathf.Floor(UnityEngine.Random.Range(1,4)),3,0);
            levels[2].AddDisc(discToAdd);
        }

        levels[2].AddDisc(new Disc(UnityEngine.Random.Range(0, 360), 0, 0));//EndDisc
        #endregion
    }
    void Load()//load from json
    {
        print("loading...");
        OnLoad?.Invoke();
    }
    void Save()//save to json
    {
        OnSave?.Invoke(levels);
    }
    void NextLevel()//generate the level
    {
        level++;
        CheckLevel();
        ds.SpawnLevel(levels[level-1]);
        OnLevelUp?.Invoke(level);
    }
    public void SetLevelData(List<LevelData> _levels)
    {
        levels = _levels;
    }
    public List<LevelData> GetLevelData()
    {
        return levels;
    }



}
