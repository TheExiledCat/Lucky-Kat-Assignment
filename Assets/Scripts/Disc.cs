using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Disc 
{
    public float startRotation;
    public int amountOfSplits;
    public float gap;
    public Disc(float _rot,int _splits,float _gap = ConstantValues.GAP)
    {
        startRotation = _rot;
        amountOfSplits = _splits;
        gap = _gap;
    }
}
