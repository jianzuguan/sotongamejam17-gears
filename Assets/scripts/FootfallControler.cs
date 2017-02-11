using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootfallControler : MonoBehaviour {

    private bool grounded = false;
    private int state = 0;

    private Vector3 groundedlocation;
    private string groundedname;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // This part detects whether or not the object is grounded and stores it in a variable
    void OnCollisionEnter(Collision collision) 
    {
        foreach (ContactPoint contact in collision.contacts) {

            if (contact.thisCollider.name != groundedname)
            {
                groundedname = contact.thisCollider.name;
                groundedlocation.x = GameObject.Find(groundedname).GetComponent<Transform>().position.x;
                groundedlocation.y = GameObject.Find(groundedname).GetComponent<Transform>().position.y;
                groundedlocation.z = GameObject.Find(groundedname).GetComponent<Transform>().position.z;
                Debug.Log("New collision point " + groundedname + " at " + groundedlocation.ToString());
            }
            Debug.DrawRay(contact.point, contact.normal*100, Color.red);
        }

	    state ++;
	    if(state > 0)
	    {
		    grounded = true;
	    }
    }
 
 
    void OnCollisionExit ()
    {
	    state --;
	    if(state < 1)
	    {
		    grounded = false;
		    state = 0;
	    }
    }

    // This is called every physics frame
    void FixedUpdate()
    {
        if(grounded)
        {
            Vector3 currentloc = GameObject.Find(groundedname).GetComponent<Transform>().position;
            Vector3 movement = currentloc - groundedlocation;
            //Debug.Log("Moved"+movement.ToString()+" From: "+groundedlocation.ToString()+" to "+currentloc.ToString());

            transform.position -= movement;
        }
    }
}