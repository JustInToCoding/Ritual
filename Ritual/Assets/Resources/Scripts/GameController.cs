using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject LiveIndicatorPrefab;

	//the controller with variables of the player
	private PlayerController playerController;

	//amount of lives of the player 
	private List<GameObject> Lives;

	// Use this for initialization
	void Start () {
		Lives = new List<GameObject>();
		playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		var amountOfLives = playerController.amountOfLives;

		Camera cam = Camera.main;
		float height =(2f * cam.orthographicSize)/2;
		float width = (2f * cam.orthographicSize * cam.aspect)/2;

		var spacing = 0f;
		for(var i=0; i < amountOfLives; i++){
			var live = Instantiate(LiveIndicatorPrefab, new Vector3(-width+spacing,height-0.5f,0), Quaternion.identity) as GameObject;
			live.transform.parent = Camera.main.transform;
			live.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
			Lives.Add(live);
			spacing += 0.7f;
		}
	
	}
		
	//removes one live from ui
	public void RemoveLive(){
		GameObject liveToLose = Lives[Lives.Count - 1];
		Lives.RemoveAt(Lives.Count-1);
		Destroy(liveToLose);
	}

	//change the scene to gameover scene
	public void GameOver(){
		SceneManager.LoadScene("GameOver");
	}
}
