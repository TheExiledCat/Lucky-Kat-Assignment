using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(DiscRenderProfile))]
public class DiscManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> loadedObjects = new List<GameObject>();
    LevelData currentLevel;
    [SerializeField]Color poleColor;
    GameObject Pole;
    GameObject Container;
    DiscRenderProfile drp;
    public event Action OnSet;
    private void Awake()
    {
        drp = GetComponent<DiscRenderProfile>();
        GeneratePole();
    }
    private void Start()
    {
        GameManager.GM.OnScore += DeleteTopDisc;
    }
    private void Update()
    {
        if (GameManager.GM.successfullJump)
        {
            StartCoroutine(MoveUp());
        }
    }
    IEnumerator MoveUp()//moves all the discs up
    {
        float amountToMove= ConstantValues.DISC_SPACE+ConstantValues.DISC_THICKNESS;
        float total = Container.transform.position.y + amountToMove;
        while (Container.transform.position.y < total)
        {
            Container.transform.position += Vector3.up * ConstantValues.JUMP_HEIGHT;
            GameManager.GM.isFalling = true;
           yield return new WaitForEndOfFrame();
        }
        if(Container.transform.position.y>=total)
        {
            GameManager.GM.isFalling = false;
            Container.transform.position = Vector3.up * total;
            Set();
        }
        yield return null;
    }
    void Set()
    {
        OnSet?.Invoke();
    }
    public void GenerateNewLevel(LevelData _level)//generates a new level and deletes the old one
    {
        currentLevel = _level;
        ClearStage();
        GenerateStage();
    }
    void ClearStage()
    {
        if (loadedObjects != null && loadedObjects.Count > 0)
        {
            foreach (GameObject g in loadedObjects)
            {
                Destroy(g);
            }
        }
        loadedObjects.Clear();
    }
    void GenerateStage()
    {
        
        GenerateDiscs();
        drp.SetGameObjects(loadedObjects);
    }
    void GeneratePole()//creates the pole in the center of the screen
    {
        Pole = new GameObject("Pole",typeof(DiscVertexShaper));
        Pole.transform.position = Vector3.zero + (Vector3.up * ConstantValues.POLE_LENGTH);
        DiscVertexShaper dvs = Pole.GetComponent<DiscVertexShaper>();
        dvs.Initialize(0, 0, ConstantValues.POLE_LENGTH,ConstantValues.POLE_RADIUS,20);
        Pole.GetComponent<MeshRenderer>().material = Resources.Load<Material>(ConstantValues.POLE_MATERIAL_PATH);
        Pole.GetComponent<DiscRender>().SetColor(poleColor);
        Container = new GameObject("Container");
        Container.transform.parent = Pole.transform;
        Container.transform.position = Vector3.up*(ConstantValues.POLE_LENGTH-ConstantValues.DISC_SPACE*3);
        Pole.AddComponent<MobileController>();
    }
    void GenerateDiscs()//creates the discs
    {
        for(int i = 0; i < currentLevel.discArray.Count; i++)//loop trough every disc to spawn
        {
            Disc currentDisc = currentLevel.discArray[i];
            GameObject disc = new GameObject("Disc" + i);

            disc.transform.position = Container.transform.position + (Vector3.down * (ConstantValues.DISC_SPACE + ConstantValues.DISC_THICKNESS) * i);
            disc.transform.parent = Container.transform;

            if (currentLevel.discArray[i].amountOfSplits > 1)//multi disc
            {
                //disc.transform.position = Container.transform.position + (Vector3.down * (ConstantValues.DISC_SPACE + ConstantValues.DISC_THICKNESS) * i)
                for (int j = 0; j < currentLevel.discArray[i].amountOfSplits; j++)//check the amount of splits in it and make a divided disc for every split
                {
                    GameObject subDisc = new GameObject("SubDisc" + j, typeof(DiscVertexShaper));
                    
                    
                    DiscVertexShaper dvs = subDisc.GetComponent<DiscVertexShaper>();
                    dvs.Initialize
                        (
                        currentDisc.startRotation + currentDisc.gap * j,
                        ( ((currentDisc.amountOfSplits*currentDisc.gap)+currentDisc.gap*2))
                        );

                    subDisc.transform.parent = disc.transform;

                    subDisc.transform.localPosition = Vector3.zero;
                    subDisc.transform.localEulerAngles = (Vector3.up * (360/currentDisc.amountOfSplits* j));
                    subDisc.GetComponent<MeshRenderer>().material = Resources.Load<Material>(ConstantValues.DISC_MATERIAL_PATH);
                    subDisc.GetComponent<DiscRender>().SetColor(currentLevel.GetColor());
                    
                }
            }
            else // normal disc
            {
                disc.AddComponent<DiscVertexShaper>();
                //disc.transform.position = Container.transform.position + (Vector3.down * (ConstantValues.DISC_SPACE + ConstantValues.DISC_THICKNESS) * i);
                


                DiscVertexShaper dvs = disc.GetComponent<DiscVertexShaper>();
                dvs.Initialize
                    (
                    currentDisc.startRotation,
                    currentDisc.gap
                    );
                //disc.transform.localEulerAngles = (Vector3.up * currentDisc.startRotation);
                disc.GetComponent<MeshRenderer>().material = Resources.Load<Material>(ConstantValues.DISC_MATERIAL_PATH);
                disc.GetComponent<DiscRender>().SetColor(currentLevel.GetColor());
                
            }
            disc.transform.localEulerAngles = currentDisc.startRotation * Vector3.up;
            loadedObjects.Add(disc);
        }
    }
    void DeleteTopDisc()
    {
        Destroy(loadedObjects[0]);
        loadedObjects.RemoveAt(0);
        
        drp.SetGameObjects(loadedObjects);
    }
    public GameObject GetTopDisc()
    {
        return loadedObjects[0];
    }
    public List<GameObject> GetDiscs()
    {
        return loadedObjects;
    }
}
