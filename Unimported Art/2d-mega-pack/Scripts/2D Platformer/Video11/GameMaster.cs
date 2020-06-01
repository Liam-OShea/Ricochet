using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

	public static void KillPlayer (Player player) {
		Destroy (player.gameObject);
	}

}