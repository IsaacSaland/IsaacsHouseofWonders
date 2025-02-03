using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerUps : MonoBehaviour
{
    //fields
    public GameObject slowMoPrefab, lifeUpPrefab, invincibilityPrefab, superDashPrefab;
    public List<GameObject> activePowerUps;
    public float sloMoTimer, lifeUpTimer, invTimer, superDashTimer;
    public int xTop, xBot, yTop, yBot;

    void Awake()
    {
        sloMoTimer = 30f;
        lifeUpTimer = 20f;
        invTimer = 15f;
        superDashTimer = 45f;

        yTop = 5;
        yBot = -4;
        xTop = 11;
        xBot = -11;

        if (GameObject.FindWithTag("Player").GetComponent<Stats>().hardMode)
        {
            xBot++;
            xTop--;

            sloMoTimer += 10;
            lifeUpTimer += 10;
            invTimer += 5;
            superDashTimer += 5;
        }

        activePowerUps = new List<GameObject>();
        InvokeRepeating("spawnSloMo", sloMoTimer, sloMoTimer);
        InvokeRepeating("spawnLifeUp", lifeUpTimer, lifeUpTimer);
        InvokeRepeating("spawnInvincibility", invTimer, invTimer);
        InvokeRepeating("spawnSuperDash", superDashTimer, superDashTimer);
    }

    void Update()
    {
    }

    //does not allow to spawn on edges
    private void spawnPowerUp(GameObject powerUp, string type)
    {
        //determining the position (not allowing overlaps)
        Vector2 rand = new Vector2(0, 0);
        for (int i = 0; i < 1; i++)
        {
            rand = new Vector2(Random.Range(xBot + 1, xTop - 1), Random.Range(yBot + 1, yTop - 1));
            foreach (GameObject pu in activePowerUps)
            {
                if (pu.GetComponent<Transform>().position.x == rand.x && pu.GetComponent<Transform>().position.y == rand.y)
                {
                    i--;
                }
            }
        }

        switch (type)
        {
            case "slowMo":
                powerUp = Instantiate(slowMoPrefab, rand, Quaternion.identity);
                break;
            case "lifeUp":
                powerUp = Instantiate(lifeUpPrefab, rand, Quaternion.identity);
                break;
            case "inv":
                powerUp = Instantiate(invincibilityPrefab, rand, Quaternion.identity);
                break;
            default:
                Debug.Log("you messed up");
                break;
        }
        activePowerUps.Add(powerUp);
    }
    private void spawnPowerUpEdge(GameObject powerUp, string type)
    {
        //determining the position (not allowing overlaps)
        Vector2 rand = new Vector2(0, 0);
        for (int i = 0; i < 1; i++)
        {
            int tempY = -4;
            if (Random.value > 0.5)
            {
                tempY = 5;
            }
            rand = new Vector2(Random.Range(xBot, xTop), tempY);
            foreach (GameObject pu in activePowerUps)
            {
                if (pu.GetComponent<Transform>().position.x == rand.x && pu.GetComponent<Transform>().position.y == rand.y)
                {
                    i--;
                }
            }
        }

        switch (type)
        {
            case "SDash":
                powerUp = Instantiate(superDashPrefab, rand, Quaternion.identity);
                break;
            default:
                Debug.Log("uh oh");
                break;
        }
        activePowerUps.Add(powerUp);
    }

    private void spawnSloMo()
    {
        if (Random.value < 0.5) //50% chance to spawn
        {
            spawnPowerUp(new GameObject(), "slowMo");
        }
    }
    private void spawnLifeUp()
    {
        if (Random.value < 0.9) //90% chance to spawn
        {
            spawnPowerUp(new GameObject(), "lifeUp");
        }
    }
    private void spawnInvincibility()
    {
        if (Random.value < 0.75) //75% chance to sapwn
        {
            spawnPowerUp(new GameObject(), "inv");
        }
    }

    private void spawnSuperDash()
    {
        if (Random.value < 33) //33% chance to spawn
        {
            spawnPowerUpEdge(new GameObject(), "SDash");
        }
    }
}
