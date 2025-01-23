using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI displayedScore; 
   

    // Start is called before the first frame update
    void Start()
    {
        displayedScore.text = GameManager.Instance.score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        displayedScore.text = GameManager.Instance.score.ToString();
        // if 
        // if trick update score 
        // if shell break update multipler 
    }
}
