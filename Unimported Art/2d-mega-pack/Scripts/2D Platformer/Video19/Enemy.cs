using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	[System.Serializable]
	public class EnemyStats {
		public int maxHealth = 100;

		private int _curHealth;
		public int curHealth
		{
			get { return _curHealth; }
			set { _curHealth = Mathf.Clamp (value, 0, maxHealth); }
		}

		public void Init()
		{
			curHealth = maxHealth;
		}
	}
	
	public EnemyStats stats = new EnemyStats();

	[Header("Optional: ")]
	[SerializeField]
	private StatusIndicator statusIndicator;

	void Start()
	{
		stats.Init();

		if (statusIndicator != null)
		{
			statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
		}
	}
	
	public void DamageEnemy (int damage) {
		stats.curHealth -= damage;
		if (stats.curHealth <= 0)
		{
			GameMaster.KillEnemy (this);
		}

		if (statusIndicator != null)
		{
			statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
		}
	}
}
