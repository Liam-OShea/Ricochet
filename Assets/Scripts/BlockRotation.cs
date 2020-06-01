/*
Author: Liam O'Shea
File: BlockRotation.cs
Description: Applying this script to an object allows the player to rotate that object by
mousing over it and pressing "R"
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRotation : MonoBehaviour
{   
    public float rotationFactor;
    // Start is called before the first frame updatea
   
    void OnMouseOver() { 
        if(Input.GetKey(KeyCode.R))
        {
            gameObject.transform.Rotate(Vector3.forward * (360/rotationFactor));
        }
     }
}
