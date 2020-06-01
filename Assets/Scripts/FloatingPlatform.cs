using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{

    public float left = 0;
    public float right = 4;
    public float moveSpeed = 1;

    private float originX = 0;

    public bool move = true;
    private bool mRight = true;
    private Transform transform;

    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        originX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            if (transform.position.x-originX <= left)
            {
                mRight = true;
            }
            if (transform.position.x-originX >= right)
            {
                mRight = false;
            }

            if (mRight)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        }
        
    }

    bool getMove()
    {
        return move;
    }

}
