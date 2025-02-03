using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodyMovement : MonoBehaviour
{
    //Fields
    [SerializeField] private Transform player;
    [SerializeField] private float duration = 20f;
    private bool isMoving;
    private bool isDashing;
    public float dashCooldown; //in seconds
    [SerializeField] public int dashLength; //in tiles
    public int yTop, yBot, xTop, xBot;
    public bool invPowerUpActive;
    public Stats stats;

    void Awake()
    {
        dashLength = 2;
        yTop = 5;
        yBot = -4;
        xTop = 11;
        xBot = -11;

        if (stats.hardMode)
        {
            xTop--;
            xBot++;
        }

        dashCooldown = 2f;
        isMoving = false;
        isDashing = false;
    }

    // Update is called once per frame
    void Update()
    {
        //moving
        if (Input.GetKeyDown(KeyCode.W) && !isMoving && transform.position.y < yTop)
        {
            StartCoroutine(move(player.up));
        }
        else if (Input.GetKeyDown(KeyCode.S) && !isMoving && transform.position.y > yBot)
        {
            StartCoroutine(move(-player.up));
        }
        else if (Input.GetKeyDown(KeyCode.A) && !isMoving && transform.position.x > xBot)
        {
            StartCoroutine(move(-player.right));
        }
        else if (Input.GetKeyDown(KeyCode.D) && !isMoving && transform.position.x < xTop)
        {
            StartCoroutine(move(player.right));
        }

        //dashing
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isMoving && !isDashing && transform.position.y < yTop - (dashLength - 1))
        {
            StartCoroutine(dash(player.up));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !isMoving && !isDashing && transform.position.y > yBot + (dashLength - 1))
        {
            StartCoroutine(dash(-player.up));
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && !isMoving && !isDashing && transform.position.x > xBot + (dashLength - 1))
        {
            StartCoroutine(dash(-player.right));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !isMoving && !isDashing && transform.position.x < xTop - (dashLength - 1))
        {
            StartCoroutine(dash(player.right));
        }
    }

    IEnumerator move(Vector3 dir)
    {
        isMoving = true;
        float timePassed = 0f;
        Vector3 target = player.position + dir;
        while (timePassed < duration)
        {
            player.position = Vector3.Lerp(player.position, target, duration * Time.deltaTime);
            timePassed += Time.deltaTime * duration * 20;
            yield return null;
        }
        isMoving = false;
        player.position = target;
    }

    IEnumerator dash(Vector3 dir)
    {
        GetComponent<Stats>().setInvincibility(true);
        isDashing = true;

        Vector3 target = player.position + (dashLength * dir);
        player.position = target;
        yield return new WaitForSeconds(0.5f);

        //so it doesn't cancel out the invincibility powerup
        if (!invPowerUpActive)
        {
            GetComponent<Stats>().setInvincibility(false);
        }
        yield return new WaitForSeconds(dashCooldown);
        isDashing = false;
    }
}