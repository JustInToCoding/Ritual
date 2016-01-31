using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SpashScreenController : MonoBehaviour {

	public string SceneName;
	public int WaitTime;

	// Use this for initialization
	void Start () {
		StartCoroutine (StartNextScene ());
	}
	
	IEnumerator StartNextScene(){
		yield return new WaitForSeconds(WaitTime);
		SceneManager.LoadScene(SceneName);
	}
}
