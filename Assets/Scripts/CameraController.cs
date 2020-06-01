/*
Author: Liam O'Shea
File: CameraController.cs
Description: A very simple camera controller which tracks the player.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
            transform.position = (new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z));
    }
}
