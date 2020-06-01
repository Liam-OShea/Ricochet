using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class GameMaster : MonoBehaviour {

	public static GameMaster gm;

	[SerializeField]
	private int maxLives = 3;
	private static int _remainingLives;
	public static int RemainingLives
	{
		get { return _remainingLives; }
	}

	void Awake () {
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GM").GetComponent<GameMaster>();
		}
	}

	public Transform playerPrefab;
	public Transform spawnPoint;
	public float spawnDelay = 2;
	public Transform spawnPrefab;
	public string spawnSoundName;

	public CameraShake cameraShake;

	[SerializeField]
	private GameObject gameOverUI;

	//cache
	private AudioManager audioManager;

	void Start()
	{
		if (cameraShake == null)
		{
			Debug.LogError("No camera shake referenced in GameMaster");
		}

		_remainingLives = maxLives;

		//caching
		audioManager = AudioManager.instance;
		if (audioManager == null)
		{
			Debug.LogError("FREAK OUT! No AudioManager found in the scene.");
		}
	}

	public void EndGame ()
	{
		Debug.Log("GAME OVER");
		gameOverUI.SetActive(true);
	}

	public IEnumerator _RespawnPlayer () {
		audioManager.PlaySound(spawnSoundName);
		yield return new WaitForSeconds (spawnDelay);

		Instantiate (playerPrefab, spawnPoint.position, spawnPoint.rotation);
		GameObject clone = Instantiate (spawnPrefab, spawnPoint.position, spawnPoint.rotation) as GameObject;
		Destroy (clone, 3f);
	}

	public static void KillPlayer (Player player) {
		Destroy (player.gameObject);
		_remainingLives -= 1;
		if (_remainingLives <= 0)
		{
			gm.EndGame();
		} else
		{
			gm.StartCoroutine(gm._RespawnPlayer());
		}
	}

	public static void KillEnemy (Enemy enemy) {
		gm._KillEnemy(enemy);
	}
	public void _KillEnemy(Enemy _enemy)
	{
		GameObject _clone = Instantiate(_enemy.deathParticles, _enemy.transform.position, Quaternion.identity) as GameObject;
		Destroy(_clone, 5f);
		cameraShake.Shake(_enemy.shakeAmt, _enemy.shakeLength);
		Destroy(_enemy.gameObject);
	}

}
