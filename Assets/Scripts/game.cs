using UnityEngine;
using System.Collections;

public class game : MonoBehaviour {

	public float objectSpeed = 1.0f;
	public GameObject treeUp, treeDown, treeLeft;
	private int score = 0, bestScore = 0;
	private Vector3 treeUpOrigin, treeDownOrigin, treeLeftOrigin;
	private int rand = 1;
	private bool moving = true;
	static public bool gameStarted = false;

	// Use this for initialization
	void Start () {
		if (treeUp)
			treeUpOrigin = treeUp.transform.position;
		if (treeDown)
			treeDownOrigin = treeDown.transform.position;
		if (treeLeft)
			treeLeftOrigin = treeLeft.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameStarted) {
			if (moving) {
				Vector3 newPosition;

				switch (rand) {
				case 0:
					newPosition = treeUp.transform.position;
					newPosition.x -= objectSpeed * Time.deltaTime;

					treeUp.transform.position = newPosition;

					if (treeUp.transform.position.x <= -35) {
						treeUp.transform.position = treeUpOrigin;
						score++;
						moving = false;
					}
					break;
				case 1:
					newPosition = treeDown.transform.position;
					newPosition.x -= objectSpeed * Time.deltaTime;

					treeDown.transform.position = newPosition;

					if (treeDown.transform.position.x <= -35) {
						treeDown.transform.position = treeDownOrigin;
						score++;
						moving = false;
					}
					break;
				case 2:
					newPosition = treeLeft.transform.position;
					newPosition.x -= objectSpeed * Time.deltaTime;

					treeLeft.transform.position = newPosition;

					if (treeLeft.transform.position.x <= -35) {
						treeLeft.transform.position = treeLeftOrigin;
						score++;
						moving = false;
					}
					break;
				case 3:
					newPosition = treeLeft.transform.position;
					newPosition.x -= objectSpeed * Time.deltaTime;
					treeLeft.transform.position = newPosition;

					newPosition = treeUp.transform.position;
					newPosition.x -= objectSpeed * Time.deltaTime;
					treeUp.transform.position = newPosition;

					if (treeUp.transform.position.x <= -35) {
						treeUp.transform.position = treeUpOrigin;
						score++;
						moving = false;
					}
					break;
				}
			} else {
				rand = Random.Range (0, 4);
				Debug.Log (rand.ToString ());
				moving = true;
			}
		} else {
			if (Input.GetButtonDown ("Jump")) {
				gameStarted = true;

				if (score > bestScore)
					bestScore = score;
				
				score = 0;
				treeUp.transform.position = treeUpOrigin;
				treeDown.transform.position = treeDownOrigin;
				treeLeft.transform.position = treeLeftOrigin;
				moving = true;
				rand = 1;
			}
		}
	}

	void OnGUI(){
		Rect position = new Rect (100.0f, 10.0f, 200.0f, 40.0f);

		GUI.Label (position, "Score: " + score.ToString ());
		position.y = 30.0f;
		GUI.Label (position, "Highscore: " + bestScore.ToString ());

		if (gameStarted == false) {
			Rect positionPlay = new Rect (200.0f, 200.0f, 400.0f, 40.0f);

			GUI.Label (positionPlay, "Appuyer sur ESPACE pour commencer");
		}
	}
}