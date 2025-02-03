using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretStuff : MonoBehaviour
{
    //Fields
    [SerializeField] private Transform firePoint;
    public GameObject prefab;
    public float waitTime; //in seconds
    public float vMult;
    public GameObject player;
    private Stats stats;
    [SerializeField] protected bool sloMoActive;

    // Start is called before the first frame update
    void Awake()
    {
        //making the turret red
        GetComponent<SpriteRenderer>().color = Color.red;

        waitTime = 2;
        vMult = 6;
        stats = player.GetComponent<Stats>();
        sloMoActive = false;

        Invoke("fire", 3);
        InvokeRepeating("speedUp", 30, 5);
    }

    // Update is called once per frame
    void Update()
    {
    }

    //getters + setters
    public void setWaitTime(float newTime)
    {
        waitTime = newTime;
    }
    public void setSloMo(bool slowMo)
    {
        sloMoActive = slowMo;
    }

    private void fire()
    {

        //making the fire rate in between bullets
        setWaitTime(Random.Range(1.0f, 4.0f));
        float velo = vMult;

        if (Random.Range(0, 4) == 1) //making it a 25% chance to fire a bullet
        {
            StartCoroutine(turretWarning(velo));

            if (player != null)
            {
                //incrementing the score by 1
                stats.setScore(stats.getScore() + 1);
            }
            else
            {
                return;
            }
        }
        Invoke("fire", waitTime);
    }
    private void speedUp()
    {
        vMult += 0.2f;
    }

    //makes the turret change to orange for 0.25 seconds
    private IEnumerator turretWarning(float velo)
    {
        //changing the color
        yield return new WaitForSeconds(waitTime - 0.25f);
        GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.35f, 0f, 1.0f);
        yield return new WaitForSeconds(0.35f);
        GetComponent<SpriteRenderer>().color = Color.red;

        if (sloMoActive)
        {
            waitTime += 2.0f;
            velo -= 3;
        }
        //firing the bullet
        GameObject firedBullet = Instantiate(prefab, firePoint.position, firePoint.rotation);
        firedBullet.GetComponent<Rigidbody2D>().velocity = firePoint.up * velo;
    }
}