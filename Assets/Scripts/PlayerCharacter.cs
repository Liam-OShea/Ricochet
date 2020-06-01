// References
// https://www.youtube.com/watch?v=ndYd4S7UkAU - GamesPlusJames Respawning
// https://www.youtube.com/watch?v=vwUahWrY9Jg - GamesPlusJames Particle Effects


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour
{

    public int currentHP;
    public int maximumHP;
    public int lives;
    private int collectibles = 0;
    public int numCollectiblesForReward;
    public float spawnDelay;
    private bool died = false;

    public GameObject currentCheckpoint;
    public PlayerController player;
    public GameObject deathParticles;
    public GameObject respawnParticles;
    public GameObject ricochetGun;
    private Time lastCollision;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        currentHP = maximumHP;
        
    }

    public void DoDamage(int damage){
        currentHP -= damage;
        if(currentHP <= 0){
            if (!died)
            {
                Respawn();
                died = true;
            } 
        }
    }

    public void Respawn(){
        lives--;

        if (lives == 0)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        else
        {
            StartCoroutine("RespawnPlayerCoroutine");
        }
    }

    public IEnumerator RespawnPlayerCoroutine(){
        // Play death particle animation
        Instantiate(deathParticles, player.transform.position, player.transform.rotation);
        
        // Disable player and gun for amount of time and respawn at checkpoint
        player.enabled = false;
        player.GetComponent<Renderer>().enabled = false;
        ricochetGun.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(spawnDelay);

        currentHP = maximumHP;
        player.transform.position = currentCheckpoint.transform.position;
        player.enabled = true;
        ricochetGun.GetComponent<Renderer>().enabled = true;
        player.GetComponent<Renderer>().enabled = true;
        died = false;

        // Play respawn particle animation, reset health
        Instantiate(respawnParticles, currentCheckpoint.transform.position, currentCheckpoint.transform.rotation);
    }

    public int getHealth()
    {
        return currentHP;
    }

    public int getLives()
    {
        return lives;
    }

    public int getCollectibles()
    {
        return collectibles;
    }

    public void incrementCollectibles(){
        collectibles++;
        // Award player a life for collecting some number of golden ants
        if(collectibles == numCollectiblesForReward){
            collectibles = 0;
            lives++;
        }
    }

}
