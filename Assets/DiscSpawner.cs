using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscSpawner : MonoBehaviour
{
    GameObject[] loadedObjects;
    LevelData currentLevel;
    public void GenerateNewLevel(LevelData _level)
    {
        currentLevel = _level;
        ClearStage();
    }
    void ClearStage()
    {
        if (loadedObjects != null && loadedObjects.Length > 0)
        {
            foreach (GameObject g in loadedObjects)
            {
                Destroy(g);
            }
        }
    }
}
