using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicochetProjectile : MonoBehaviour
{

    private Vector2 target, origin;
    private CircleCollider2D CCollider;
    public float speed = 6.0f;
    public float maxLife = 100f;

    private bool entered = false;

    // Start is called before the first frame update
    void Start()
    {
        CCollider = GetComponent<CircleCollider2D>();
        CCollider.enabled = false;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        Debug.DrawRay(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);

        if (hit.collider != null)
        {
            target = hit.point;
        }
        else
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition) ;
        }

        CCollider.enabled = true;
        
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        maxLife -= Time.deltaTime;

        if(maxLife <= 0)
        {
            Destroy(gameObject);
        }

        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target, step);//new Vector3(target.x, target.y, 0).normalized * step;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (!entered)
        {
            entered = true;
            CCollider.enabled = false;

            float step = speed * Time.deltaTime;

            Debug.Log("DIGG");

            RaycastHit2D originHit = Physics2D.Raycast(origin, target - origin);
            Debug.DrawRay(origin, target - origin, Color.blue);

            Vector2 reflection = Vector2.Reflect(target - origin, originHit.normal);

            RaycastHit2D hit = Physics2D.Raycast(target, reflection);
            Debug.DrawRay(target, reflection, Color.black);

            origin = originHit.point;

            CCollider.enabled = true;

            if (hit.collider != null)
            {
                Debug.Log(target.ToString() + " " + hit.point.ToString());
                target = hit.point;
            }
            else
            {
                Debug.Log("No Collider to hit found");
                target = reflection;
            }
        }

        if(collision.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        entered = false;
    }
}
