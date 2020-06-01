using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class GameMaster : MonoBehaviour
{
	public static GameMaster gm;

	[SerializeField]
	private int maxLives = 3;
	private static int _remainingLives;
	public static int RemainingLives
	{
		get { return _remainingLives; }
	}

	[SerializeField]
	private int startingMoney;
	public static int Money;

	void Awake()
	{
		if (gm == null)
		{
			gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
		}
	}

	public Transform playerPrefab;
	public Transform spawnPoint;
	public float spawnDelay = 2;
	public Transform spawnPrefab;
	public string respawnCountdownSoundName = "RespawnCountdown";
	public string spawnSoundName = "Spawn";

	public string gameOverSoundName = "GameOver";

	public CameraShake cameraShake;

	[SerializeField]
	private GameObject gameOverUI;

	[SerializeField]
	private GameObject upgradeMenu;

	public delegate void UpgradeMenuCallback(bool active);
	public UpgradeMenuCallback onToggleUpgradeMenu;

	//cache
	private AudioManager audioManager;

	void Start()
	{
		if (cameraShake == null)
		{
			Debug.LogError("No camera shake referenced in GameMaster");
		}

		_remainingLives = maxLives;

		Money = startingMoney;

		//caching
		audioManager = AudioManager.instance;
		if (audioManager == null)
		{
			Debug.LogError("FREAK OUT! No AudioManager found in the scene.");
		}
	}

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.U))
		{
			ToggleUpgradeMenu();
        }
	}

	private void ToggleUpgradeMenu ()
	{
		upgradeMenu.SetActive( !upgradeMenu.activeSelf );
		onToggleUpgradeMenu.Invoke(upgradeMenu.activeSelf);
	}

	public void EndGame()
	{
		audioManager.PlaySound(gameOverSoundName);

		Debug.Log("GAME OVER");
		gameOverUI.SetActive(true);
	}

	public IEnumerator _RespawnPlayer()
	{
		audioManager.PlaySound(respawnCountdownSoundName);
		yield return new WaitForSeconds(spawnDelay);

		audioManager.PlaySound(spawnSoundName);
		Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
		GameObject clone = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation) as GameObject;
		Destroy(clone, 3f);
	}

	public static void KillPlayer(Player player)
	{
		Destroy(player.gameObject);
		_remainingLives -= 1;
		if (_remainingLives <= 0)
		{
			gm.EndGame();
		}
		else
		{
			gm.StartCoroutine(gm._RespawnPlayer());
		}
	}

	public static void KillEnemy(Enemy enemy)
	{
		gm._KillEnemy(enemy);
	}
	public void _KillEnemy(Enemy _enemy)
	{
		// Let's play some sound
		audioManager.PlaySound(_enemy.deathSoundName);

		// Add particles
		GameObject _clone = Instantiate(_enemy.deathParticles, _enemy.transform.position, Quaternion.identity) as GameObject;
		Destroy(_clone, 5f);

		// Go camerashake
		cameraShake.Shake(_enemy.shakeAmt, _enemy.shakeLength);
		Destroy(_enemy.gameObject);
	}

}
