using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ricochet : MonoBehaviour
{
    public GameObject projectile;
    private LineRenderer line;

    public int maxBounces = 10;
    private int bounces = 0;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.startColor = Color.red;
        line.endColor = Color.red;
        line.startWidth = 0.1f;
        line.endWidth = line.startWidth;
    }

    void Update()
    {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 target, lastHit = transform.position;

        RaycastHit2D[] hits = new RaycastHit2D[maxBounces];
        hits[0] = Physics2D.Raycast(transform.position, mousePos - transform.position);

        line.SetPosition(0, transform.position);
        line.SetPosition(1, mousePos);

        Debug.DrawRay(transform.position, mousePos - transform.position);

        bounces = 0;

        for (int i = 1; i < maxBounces; i++)
        {
            if (hits[i - 1].collider != null && hits[i - 1].collider.tag != "Player")
            {
                bounces++;

                target = Vector2.Reflect(hits[i - 1].point - lastHit, hits[i - 1].normal);

                hits[i] = Physics2D.Raycast(hits[i - 1].point, target);
                Debug.DrawRay(hits[i - 1].point, target, Color.green);

                lastHit = hits[i - 1].point;
                line.SetPosition(bounces, lastHit);
            }
            else
            {
                for (int j = i + 1; j < maxBounces; j++)
                {
                    if (j == 1) j++;
                    line.SetPosition(j, lastHit);
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject sprites = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
        }
    }
}