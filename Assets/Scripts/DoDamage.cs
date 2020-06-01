/*
Author: Liam O'Shea
File: DoDamage.cs
Description: This script is applied to projectiles/hazards and does damage to player if they collide.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DoDamage : MonoBehaviour
{
    
    public int damage = 1;
    public bool doingDamage = false;

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<PlayerCharacter>().DoDamage(damage);
            doingDamage = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        doingDamage = false;
    }
}
