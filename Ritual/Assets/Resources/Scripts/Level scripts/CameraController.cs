using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	// used for following the player
	public float maxX;
	public float minX;
	public float maxY;
	public float minY;

	private Vector2 velocity;
	public float smootTimeY;
	public float smootTimeX;

	private GameObject player;

	public void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");

	}


	public void FixedUpdate(){

		float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smootTimeX);
		float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smootTimeY);
		 
		if(player.transform.position.x > maxX || player.transform.position.x < minX){
			posX = transform.position.x;
		}
		if(player.transform.position.y > maxY || player.transform.position.y < minY){
			posY = transform.position.y;
		}
		transform.position = new Vector3(posX, posY, transform.position.z);
	}
		
}
