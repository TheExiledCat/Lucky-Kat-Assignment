using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(DiscSpawner))]
public class LevelManager : MonoBehaviour
{
    int level = 0;
    LevelData[] levels= new LevelData[10];
    DiscSpawner ds;
    private void Start()
    {
        ds = GetComponent<DiscSpawner>();
        levels[0] = new LevelData(new List<Disc>(20),Color.black);
        for (int i = 0; i < 3; i++)
        {
            Disc discToAdd = new Disc(Random.Range(0, 360),1);
            levels[0].AddDisc(discToAdd);
        }
    }
    void LoadLevel()
    {
        level++;
        ds.GenerateNewLevel(levels[level-1]);
    }
    

}
