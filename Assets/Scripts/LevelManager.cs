using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DiscSpawner))]
[RequireComponent(typeof(LevelStores))]

public class LevelManager : MonoBehaviour//holds the data of the levels, im making the first 10 levels of the game, which are all preprogrammed and hard coded.
{
    int level = 0;
    [SerializeField] List<LevelData> levels;
    DiscSpawner ds;
    public event Action<List<LevelData>> OnSave;
    public event Action OnLoad;
    private void Start()
    {

        levels = new List<LevelData>();
        Load();
        print(levels.Count);
        if (levels.Count==0) {
            
            CreateLevelData();
            Save();
        }


        NextLevel();
    }
    void CreateLevelData()
    {
        #region level 1
        ds = GetComponent<DiscSpawner>();
        levels.Add(new LevelData(new List<Disc>(20), Color.black));
        for (int i = 0; i < 3; i++)
        {
            Disc discToAdd = new Disc(UnityEngine.Random.Range(0, 360), 1);
            levels[0].AddDisc(discToAdd);
        }
        levels[0].AddDisc(new Disc(UnityEngine.Random.Range(0, 360), 3));
        for(int i = 0; i < 3; i++)
        {
            levels[0].AddDisc(new Disc(UnityEngine.Random.Range(0, 360), i + 1));
        }
        for(int i = 0; i < 5; i++)
        {
            levels[0].AddDisc(new Disc(UnityEngine.Random.Range(0, 360), 2));
        }

        levels[0].AddDisc(new Disc(UnityEngine.Random.Range(0, 360),1 ));
        for (int i = 0; i < 4; i++)
        {
            levels[0].AddDisc(new Disc(UnityEngine.Random.Range(0, 360), 2));
        }
        for (int i = 0; i < 2; i++)
        {
            Disc discToAdd = new Disc(UnityEngine.Random.Range(0, 360), 1);
            levels[0].AddDisc(discToAdd);
        }

        levels[0].AddDisc(new Disc(UnityEngine.Random.Range(0, 360), 3));
        
        levels[0].AddDisc(new Disc(UnityEngine.Random.Range(0, 360), 3));//EndDisc
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
        ds.GenerateNewLevel(levels[level - 1]);
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
