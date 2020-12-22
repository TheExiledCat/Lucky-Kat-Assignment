using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{

    [SerializeField] TMP_Text score;
    [SerializeField] TMP_Text level;
    // Start is called before the first frame update
    void Start()
    {
        GameScript.GS.OnScore += SetScore;
        LevelGenerator.LG.OnLevelUp += SetLevel;
        GameScript.GS.OnDeath += ResetScore;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameScript.GS.CheckCombo())
        {
            score.color = Color.black;
        }
        else
        {
            score.color = Color.white;
        }
    }

    void SetLevel(int _level)
    {
        level.text ="Level "+ _level.ToString();

    }
    void ResetScore()
    {
        score.text = 0.ToString();
    }
    void SetScore(int _score)
    {
        score.text = _score.ToString();
    }
}
