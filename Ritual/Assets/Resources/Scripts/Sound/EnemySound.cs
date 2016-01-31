using UnityEngine;
using System.Collections;

public class EnemySound : MonoBehaviour {

    AudioSource au_s;
    public AudioClip hit, enemyDeath, growl;
	// Use this for initialization
	void Start () {
        au_s = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
