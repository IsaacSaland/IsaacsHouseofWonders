using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LifeText : MonoBehaviour
{
    //fields
    public TextMeshProUGUI lifeCounter;
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
            lifeCounter.text = "Lives: " + player.GetComponent<Stats>().health;
        }
    }
}
