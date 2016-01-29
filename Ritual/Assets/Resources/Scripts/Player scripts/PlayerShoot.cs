using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

	public Rigidbody2D BulletPrefab;
	public float BulletSpeed;
	public float AttackSpeed;
	float Cooldown;



	// Update is called once per frame
	void Update () {

		if(Time.time >= Cooldown){

			if(Input.GetKey(KeyCode.Space)){
				Fire();
			}
			if (Input.GetMouseButton(0)) {
				Fire();
			}
		}
	}

	void Fire(){

			
//		var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//			targetPos.z = transform.position.z;
//
//		print("y:"+Input.mousePosition+" x:"+targetPos.x+" z:"+targetPos.z);
//			AudioSource audioSource = gameObject.GetComponent<AudioSource>();
//			audioSource.Play();

		Rigidbody2D bPrefab = Instantiate(BulletPrefab, transform.position, Quaternion.identity) as Rigidbody2D;
//			bPrefab.transform.position = Vector3.MoveTowards(transform.position, targetPos, BulletSpeed * Time.deltaTime);

		//get the thing component on your instantiated object
		BulletController cbController = bPrefab.GetComponent<BulletController>();
		
		PlayerController playerController = gameObject.GetComponent<PlayerController>();
		//now get the variable from the script reference we just made
		float playerDirection = playerController.direction;

		cbController.direction = playerDirection;

		Cooldown = Time.time + AttackSpeed;
		//anim.Play("cannon");

	}

}


