using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour {

	[SerializeField]
	string mouseHoverSound = "ButtonHover";

	[SerializeField]
	string buttonPressSound = "ButtonPress";

	AudioManager audioManager;

	void Start ()
	{
		audioManager = AudioManager.instance;
		if (audioManager == null)
		{
			Debug.LogError("FREAK OUT! No AudioManager found in the scene.");
		}
	}

	public void Quit ()
	{
		audioManager.PlaySound(buttonPressSound);

		Debug.Log("APPLICATION QUIT!");
		Application.Quit();
	}

	public void Retry ()
	{
		audioManager.PlaySound(buttonPressSound);

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void OnMouseOver ()
	{
		audioManager.PlaySound(mouseHoverSound);
	}
	
}
