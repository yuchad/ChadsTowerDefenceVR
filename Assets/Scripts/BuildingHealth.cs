using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHealth : MonoBehaviour {
    public int startingHealth = 5;
    public int currentHealth;
    // Use this for initialization
    void Start () {
        currentHealth = startingHealth;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(int amount) {
        currentHealth -= amount;

        if (currentHealth <= 0) {
            Debug.Log("This building has died fam");
        }
    }
}
