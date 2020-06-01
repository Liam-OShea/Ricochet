/*
Author: Liam O'Shea
File: RicochetProjectileController.cs
Description: This script moderates the behaviour of Ricochet Projectiles. (Counting bounces, etc)
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicochetProjectileController : MonoBehaviour
{
    public AudioSource ricochetSFX;
    public AudioSource destroySFX;
    public GameObject FloatingTextPrefab;
    public int maxBounces;
    public int baseDamage = 1;
    private int timesBounced;

    private HUDManager HUD;
    private PlayerCharacter player;
    private GameObject playerObject;
    private float lastCollision;
    public GameObject impactParticles;
    // Start is called before the first frame update
    void Start()
    {
        timesBounced = 0;
        playerObject = GameObject.FindWithTag("Player");
        HUD = FindObjectOfType<HUDManager>();
        player = FindObjectOfType<PlayerCharacter>();
        HUD.setMostRecentRicochetProjectile(gameObject);

        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), playerObject.GetComponent<Collider2D>(), true);
    }

    void OnTriggerEnter2D(Collider2D other){

        // This conditional checks if another collision has happened in the same frame.
        // Only the first collision of the frame will be applied
        // Can happen when striking the position where two tiles meet. (2 colliders)
        if(lastCollision != Time.time){
            

            if(other.gameObject.tag == "Enemies" || other.gameObject.tag == "No Bounce"){
                if(other.gameObject.tag == "Enemies" && timesBounced > 0){
                    other.GetComponent<EnemyHealthManager>().DoDamage(baseDamage * timesBounced);

                    // Show floating text to indicate damage here
                    if(FloatingTextPrefab) ShowFloatingText();
                } 
                timesBounced = 0;
                destroySFX.Play();
                Destroy(gameObject);
                Instantiate(impactParticles, gameObject.transform.position, gameObject.transform.rotation);
            }

            timesBounced ++;
            ricochetSFX.Play();
        }


        
        
            if(timesBounced > maxBounces){
                destroySFX.Play();
                Destroy(gameObject);
                Instantiate(impactParticles, gameObject.transform.position, gameObject.transform.rotation);
            }
            
        lastCollision = Time.time;
    }

    // Trying to keep projectiles from colliding with Sherman. Does not currently work.
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Player"){
            Debug.Log("Player hit");
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider2D>(), gameObject.GetComponent<CircleCollider2D>(), true);
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<CircleCollider2D>(), gameObject.GetComponent<CircleCollider2D>(), true);
        }
    }

    void ShowFloatingText(){
        var go = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity);
        go.transform.SetAsLastSibling ();
        go.GetComponent<TextMesh>().text = (baseDamage * timesBounced).ToString();
    }

    public int getNumberOfBounces(){
        return timesBounced;
    }
}
