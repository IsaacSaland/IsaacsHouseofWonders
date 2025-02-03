using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    //fields
    public TextMeshProUGUI scoreCounter;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            scoreCounter.text = "Score: " + player.GetComponent<Stats>().getScore();
        }
    }
}
