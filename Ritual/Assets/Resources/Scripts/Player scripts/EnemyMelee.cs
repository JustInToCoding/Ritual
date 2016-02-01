using UnityEngine;
using System.Collections;

public class EnemyMelee : EnemyController {
	protected bool againstPlayer;
	protected float Cooldown;
	public float AttackSpeed = 1;


	public override void FixedUpdate () {
		againstPlayer = (xDifPlayer > - 0 && xDifPlayer < 0) && (yDifPlayer > -0 && yDifPlayer < 0);
		xDifPlayer = player.transform.position.x - transform.position.x;
		yDifPlayer = player.transform.position.y - transform.position.y;

		if (path.vectorPath.Count - currentWaypoint < 3 && againstPlayer) {

			if (Time.time >= Cooldown) {
				Attack ();
			}
			return;
		}else{
			animation.SetBool ("isShooting", false);
		}
		base.FixedUpdate ();
	}

	public override void createNewPath () {
		if (currentWaypoint >= path.vectorPath.Count) {
			if (!againstPlayer) {
				seeker.StartPath (transform.position, player.transform.position, OnPathComplete);
			}
			return;
		}
	}

	// Update is called once per frame
	void Attack () {
		bool playerInRange = (xDifPlayer > -1 && xDifPlayer < 1) && (yDifPlayer > -1 && yDifPlayer < 1);
		if (playerInRange) {
			player.GetComponent<PlayerController>().hit (gameObject);
			Cooldown = Time.time + AttackSpeed;
		}
	}
}
