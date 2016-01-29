using UnityEngine;
using System.Collections;

public class SacrificialItem : Item {

    private GameObject player;

	// Use this for initialization
	void Start () {
	    player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player.GetComponent<HeldItems>().heldSacrificialItems++;
            Destroy(this.gameObject);
        }
    }
}
