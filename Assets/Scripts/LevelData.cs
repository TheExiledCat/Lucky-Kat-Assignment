using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class LevelData //hold the data for a all the discs in a level
{
    public List<Disc> discArray;
    public Color color;
    public LevelData(List<Disc> _newDiscs,Color _c)
    {
        SetDiscs(_newDiscs);
        SetColor(_c);
    }
    #region discs   
    public List<Disc> GetDiscs()
    {
        return discArray;
    }
    public void SetDiscs(List<Disc> _newDiscs)
    {
        discArray = _newDiscs;
    }
    public void AddDiscs(List<Disc> _newDiscs)
    {
        discArray.AddRange(_newDiscs);
        
    }
    public void AddDisc(Disc _newDisc)
    {
        discArray.Add(_newDisc);

    }
    #endregion
    #region color
    public Color GetColor()
    {
        return color;
    }
    public void SetColor(Color _c)
    {
        color = _c;
    }
    #endregion
}
