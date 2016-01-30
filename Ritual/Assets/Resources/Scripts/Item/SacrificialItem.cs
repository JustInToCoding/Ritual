using UnityEngine;
using System.Collections;

public class SacrificialItem : MonoBehaviour {

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
            if (HeldItems.heldSacrificialItems < 3)
            {
                HeldItems.heldSacrificialItems++;
                Destroy(this.gameObject);
            }
        }
    }
}
