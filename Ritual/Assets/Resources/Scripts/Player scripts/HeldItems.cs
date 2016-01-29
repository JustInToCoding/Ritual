using UnityEngine;
using System.Collections;

public class HeldItems : MonoBehaviour {

    public int currBoss;
    public int heldSacrificialItems;
    // public LevelGenerator level;

	// Use this for initialization
	void Start () {
        currBoss = 1;
        heldSacrificialItems = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (heldSacrificialItems >= 3)
        {
            StartRitual();
            Reset();
        }

	}

    void StartRitual()
    {
        // Start the ritual when 3 items are collected at the altar
        // level.SpawnEnemies = true;
        // GameObject Boss = Instantiate (Resources.Load("Resources/Prefabs/Bosses/Boss#1"), transform.position, transform.rotation);
        // Reset();
        
    }

    void Reset()
    {
        heldSacrificialItems = 0;
    }
}
