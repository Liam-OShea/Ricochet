using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Platformer2DUserControl))]
public class Player : MonoBehaviour
{
	public int fallBoundary = -20;

	public string deathSoundName = "DeathVoice";
	public string damageSoundName = "Grunt";

	private AudioManager audioManager;

	[SerializeField]
	private StatusIndicator statusIndicator;

	private PlayerStats stats;

	void Start()
	{
		stats = PlayerStats.instance;

		stats.curHealth = stats.maxHealth;

		if (statusIndicator == null)
		{
			Debug.LogError("No status indicator referenced on Player");
		}
		else
		{
			statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
		}

		GameMaster.gm.onToggleUpgradeMenu += OnUpgradeMenuToggle;

		audioManager = AudioManager.instance;
		if (audioManager == null)
		{
			Debug.LogError("PANIC! No audiomanager in scene.");
		}

		InvokeRepeating("RegenHealth", 1f/stats.healthRegenRate, 1f/stats.healthRegenRate);
	}

	void RegenHealth ()
	{
		stats.curHealth += 1;
		statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
	}

	void Update()
	{
		if (transform.position.y <= fallBoundary)
			DamagePlayer(9999999);
	}

	void OnUpgradeMenuToggle(bool active)
	{
		GetComponent<Platformer2DUserControl>().enabled = !active;
		Weapon _weapon = GetComponentInChildren<Weapon>();
		if (_weapon != null)
			_weapon.enabled = !active;
	}

	void OnDestroy()
	{
		GameMaster.gm.onToggleUpgradeMenu -= OnUpgradeMenuToggle;
	}

	public void DamagePlayer(int damage)
	{
		stats.curHealth -= damage;

		if (stats.curHealth <= 0)
		{
			//play death sound
			audioManager.PlaySound(deathSoundName);

			//kill player
			GameMaster.KillPlayer(this);
		}
		else
		{
			//play damage sound
			audioManager.PlaySound(damageSoundName);
		}

		statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
	}

}
