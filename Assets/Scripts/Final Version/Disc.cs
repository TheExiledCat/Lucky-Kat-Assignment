using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Disc 
{
    public float startRotation;
    public int amountOfSplits;
    public float gap;
    public int amountOfKillZones;
    public Disc(float _rot,int _splits,float _gap = ConstantValues.GAP)
    {
        startRotation = _rot;
        amountOfSplits = _splits;
        gap = _gap;
    }
    public Disc(float _rot, int _splits, int _zones,float _gap = ConstantValues.GAP)
    {
        startRotation = _rot;
        amountOfSplits = _splits;
        gap = _gap;
        amountOfKillZones = _zones;
    }
}
