// References
// https://www.youtube.com/watch?v=vwUahWrY9Jg - GamesPlusJames Particle Effects

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCleanup : MonoBehaviour
{

    private ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!particle.isPlaying){
            Destroy(gameObject);
        }

    }
}
