using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMechanism : MonoBehaviour
{
    public bool activated = false;
    public GameObject[] doors = new GameObject[10];
    public FloatingPlatform[] platforms = new FloatingPlatform[4];
    private int switchTimer = 0;
    private bool counting = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Player" || collision.tag == "Shot") && !counting)
        {
            Debug.Log("Switch hit");
            Debug.Log("activated status: " + activated);
            activated = !activated;
            toggleDoors();
            togglePlatforms();
            counting = true;
            switchTimer = 10;
            toggleColour();
            if (collision.tag == "Shot")
            {
                Destroy(collision.gameObject);
            }
        }
    }

    void toggleColour()
    {
        if (activated) {
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
        }
        else{
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color (255, 255, 255);
        }
    }

    void toggleDoors()
    {
        foreach (GameObject g in doors) {
            if (!nullObject(g))
            {
                g.GetComponent<BoxCollider2D>().enabled = !activated;
                Debug.Log("Box collider status: " + g.GetComponent<BoxCollider2D>().enabled);
                foreach (SpriteRenderer sr in g.GetComponentsInChildren<SpriteRenderer>())
                {
                    sr.enabled = !activated;
                }
               
                
            }
        }

  
    }

    void togglePlatforms()
    {
        foreach (FloatingPlatform f in platforms)
        {
            if (!nullObject(f.gameObject))
            {
                f.move = !f.move;
            }
        }
    }

    bool nullObject(GameObject g)
    {
        return g == null;
    }

    private void FixedUpdate()
    {
        if (counting) { 
            switchTimer -= 1;
            if (switchTimer == 0)
            {
                counting = false;
            }
        }
    }
}
