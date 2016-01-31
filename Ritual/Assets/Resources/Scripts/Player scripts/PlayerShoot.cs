using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

	public Rigidbody2D BulletPrefab;
//	public float BulletSpeed;
	public float AttackSpeed;
	float Cooldown;

	private Animator animator;

	void Start(){
		animator = GetComponent<Animator> ();
	}


	// Update is called once per frame
	void Update () {

		if(Time.time >= Cooldown){
			animator.SetBool ("isShooting", false);

			if(Input.GetKey(KeyCode.Space)){
				Fire();
			}
			if (Input.GetMouseButton(0)) {
				Fire();
			}
		}
	}

	void Fire(){
		animator.SetBool ("isShooting", true);
//		var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//			targetPos.z = transform.position.z;
//
//		print("y:"+Input.mousePosition+" x:"+targetPos.x+" z:"+targetPos.z);
//			AudioSource audioSource = gameObject.GetComponent<AudioSource>();
//			audioSource.Play();

//		bulletStartLocation.x -= 0.2f;

//		bPrefab.transform.position = Vector3.MoveTowards(transform.position, targetPos, BulletSpeed * Time.deltaTime);

		//get the thing component on your instantiated object
//		
		StartCoroutine(MakeBullet());
		Cooldown = Time.time + AttackSpeed;
		//anim.Play("cannon");

	}

	IEnumerator MakeBullet() {
		yield return new WaitForSeconds(0.2f); //this will wait 0.15 second
		Vector3 bulletStartLocation = transform.position;
		bulletStartLocation.y += 0.2f;
//		bulletStartLocation.x += 0.2f;
		Rigidbody2D bPrefab = Instantiate(BulletPrefab, bulletStartLocation, Quaternion.identity) as Rigidbody2D;


		BulletController cbController = bPrefab.GetComponent<BulletController>();

		PlayerController playerController = gameObject.GetComponent<PlayerController>();
		//now get the variable from the script reference we just made
		float playerDirection = playerController.direction;

		cbController.direction = playerDirection;
	}

}


