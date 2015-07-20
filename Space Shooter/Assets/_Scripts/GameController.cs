using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public Text scoreText;
	private int score;

	public Text gameOverText;
	public Text restartText;

	public GameObject restartBtnObject;
	public GameObject canvasObj;

	private bool gameOver;
	private bool restart;

	void Start()
	{
		gameOverText.text = "";
		restartText.text = "";
		gameOver = false;
		restart = false;

		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}

	void Update()
	{
		if (restart)
		{
			if (Input.GetKeyDown(KeyCode.R))
			{
				Restart();
			}
		}
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds (startWait);
		
		while (true) {
			for (int i=0; i<hazardCount;i++)
			{
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(spawnWait);
			}
			yield return new WaitForSeconds(waveWait);

			if (gameOver)
			{
				restartText.text = "Press 'R' to restart game!";
				restart = true;

				GameObject restartBtn = Instantiate(restartBtnObject) as GameObject;
				restartBtn.transform.SetParent(canvasObj.transform, false);

				Button rbt = restartBtn.GetComponent<Button>();
				rbt.onClick.AddListener(() => Restart());

				break;
			}
		}
	}

	public void AddScore(int newScore)
	{
		score += newScore;
		UpdateScore ();
	}

	public void GameOver()
	{
		gameOverText.text = "Game Over!";
		gameOver = true;
	}

	public void Restart()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}
}
