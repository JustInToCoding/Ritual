using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public float direction;
	public float speed;
	public float damage;
	public Animator anim;
	private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start() {
//		anim = GetComponent<Animator>();
		rigidBody = GetComponent<Rigidbody2D>();
//		gameObject.GetComponent<CircleCollider2D>().enabled = false;
//		anim.Play("cannonball");

	}

	// Update is called once per frame
	void FixedUpdate () {

		if(direction == 1){
			rigidBody.velocity = new Vector3(0, speed, 0);
		}else if(direction == 2){
			rigidBody.velocity = new Vector3(-speed, 0, 0);
		}else if(direction == 3){
			rigidBody.velocity = new Vector3(0, -speed, 0);
		}else if(direction == 4){
			rigidBody.velocity = new Vector3(speed, 0, 0);
		}

//		if(Vector3.Distance(transform.position, target) < 0.1f){
			//			print("anim explode");
//			anim.CrossFade("cannonball_explosion", 0.4f);
//			gameObject.GetComponent<CircleCollider2D>().enabled = true;
//			StartCoroutine(removeObject());
//		}
	}

	IEnumerator removeObject() {
		yield return new WaitForSeconds(0.6f); //this will wait 1 second
		Destroy(gameObject);
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Enemy") {
			other.gameObject.GetComponent<EnemyController> ().collide (this.gameObject);
		}
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<PlayerController> ().hit (this.gameObject);
		}
		if (other.gameObject.tag == "Wall") {
			Destroy (gameObject);
		}
	}
}
