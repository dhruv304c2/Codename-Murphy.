using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameScore : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI Text1;
    public TextMeshProUGUI Text2;

    private void Start()
    {
        score = 0;
        string disp = "Score: " + score.ToString();
        Text1.text = disp;
        Text2.text = disp;
    }

    public void CountUp()
    {
        score++;
        string disp = "Score: " + score.ToString();
        Text1.text = disp;
        Text2.text = disp; 
    }
}
