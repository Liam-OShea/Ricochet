// References
// https://www.youtube.com/watch?v=zit45k6CUMk - Dani - Parallax Background

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax2 : MonoBehaviour
{

    private float length, startpos;
    public GameObject cam;
    public float effectStrength;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        float temp = (cam.transform.position.x * (1 - effectStrength));
        float dist = (cam.transform.position.x * effectStrength);
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if(temp > startpos + length) startpos += length;
        else if(temp < startpos - length) startpos -= length;
    }
}
