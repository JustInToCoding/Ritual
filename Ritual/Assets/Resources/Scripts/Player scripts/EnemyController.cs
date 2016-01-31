﻿using UnityEngine;
using System.Collections;
using Pathfinding;

public class EnemyController : MonoBehaviour {
	public GameObject projectile;
	public float playerRange;
	public float attackRange;
	public float direction;
	public Animator animation;
	protected GameObject player;

	float health = 40;
	protected float speed = 2;
	protected float Cooldown;
	protected int AttackSpeed = 1;
	protected bool canAttack = true;
	bool walking = false;

	protected float xDifPlayer;
	protected float yDifPlayer;
	Vector2 target;

	Seeker seeker;
	public Path path;
	public float nextWaypointDistance = 0.02f;
	int currentWaypoint = 0;


	// Use this for initialization
	public virtual void Start () {
		direction = 1;
		player = GameObject.FindGameObjectWithTag("Player");
		animation = GetComponent<Animator> ();
		seeker = GetComponent<Seeker> ();
		seeker.StartPath (transform.position, player.transform.position, OnPathComplete);
	}

	public void FixedUpdate () {
		bool playerInRange = (xDifPlayer > -playerRange && xDifPlayer < playerRange) && (yDifPlayer > -playerRange && yDifPlayer < playerRange);
		xDifPlayer = player.transform.position.x - transform.position.x;
		yDifPlayer = player.transform.position.y - transform.position.y;
		walking = false;
		calculateDirection ();
		Animate ();
		if (path == null)
		{
			//We have no path to move after yet
			return;
		}

		if (path.vectorPath.Count - currentWaypoint < 3 && playerInRange) {
			
			if (Time.time >= Cooldown) {
				Attack ();
			}
			return;
		}

		if (currentWaypoint >= path.vectorPath.Count)
		{
			if (!playerInRange) {
				seeker.StartPath (transform.position, player.transform.position, OnPathComplete);
			}
			//Debug.Log( "End Of Path Reached" );
			return;
		}
			

		//Direction to the next waypoint
		Vector3 dir = ( path.vectorPath[ currentWaypoint ] - transform.position).normalized;
		dir *= speed * Time.fixedDeltaTime;
		this.gameObject.transform.Translate( dir );
		walking = true;
		Animate ();
		//Check if we are close enough to the next waypoint
		//If we are, proceed to follow the next waypoint
		if (Vector3.Distance(path.vectorPath[ currentWaypoint ], transform.position ) < nextWaypointDistance)
		{
			currentWaypoint++;
			return;
		}
	}

	public virtual void Attack() {
		bool playerInRange = (xDifPlayer > -attackRange && xDifPlayer < attackRange) && (yDifPlayer > -attackRange && yDifPlayer < attackRange);
		calculateDirection ();
		if (playerInRange && canAttack) {
			GameObject attack = Instantiate (projectile, transform.position, Quaternion.identity) as GameObject;
			BulletController attackController = attack.GetComponent<BulletController> ();
			attackController.direction = direction;
			attack.transform.position = transform.position;
			Cooldown = Time.time + AttackSpeed;
		}
	}

	void Hit(float damage) {
		health -= damage;
		if (health <= 0) {
			Destroy (gameObject);
		}
	}

	public void collide(GameObject col){
		if (col.tag == "PlayerBullet") {
			Hit (col.GetComponent<BulletController>().damage);
			Destroy (col);
			//Debug.Log (col.gameObject);
		}
		if (col.tag == "EnemeyBullet") {
			Destroy (col);
		}
	}

	protected void calculateDirection() {
		float angle  = (Mathf.Atan2((transform.position.x - player.transform.position.x), (transform.position.y - player.transform.position.y)) * 180 / Mathf.PI) + 180;

		if (angle > 45 && angle < 135) {
			direction = 4; // right
		}else if (angle > 135 && angle < 225) {
			direction = 3; // down
		}else if (angle > 225 && angle < 315) {
			direction = 2; // left
		}else if (angle > 315 || angle < 45) {
			direction = 1; // up
		}
	}

	void Animate () {
		Vector3 temp = this.transform.localScale;

		if (walking) {
			if (direction == 4) {
				temp.x = 0.3f;
				animation.SetBool ("isWalking", true);
			} else if (direction == 3) {
				animation.SetBool ("isWalking", true);
			} else if (direction == 2) {
				temp.x = -0.3f;
				animation.SetBool ("isWalking", true);
			} else if (direction == 1) {
				animation.SetBool ("isWalking", true);
			}
			this.transform.localScale = temp;
		} else {
			animation.SetBool ("isWalking", false);
		}
	}

	public void OnPathComplete ( Path p ) {
		if (!p.error) {
			path = p;
			//Reset the waypoint counter
			currentWaypoint = 0;
		}
	}
}
