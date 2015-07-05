using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	private int count;

	public float speed;
	public Text countText;
	public Text winText;
	public Text gameOver;

	void Start()
	{
		count = 0;
		rb = GetComponent<Rigidbody>();
		setCountText ();
		winText.text = "";
		gameOver.text = "";
	}

	void Update()
	{
	
	}

	void FixedUpdate()
	{
		//Keyboard Input
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical"); 

//		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		Vector3 movement = new Vector3(Input.acceleration.x, 0.0f, Input.acceleration.y);

		rb.AddForce(movement * speed);
	}

	void OnTriggerEnter(Collider other)
	{	
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);
			count++;
			gameOver.text = "";
			setCountText();
		}

		if (other.gameObject.CompareTag ("Hole")) {
			count = 0;
			gameOver.text = "Score Reset!";
			setCountText();
		}
	}

	void setCountText()
	{
		countText.text = "Score: " + count.ToString ();

		if (count >= 8) {
			winText.text = "You win!";
		}
	}
}
