using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstructController : MonoBehaviour {

    public GameObject badEndUI;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision) {
        switch (collision.gameObject.tag) {
            case "Player":
                this.badEndUI.SetActive(true);
                // Change to Layer Ghost.
                collision.gameObject.layer = 9;
                collision.transform.position = new Vector3(
                    collision.transform.position.x,
                    2,
                    collision.transform.position.z);
                collision.gameObject.GetComponent<FootfallControler>().clearGrounded();
                break;
        }
    }
}
