// References
// https://www.youtube.com/watch?v=_ICCSDmLCX4 - Indie Nugget - 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextManager : MonoBehaviour
{
    public float destroyTime = 3.0f;
    public Vector3 offset = new Vector3(0,2,0);
    public Vector3 posNoise = new Vector3(1, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);
        transform.localPosition += offset;
        transform.localPosition += new Vector3(Random.Range(-posNoise.x, posNoise.x), 0, 0);
    }

}
