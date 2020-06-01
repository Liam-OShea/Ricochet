// References
// https://www.youtube.com/watch?v=5E5_Fquw7BM - Brackeys - Parallax Scrolling

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    
    public Transform[] backgrounds;     // Contains backgrounds to be parallaxed
    private float[] parallaxScales;     // Proportion of cam movement to move background by
    public float smoothing = 1f;        // Smooth factor for parallax effect. Set this above 0.
    private Transform cam;              // Reference to main cameras transform
    private Vector3 previousCamPosition;// Store position of camera in prev frame
    
    // Awake is called before start.
    void Awake(){
        cam = Camera.main.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Prev frame has curr frame's cam position
        previousCamPosition = cam.position;
        parallaxScales  = new float[backgrounds.Length];
        for(int i = 0; i > backgrounds.Length; i++){
            parallaxScales[i] = backgrounds[i].position.z*-1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        for(int i = 0; i < backgrounds.Length; i++){
            // Parallax is opposite of camera movement
            float parallax = (previousCamPosition.x - cam.position.x) * parallaxScales[i];

            // Set target x pos
            float bgTargetPosX = backgrounds[i].position.x + parallax;

            Vector3 bgTargetPos = new Vector3(bgTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            // Fade between current position and target position
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, bgTargetPos, smoothing * Time.deltaTime);
            Debug.Log(bgTargetPos.x);
        }
        // Set prev cam position to our current pos for next frame
        previousCamPosition = cam.position;
    }
}
