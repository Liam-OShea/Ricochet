/*
Author: Liam O'Shea
File: TurretProjectileController.cs
Description: This script manages behaviour of turret projectiles.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectileController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag != "Enemies"){
            Destroy(gameObject);
        }
    }
}
