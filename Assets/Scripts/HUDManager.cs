using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public Text healthText;
    public Text bouncesText;
    public Text collectiblesText;
    public Text livesText;

    private GameObject mostRecentProjectile;

    private PlayerCharacter player;

    // Update is called once per frame
    void Start()
    {
        player = FindObjectOfType<PlayerCharacter>();
    }
    void Update()
    {
        healthText.text = player.getHealth().ToString();
        livesText.text = player.getLives().ToString();
        collectiblesText.text = player.getCollectibles().ToString();
        if(mostRecentProjectile){
            bouncesText.text = mostRecentProjectile.GetComponent<RicochetProjectileController>().getNumberOfBounces().ToString();
         }
    }

    public void setMostRecentRicochetProjectile(GameObject projectile){
        mostRecentProjectile = projectile;
    }
}
