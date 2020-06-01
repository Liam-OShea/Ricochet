using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour {

	[SerializeField]
	private Text healthText;

	[SerializeField]
	private Text speedText;

	[SerializeField]
	private float healthMultiplier = 1.3f;

	[SerializeField]
	private float movementSpeedMultiplier = 1.3f;

	[SerializeField]
	private int upgradeCost = 50;

	private PlayerStats stats;

	void OnEnable ()
	{
		stats = PlayerStats.instance;
		UpdateValues();
    }

	void UpdateValues ()
	{
		healthText.text = "HEALTH: " + stats.maxHealth.ToString();
		speedText.text = "SPEED: " + stats.movementSpeed.ToString();
    }

	public void UpgradeHealth ()
	{
		if (GameMaster.Money < upgradeCost)
		{
			AudioManager.instance.PlaySound("NoMoney");
			return;
		}

		stats.maxHealth = (int)(stats.maxHealth * healthMultiplier);

		GameMaster.Money -= upgradeCost;
		AudioManager.instance.PlaySound("Money");

		UpdateValues();
	}

	public void UpgradeSpeed()
	{
		if (GameMaster.Money < upgradeCost)
		{
			AudioManager.instance.PlaySound("NoMoney");
			return;
		}

		stats.movementSpeed = Mathf.Round (stats.movementSpeed * movementSpeedMultiplier);

		GameMaster.Money -= upgradeCost;
		AudioManager.instance.PlaySound("Money");

		UpdateValues();
	}

}
