using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    /*public Transform[] target;
    public float speed;

    private int current;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position != target[current].position) {
            Vector3 pos = Vector3.MoveTowards(transform.position, target[current].position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
            print("hello");
        }
        else {

            current = (current + 1) % target.Length;
            print("this is reached");
        }
	}
    */

    public GameObject[] waypoints;
    public int num = 0;

    public float minDist;
    public float speed;

    public bool rand = false;
    public bool go = true;

    public int damage = 1;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        float dist = Vector3.Distance(gameObject.transform.position, waypoints[num].transform.position);

        if (go) {
            if(dist > minDist) {
                Move();
            }
            else {
                if (!rand) {
                    if(num+1 == waypoints.Length) {
                        //num = 0;
                    }
                    else {
                        num++;
                    }
                }
                else {
                    num = Random.Range(0, waypoints.Length);
                }
            }
        }
    }
     public void Move() {
        gameObject.transform.LookAt(waypoints[num].transform.position);
        gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.collider.tag == "MainHome") {
            BuildingHealth buildingHealth = collision.collider.GetComponent<BuildingHealth>();
            if (buildingHealth != null) {
                buildingHealth.TakeDamage(damage);
            }
            Destroy(this.gameObject);
        }
    }
}
