using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    //Fields
    public bool isInvincible;
    public int health;
    public float invTime;
    [SerializeField] private int score;
    public bool hardMode;

    //Awake + Update
    void Awake()
    {
        invTime = 1f;
        isInvincible = false;
        score = 0;
        health = 3;
    }

    void Update()
    {
        //just a button to toggle invincibility for testing
        if (Input.GetKeyDown(KeyCode.L))
        {
            toggleInvincibility();
        }
        //if the player has no health left it dies
        if (health <= 0)
        {
            die();
        }
    }

    //Getters + setters
    public void setInvincibility(bool isInvincible)
    {
        this.isInvincible = isInvincible;
    }
    public int getScore()
    {
        return score;
    }
    public void setScore(int score)
    {
        this.score = score;
    }

    //Methods
    public void toggleInvincibility()
    {
        isInvincible = !isInvincible;
    }
    private void die()
    {
        Time.timeScale = 0;
        Destroy(gameObject);
    }

    private void takeDamage()
    {
        if (!isInvincible)
        {
            health--;
            StartCoroutine(tempInvincibility(invTime));
            StartCoroutine(gameObject.GetComponent<PlayerSpriteController>().playerHurt());
        }
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Bullet")
        {
            takeDamage();
        }
    }
    public IEnumerator tempInvincibility(float seconds)
    {
        setInvincibility(true);
        yield return new WaitForSeconds(seconds);
        setInvincibility(false);
    }
}
