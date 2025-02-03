using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUp : MonoBehaviour
{
    //Fields
    public GameObject player, spawner;
    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.FindGameObjectsWithTag("SpawnManager")[0];
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        coroutine = despawnTimer();
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            StopCoroutine(coroutine);
            effect();
        }
    }

    private void effect()
    {
        player.GetComponent<Stats>().health++;
        player.GetComponent<Stats>().tempInvincibility(0.5f);
        StartCoroutine(player.GetComponent<PlayerSpriteController>().extraHealth());
        GetComponent<SpriteRenderer>().color = Color.clear; //setting the color to clear to make it seem as if it was destroyed
        Invoke("destroySelf", 0.6f); //actually destroying the object
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
