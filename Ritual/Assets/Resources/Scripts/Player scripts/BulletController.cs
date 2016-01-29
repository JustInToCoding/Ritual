using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public float direction;
	public float speed;
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
	void Update () {

//		float step = speed * Time.deltaTime;
//		transform.position = Vector3.MoveTowards(transform.position, target, step);
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
}
