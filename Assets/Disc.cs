using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disc : MonoBehaviour
{
    float startRotation;
    int amountOfSplits;
    float gap;
    public Disc(float _rot,int _splits,float _gap = ConstantValues.GAP)
    {
        startRotation = _rot;
        amountOfSplits = _splits;
        gap = _gap;
    }
}
