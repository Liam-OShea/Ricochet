using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public GameObject particles;
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            Destroy(gameObject);
            Instantiate(particles, gameObject.transform.position, gameObject.transform.rotation);
            FindObjectOfType<PlayerCharacter>().incrementCollectibles();
        }
    }
}
