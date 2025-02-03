using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class superDash : MonoBehaviour
{
    //Fields
    [SerializeField] public GameObject spawner;
    private IEnumerator coroutine;
    public GameObject player;
    private bool didEffect;

    void Awake()
    {
        didEffect = false;
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
        if (collision.tag == "Player" && !didEffect)
        {
            didEffect = true;
            StopCoroutine(coroutine);
            StartCoroutine(effect());
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

    private IEnumerator effect()
    {
        player.GetComponent<CodyMovement>().dashCooldown /= 2f;
        player.GetComponent<CodyMovement>().dashLength++;
        GetComponent<SpriteRenderer>().color = Color.red; //colors
        yield return new WaitForSeconds(13);
        StartCoroutine(flash());
        yield return new WaitForSeconds(2);
        player.GetComponent<CodyMovement>().dashCooldown *= 2f;
        player.GetComponent<CodyMovement>().dashLength--;
        GetComponent<SpriteRenderer>().color = Color.clear; //colors
        Invoke("destroySelf", 0.1f);
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
}
