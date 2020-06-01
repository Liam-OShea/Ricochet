/*
Author: Liam O'Shea
File: ShermanWeaponDamage.cs
Description: This script is applied to Sherman's projectiles and controls the damage they deal.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShermanWeaponDamage : MonoBehaviour
{
    
    public int damage = 1;
    

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemies"){
            other.GetComponent<EnemyHealthManager>().DoDamage(damage);
        } 
    }
}
