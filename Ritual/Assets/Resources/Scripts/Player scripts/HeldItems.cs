using UnityEngine;
using System.Collections;

public class HeldItems : MonoBehaviour {

    public int currBoss;
    public static int heldSacrificialItems;
    // public LevelGenerator level;

	// Use this for initialization
	void Start () {
        currBoss = 1;
        heldSacrificialItems = 0;
	}
	
	// Update is called once per frame
	void Update () {

	}

    public static void Reset()
    {
        heldSacrificialItems = 0;
    }
}
