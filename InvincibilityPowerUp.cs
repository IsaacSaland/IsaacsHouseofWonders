using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityPowerUp : MonoBehaviour
{
    //fields
    private IEnumerator coroutine;
    [SerializeField] private GameObject spawner;
    [SerializeField] GameObject player;
    private float invTime;

    // Start is called before the first frame update
    void Start()
    {
        invTime = 5f;
        spawner = GameObject.FindWithTag("SpawnManager");
        player = GameObject.FindWithTag("Player");
        coroutine = despawnTimer();
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StopCoroutine(coroutine);
            StartCoroutine(effect());
        }
    }

    private IEnumerator effect()
    {
        player.GetComponent<CodyMovement>().invPowerUpActive = true;
        StartCoroutine(player.GetComponent<Stats>().tempInvincibility(invTime));
        player.GetComponent<SpriteRenderer>().color = Color.yellow; //making player yellow for duration of invincibility
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(invTime - 2);

        //flashing like crazy for the last 2 seconds of the powerup time
        StartCoroutine(flash());
        yield return new WaitForSeconds(2);

        GetComponent<SpriteRenderer>().color = Color.clear; //making object clear to seem like it was destroyed
        player.GetComponent<SpriteRenderer>().color = Color.white; //changing player color back to normal
        player.GetComponent<CodyMovement>().invPowerUpActive = false;
        Invoke("destroySelf", 0.1f); //actually destroying the object
    }
    private IEnumerator flash()
    {
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
