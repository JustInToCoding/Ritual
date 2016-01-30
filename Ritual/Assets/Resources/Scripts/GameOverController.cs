using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
			StartCoroutine(GoToMenu());
	}

	IEnumerator GoToMenu() {
		yield return new WaitForSeconds(2.0f); //this will wait 1 second
		SceneManager.LoadScene("Menu");
	}
}
