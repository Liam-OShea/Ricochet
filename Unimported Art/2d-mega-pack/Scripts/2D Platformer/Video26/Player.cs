using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	[System.Serializable]
	public class PlayerStats {
		public int maxHealth = 100;

		private int _curHealth;
		public int curHealth
		{
			get { return _curHealth; }
			set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
		}

		public void Init()
		{
			curHealth = maxHealth;
		}
	}

	public PlayerStats stats = new PlayerStats();

	public int fallBoundary = -20;

	public string deathSoundName = "DeathVoice";
	public string damageSoundName = "Grunt";

	private AudioManager audioManager;

	[SerializeField]
	private StatusIndicator statusIndicator;

	void Start()
	{
		stats.Init();

		if (statusIndicator == null)
		{
			Debug.LogError("No status indicator referenced on Player");
		}
		else
		{
			statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
		}

		audioManager = AudioManager.instance;
		if (audioManager == null)
		{
			Debug.LogError("PANIC! No audiomanager in scene.");
		}
	}

	void Update () {
		if (transform.position.y <= fallBoundary)
			DamagePlayer (9999999);
	}

	public void DamagePlayer (int damage) {
		stats.curHealth -= damage;

		if (stats.curHealth <= 0)
		{
			//play death sound
			audioManager.PlaySound(deathSoundName);

			//kill player
			GameMaster.KillPlayer(this);
		} else
		{
			//play damage sound
			audioManager.PlaySound(damageSoundName);
		}

		statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
	}

}
