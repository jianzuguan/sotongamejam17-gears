using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour {

    public GameObject elevator;
    public GameObject heaven;  // Or hell
    public GameObject player;
    public GameObject badEndUI;

    public Vector3 end;

    public float speed = 1.0F;
    private float journeyLength;

    private bool launched = false;
    private Vector3 elevatorOri;

    // Use this for initialization
    void Start () {
        elevatorOri = elevator.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.launched && (transform.position.y >= end.y)) {
            elevator.transform.position += Time.deltaTime * speed * -elevator.transform.up;
        }
        if (this.launched && player.transform.position.y < 20) {
            // Change layer to Player.
            player.layer = 8;
            this.badEndUI.SetActive(false);

            heaven.SetActive(true);
            elevator.transform.position = elevatorOri;
            this.launched = false;
        }
	}

    // Only collid with Player (Layer)
    private void OnCollisionEnter(Collision collision) {
        heaven.SetActive(false);
        // Launch elevator.
        this.launched = true;
    }
}
