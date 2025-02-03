using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManageMenus : MonoBehaviour
{
    public GameObject PausePanel;
    // Start is called before the first frame update
    void Start()
    {
        setPausePanelActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setPausePanelActive(bool active)
    {
        PausePanel.SetActive(active);
    }

    public void backToHome()
    {
        SceneManager.LoadScene("Menus");
    }
    public void closeGame()
    {
        Application.Quit();
    }
}
