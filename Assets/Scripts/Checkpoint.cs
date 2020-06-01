/*
Author: Liam O'Shea
File: Checkpoint.cs
Description: Sets character's checkpoint to applied object when player enters checkpoint collider.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public PlayerCharacter playerCharacterManager;
    // Start is called before the first frame update
    void Start()
    {
        playerCharacterManager = FindObjectOfType<PlayerCharacter>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
            playerCharacterManager.currentCheckpoint = gameObject;
        }
    }
}
