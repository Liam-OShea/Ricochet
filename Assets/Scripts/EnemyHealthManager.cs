/*
Author: Liam O'Shea
File: EnemyHealthManager.cs
Description: Manages enemy health. Activates particle effects.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public AudioSource dieSFX;
    public int currentHP;
    public int maxHP;
    public GameObject deathParticles;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
    }

    public void DoDamage(int damage){
        currentHP -= damage;
        if(currentHP <= 0){
            EnemyDeath();
        }
    }

    void EnemyDeath(){
        dieSFX.Play();
        Destroy(gameObject);
        Instantiate(deathParticles, gameObject.transform.position, gameObject.transform.rotation);
    }
}
