using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscRenderProfile : MonoBehaviour
{
    int amountToRender = 5;
    List<GameObject> gameObjects ;
    int lastLength;

    public void Initiate()
    {
        foreach(GameObject g in gameObjects)
        {
            g.SetActive(false);
        }
        lastLength = gameObjects.Count;
        for (int i = 0; i < amountToRender; i++)
        {
            gameObjects[i].SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (gameObjects.Count < lastLength)
        {
            for (int i = 0; i < amountToRender; i++)
            {
                gameObjects[i].SetActive(true);
            }
        }
        
        lastLength = gameObjects.Count;
    }
    public void SetGameObjects(List<GameObject> _gameObjects)
    {
        gameObjects = _gameObjects;
    }
}
