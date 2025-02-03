using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseText : MonoBehaviour
{
    //fields
    public TextMeshProUGUI self;

    // Start is called before the first frame update
    void Start()
    {
        self.text = "";
    }

    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator countDown()
    {
        self.color = Color.red;
        self.text = "3";
        yield return new WaitForSecondsRealtime(0.75f);
        self.text = "";
        yield return new WaitForSecondsRealtime(0.25f);

        self.color = Color.yellow;
        self.text = "2";
        yield return new WaitForSecondsRealtime(0.75f);
        self.text = "";
        yield return new WaitForSecondsRealtime(0.25f);

        self.color = Color.green;
        self.text = "1";
        yield return new WaitForSecondsRealtime(0.75f);
        self.text = "";
        yield return new WaitForSecondsRealtime(0.25f);

        gameObject.SetActive(false);
    }
}
