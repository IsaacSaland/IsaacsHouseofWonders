using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startNormal()
    {
        Debug.Log("test");
        SceneManager.LoadScene("NormalMode");
    }
    public void startHard()
    {
        SceneManager.LoadScene("HardMode");
    }

    public void quit()
    {
        Debug.Log("closing application");
        Application.Quit();
    }
}
