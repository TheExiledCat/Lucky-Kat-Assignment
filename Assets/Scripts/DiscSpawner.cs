using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscSpawner : MonoBehaviour
{
    GameObject[] loadedObjects;
    LevelData currentLevel;
    [SerializeField]Color poleColor;
    public void GenerateNewLevel(LevelData _level)
    {
        currentLevel = _level;
        ClearStage();
        GenerateStage();
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
    void GenerateStage()
    {
        GeneratePole();
        GenerateDiscs();
    }
    void GeneratePole()
    {
        GameObject Pole = Instantiate(new GameObject(), Vector3.zero + (Vector3.up * (ConstantValues.DISC_SPACE * currentLevel.discArray.Count)), Quaternion.identity);
        DiscVertexShaper dvs = Pole.AddComponent<DiscVertexShaper>();
        dvs.Initialize(0, 0, ConstantValues.DISC_SPACE * currentLevel.discArray.Count,ConstantValues.POLE_RADIUS,20);
        Pole.GetComponent<MeshRenderer>().material = Resources.Load<Material>(ConstantValues.DISC_MATERIAL_PATH);
        Pole.GetComponent<DiscRender>().SetColor(poleColor);
    }
    void GenerateDiscs()
    {

    }
}
