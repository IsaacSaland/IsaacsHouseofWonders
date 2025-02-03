using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseUnpause : MonoBehaviour
{
    [SerializeField] private bool paused, unPausing = false;
    public TextMeshProUGUI pauseText;
    public List<GameObject> bullets;
    public GameObject powerUpSpawner;
    private CursorHandler cursor;
    private ManageMenus manage;

    // Start is called before the first frame update
    void Start()
    {
        manage = GameObject.FindWithTag("MenuManager").GetComponent<ManageMenus>();
        cursor = GameObject.FindWithTag("EventSystem").GetComponent<CursorHandler>();
        powerUpSpawner = GameObject.FindWithTag("SpawnManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !unPausing)
        {
            if (paused)
            {
                StartCoroutine(Resume());
            }
            else
            {
                PauseGame();
            }
        }

    }

    public void PauseGame()
    {
        cursor.setVisable(true);
        paused = true;
        manage.setPausePanelActive(true);

        foreach (GameObject bullet in bullets)
        {
            bullet.GetComponent<SpriteRenderer>().color = Color.clear;
        }
        foreach (GameObject powerUp in powerUpSpawner.GetComponent<SpawnPowerUps>().activePowerUps)
        {
            powerUp.GetComponent<SpriteRenderer>().color = Color.clear;
        }
        Time.timeScale = 0;
    }
    public IEnumerator Resume()
    {
        unPausing = true;
        cursor.setVisable(false);
        pauseText.gameObject.SetActive(true);
        manage.setPausePanelActive(false);

        StartCoroutine(pauseText.GetComponent<PauseText>().countDown());
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1;
        paused = false;
        foreach (GameObject bullet in bullets)
        {
            bullet.GetComponent<SpriteRenderer>().color = Color.white;
        }
        foreach (GameObject powerUp in powerUpSpawner.GetComponent<SpawnPowerUps>().activePowerUps)
        {
            powerUp.GetComponent<SpriteRenderer>().color = Color.white;
        }
        unPausing = false;
    }
    public void ResumeGame()
    {
        StartCoroutine(Resume());
    }
}
