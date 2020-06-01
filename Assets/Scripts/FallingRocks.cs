using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRocks : MonoBehaviour
{
    public float rSpeed;

    private Rigidbody2D rigidbody2D;
    private float angle = 360;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        angle -= Time.deltaTime * rSpeed;
        rigidbody2D.rotation = angle;

        if(transform.position.y < 0)
        {
            Destroy(gameObject);
        }
    }
}
