using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStuff : MonoBehaviour
{
    private float time = 0f;
    private bool die;
    [SerializeField] private float bulletLife = 5f;
    public GameObject pauseHandler;

    // Start is called before the first frame update
    void Start()
    {
        pauseHandler = GameObject.FindGameObjectsWithTag("PauseHandler")[0];
        pauseHandler.GetComponent<PauseUnpause>().bullets.Add(gameObject);
        die = false;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= bulletLife || die)
        {
            destroySelf();
        }
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.tag != "Bullet")
        {
            die = true;
        }
    }
    private void destroySelf()
    {
        pauseHandler.GetComponent<PauseUnpause>().bullets.Remove(gameObject);
        Destroy(gameObject);
    }
}
