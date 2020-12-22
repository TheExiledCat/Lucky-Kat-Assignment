using System.IO;
using UnityEngine;
using System.Collections.Generic;
public class LevelStores : MonoBehaviour//saves and loads levels to files
{
    LevelGenerator lg;
    string storedPath;
    private void Start()
    {

        storedPath = Application.persistentDataPath + "/Level";//creates file on device for storage
        lg = GetComponent<LevelGenerator>();
        lg.OnSave += SaveToJson;
        lg.OnLoad += LoadFromJson;
    }
    void SaveToJson(List<LevelData> _levelsToSave)//save the levels to json
    {
       print("Saving to: "+storedPath);
        string json="";
        int index = 1;
        foreach(LevelData ld in _levelsToSave)
        {
            if(File.Exists(storedPath + index + ".json"))
            {
                File.Delete(storedPath +  index + ".json");
            }
            json =   SaveToString(ld) ;
            File.WriteAllText(storedPath +  index+".json", json);
            index++;
        }
        
        
    }
    void LoadFromJson()//load the levels from json file
    {
        int index = 1;

        List<LevelData> ld = new List<LevelData>();
        foreach (string  file in Directory.EnumerateFiles(Application.persistentDataPath))
        {
            print(index);
            if (File.Exists(storedPath+index+".json"))
            {
                ld.Add(JsonUtility.FromJson<LevelData>(File.ReadAllText(storedPath + index + ".json")));
                print("loading from: " + storedPath + index + ".json");
                index++;
            }
            else
            {

                print("Load Failed");
                return;
            }
        }
        lg.SetLevelData(ld);
       

        
    }
    
    public string SaveToString(object o)
    {
        return JsonUtility.ToJson(o,true);
    }
    
}

