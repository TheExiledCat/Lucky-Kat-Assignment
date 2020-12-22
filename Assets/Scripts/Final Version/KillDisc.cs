using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class KillDisc : Disc
{
    public int killZones;
    public KillDisc(float _rot,int _splits,int _killZones = 1):base(_rot,_splits)
    {
        startRotation = _rot;
        amountOfSplits = _splits;
        killZones = _killZones;
    }
   
}
