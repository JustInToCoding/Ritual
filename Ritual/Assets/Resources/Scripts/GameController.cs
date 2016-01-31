using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject LiveIndicatorPrefab;
	public Text HeldItemsText;

	private int HeldItemsHold;

	//the controller with variables of the player
	private PlayerController playerController;

	//amount of lives of the player 
	private List<GameObject> Lives;

	// Use this for initialization
	void Start () {
		Lives = new List<GameObject>();
		playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		int amountOfLives = 3;//default levels
		if(playerController!=null){
			amountOfLives = playerController.amountOfLives;
		}

        Camera cam = Camera.main;
        float height = (2f * cam.orthographicSize) / 2;
        float width = (2f * cam.orthographicSize * cam.aspect) / 2;

		var spacing = 0.3f;
		for(var i=0; i < amountOfLives; i++){
			var live = Instantiate(LiveIndicatorPrefab, new Vector3(-width+spacing + cam.transform.position.x, height-0.2f + cam.transform.position.y, 0), Quaternion.identity) as GameObject;
			live.transform.parent = Camera.main.transform;
			live.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
			Lives.Add(live);
			spacing += 0.3f;
		}
	}

	void Update()
	{
		if(HeldItemsHold != HeldItems.heldSacrificialItems){
			HeldItemsHold = HeldItems.heldSacrificialItems;
			HeldItemsText.text = HeldItemsHold+" of "+(5 - Altar.sacrificedItems)+" Remaining Items";
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
