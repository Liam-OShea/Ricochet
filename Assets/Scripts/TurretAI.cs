﻿/*
Author: Liam O'Shea
File: TurretAI.cs
Description: This script implements the Turret's AI system.
*/

// References
// https://www.youtube.com/watch?v=cJj7-Sy03HQ - Paul Conway
// https://www.youtube.com/watch?v=KKgtC_Gy65c - Brackeys
// https://www.youtube.com/watch?v=bMh4UDYVyGU - Gucio Devs


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour
{
    public Transform rayStart, player;
    private bool playerSpotted = false;
    private bool obstruction = false;
    public float fireRate = 0;
    //public LayerMask toHit;
    private float timeToFire = 0;
    public GameObject projectile;
    public float projectileSpeed;

    void Update()
    {
        Raycasting();
        if(fireRate == 0)
        {
            if(playerSpotted && !obstruction){
                Shoot();   
            }
        }
        else
        {
            if(playerSpotted && !obstruction && Time.time > timeToFire){
                timeToFire = Time.time + 1/fireRate;
                Shoot();
            }
        }
    }

    void Shoot ()
    {
        Vector2 direction = player.transform.position - rayStart.position;
        GameObject projectileClone;
        projectileClone = Instantiate(projectile, rayStart.position, rayStart.rotation) as GameObject;
        projectileClone.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
    }

    void Raycasting()
    {
        //Debug.DrawLine(rayStart.position, player.position, Color.green);
        playerSpotted = Physics2D.Linecast(rayStart.position, player.position, 1 << LayerMask.NameToLayer("Player"));
        // Can create layer with all possible obstructions
        obstruction = Physics2D.Linecast(rayStart.position, player.position, 1 << LayerMask.NameToLayer("Ground"));
    }
}
