using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator playerHurt()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(gameObject.GetComponent<Stats>().invTime);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    public IEnumerator extraHealth()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
