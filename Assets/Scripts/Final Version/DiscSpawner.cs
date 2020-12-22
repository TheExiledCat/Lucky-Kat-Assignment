using System;
using System.Collections.Generic;
using UnityEngine;

public class DiscSpawner : MonoBehaviour
{
    [SerializeField]
    List<GameObject> loadedDiscs = new List<GameObject>();
    public event Action<List<GameObject>> SetDiscs;
    public event Action<List<GameObject>> OnSpawn;
    public event Action OnGameOver;
    GameObject Pole;
    private void Start()
    {
        SetDiscs += GameScript.GS.SetTopDiscs;
        GameScript.GS.OnScore += RemoveTopDisc;
    }
    public void SpawnLevel(LevelData ld)
    {
        if (ld == null)
        {
            OnGameOver?.Invoke();
        }
        Clear();
        GeneratePole(ld);
        GenerateDiscs(ld);
        SetDiscs?.Invoke(loadedDiscs); 
    }
    void Clear()
    {
        foreach(GameObject g in loadedDiscs)
        {
            Destroy(g);
        }
        Destroy(Pole);
        loadedDiscs.Clear();
    }
    void RemoveTopDisc(int _score=0)
    {
        if (loadedDiscs.Count > 0)
        {

            Destroy(loadedDiscs[0]);
            loadedDiscs.RemoveAt(0);
            SetDiscs?.Invoke(loadedDiscs);
        }
    }
    void GenerateDiscs(LevelData ld)
    {
        int index = 0;
        foreach (Disc d in ld.GetDiscs())
        {
            
            GameObject disc = Instantiate(Resources.Load<GameObject>("Prefabs/Helix " + d.amountOfSplits + " Slice"), Vector3.up*ConstantValues.POLE_LENGTH+Vector3.down * index * ConstantValues.DISC_SPACE, Quaternion.identity);
            int i = 0;
            int zones = d.amountOfKillZones;
            foreach (Transform t in disc.transform)
            {
                MeshRenderer m = t.gameObject.GetComponent<MeshRenderer>();
                m.material = Resources.Load<Material>("Mats/Disc");
                
                MaterialPropertyBlock mpb = new MaterialPropertyBlock();
                
                print(i);
                if (d.amountOfKillZones == 0)
                {


                    mpb.SetColor("Color", ld.color);
                    m.SetPropertyBlock(mpb);

                }
                else
                {
                    
                    if (i % 3 == 0&&zones>0)
                    {
                        mpb.SetColor("Color", Color.black);
                        t.tag = ConstantValues.ZONE_TAG;
                        zones--;
                       
                    }
                    else
                    {
                        mpb.SetColor("Color", ld.color);
                    }
                    m.SetPropertyBlock(mpb);
                }
                i++;
            }
            disc.transform.eulerAngles = d.startRotation * Vector3.up;
            disc.transform.parent = Pole.transform;
            loadedDiscs.Add(disc);
            index++;

        }
        OnSpawn?.Invoke(loadedDiscs);
    }
    void GeneratePole(LevelData _ld)
    {
        Pole = new GameObject("Pole", typeof(DiscVertexShaper));
        float length = ( _ld.discArray.Count * ConstantValues.POLE_LENGTH);
        Pole.transform.position = Vector3.up*length/10;
        DiscVertexShaper dvs = Pole.GetComponent<DiscVertexShaper>();
        dvs.Initialize(0, 0,length,1, 20);
        Pole.GetComponent<MeshRenderer>().material = Resources.Load<Material>(ConstantValues.POLE_MATERIAL_PATH);
        Pole.GetComponent<DiscRender>().SetColor(Color.red);
        Pole.AddComponent<MobileController>();
    }
}
