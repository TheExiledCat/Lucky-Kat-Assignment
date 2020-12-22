using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscRenderProfile : MonoBehaviour // Occlusion clips the Discs so the performance is way better. Only renders a max of 'ContantValues.DISC_OCCLUSION'
{
    int amountToRender = 5;
    List<GameObject> gameObjects ;
    int lastLength;
    DiscSpawner ds;
    private void Awake()
    {
        
            ds = GetComponent<DiscSpawner>();
            ds.SetDiscs += SetGameObjects;
            ds.OnSpawn += Initiate;
        
    }
    public void Initiate(List<GameObject> _l)
    {
        foreach(GameObject g in _l)
        {
            g.SetActive(false);
        }
        lastLength = _l.Count;
        gameObjects = _l;
        UpdateRender();
    }
    bool Maximum()
    {
        if (gameObjects.Count <= amountToRender)
        {
            Disable();
            return true;
        }
        return false;
    }
    
    private void Disable()
    {
        for (int i = 0; i < amountToRender; i++)
        {
            gameObjects[i].SetActive(true);
        }
        ds.SetDiscs -= SetGameObjects;
        ds.OnSpawn -= Initiate;
        Destroy(this);
    }
    // Update is called once per frame
    void Update()
    {
        
        if (gameObjects.Count < lastLength&&gameObjects.Count>0)
        {
            UpdateRender();
        }
        
        lastLength = gameObjects.Count;
    }
    void UpdateRender()
    {
        if (!Maximum())
        {
            for (int i = 0; i < amountToRender; i++)
            {
                gameObjects[i].SetActive(true);
            }
        }
        

    }
    public void SetGameObjects(List<GameObject> _gameObjects)
    {
        gameObjects = _gameObjects;
        Initiate(_gameObjects);
    }
}
