using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public int totalScore;
    public int multiplier;
    [SerializeField] private Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        totalScore = 0; //where to set this 
    }

    // Update is called once per frame
    void Update()
    {
        // if trick update score 
        // if shell break update multipler 
    }
}
