using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {


	//opens gameScene
	public void StartGame(string sceneToChangeTo){
		SceneManager.LoadScene(sceneToChangeTo);
	}
		
}
