// References
// https://www.youtube.com/watch?v=bY4Hr2x05p8&t=75s - Blackthornprod - 2D Ranged Combat System

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public AudioSource fireWeaponSFX;
    public float regOffset;
    public float flipOffset;
    public Transform firePoint;
    public GameObject projectile;
    public int projectileSpeed;

    void Update()
    {
        bool flipped = FindObjectOfType<PlayerController>().IsFlipped();
        float rotation;
        
        Vector3 delta = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if(flipped)
        {
            rotation = Mathf.Atan2(delta.x, -delta.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotation + flipOffset);  
        }
        else
        {
            rotation = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotation + regOffset);  
        }

        if(Input.GetMouseButtonDown(0)){
            Shoot();

        }
         
    }
    
    void Shoot(){
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position;
        // Vector2 origin = new Vector2(rayStart.position.x, rayStart.position.y);
        // Vector2 target = new Vector2(player.position.x, player.position.y);
        // //Params for Raycast: Origin, direction, distance, layer mask
        // RaycastHit2D hit = Physics2D.Raycast(origin, target-origin, 100, toHit);
        // Debug.DrawLine(rayStart.position, player.position);
        GameObject projectileClone;
        projectileClone = Instantiate(projectile, firePoint.position, firePoint.rotation) as GameObject;
        projectileClone.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;

        fireWeaponSFX.Play();
    }
}
