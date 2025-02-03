using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMo : MonoBehaviour
{
    //Fields
    public GameObject[] turrets;
    private bool didEffect;
    [SerializeField] public GameObject spawner;
    private IEnumerator coroutine;

    void Awake()
    {
        didEffect = false;
        turrets = GameObject.FindGameObjectsWithTag("Turret");
        spawner = GameObject.FindGameObjectsWithTag("SpawnManager")[0];
        coroutine = despawnTimer();
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !didEffect)
        {
            StopCoroutine(coroutine);
            effect();
        }
    }
    //the actual powerup
    private void effect()
    {
        didEffect = true;
        GetComponent<SpriteRenderer>().color = Color.red;

        //turning on the powerup
        foreach (GameObject turret in turrets)
        {
            turret.GetComponent<TurretStuff>().setSloMo(true);
        }
        StartCoroutine(reverseEffect());
        StartCoroutine(flash());
    }
    private IEnumerator reverseEffect()
    {
        yield return new WaitForSeconds(10.0f);
        //turning off the powerup
        foreach (GameObject turret in turrets)
        {
            turret.GetComponent<TurretStuff>().setSloMo(false);
        }
        destroySelf();
    }

    private IEnumerator flash()
    {
        yield return new WaitForSeconds(8);
        float timer = 0f;
        while (timer <= 2)
        {
            yield return new WaitForSeconds(0.2f);
            GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(0.2f);
            GetComponent<SpriteRenderer>().color = Color.clear;

            timer += 0.4f;
        }
    }

    private IEnumerator despawnTimer()
    {
        yield return new WaitForSeconds(10);
        destroySelf();
    }

    /**
     * removes itself from the activePowerUps List and destroys its object
     */
    private void destroySelf()
    {
        spawner.GetComponent<SpawnPowerUps>().activePowerUps.Remove(gameObject);
        Destroy(gameObject);
    }
}
