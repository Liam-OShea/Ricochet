using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRockGenerator : MonoBehaviour
{

    public GameObject FallingRock;
    public float start = -50;
    public float end = 150;
    int rate = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rate == 0)
        {
            Instantiate(FallingRock, new Vector3(Random.Range(start, end), transform.position.y, transform.position.z), new Quaternion());
            rate = 10;
        }
        else
        {
            rate--;
        }
    }
}
