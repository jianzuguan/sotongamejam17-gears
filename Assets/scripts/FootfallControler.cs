using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootfallControler : MonoBehaviour {

    private bool grounded = false;
    private int state = 0;
    private int stateLeft;
    private int stateRight;

    private Vector3 groundedlocation;
    private string groundedname;

    private Vector3 modeloc;


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

            if (contact.thisCollider.name != groundedname && contact.otherCollider.name != "Gear_000" && contact.otherCollider.name != "Gear_001" && contact.otherCollider.name != "Gear_002")
            {

                groundedname = contact.thisCollider.name;
                groundedlocation.x = GameObject.Find(groundedname).GetComponent<Transform>().position.x;
                groundedlocation.y = GameObject.Find(groundedname).GetComponent<Transform>().position.y;
                groundedlocation.z = GameObject.Find(groundedname).GetComponent<Transform>().position.z;
                Debug.Log("New collision point " + groundedname + " at " + groundedlocation.ToString() + "\r\n"
                    + "Last footfall moved " + (modeloc-transform.position).ToString() 
                    + "Colcount = " + stateLeft.ToString() + " / " + stateRight.ToString() );

                modeloc = transform.position;   //update model position

                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = groundedlocation;
                cube.transform.localScale = new Vector3((float)0.2, (float)0.2, (float)0.2);
                cube.GetComponent<Collider>().enabled = false;
                cube.GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0);
            }
            Debug.DrawRay(contact.point, contact.normal*100, Color.red);

            state++;
            if (state > 0)
            {
                grounded = true;
            }

            if (contact.thisCollider.name == "EthanLeftFoot")
            {
                stateLeft++;
            }
            if (contact.thisCollider.name == "EthanRightFoot")
            {
                stateRight++;
            }
        }
    }


    void OnCollisionExit(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {

            state++;
            if (state > 0)
            {
                grounded = true;
            }

            if (contact.thisCollider.name == "EthanLeftFoot")
            {
                stateLeft--;
            }
            if (contact.thisCollider.name == "EthanRightFoot")
            {
                stateRight--;
            }
        }
    }

    // This is called every physics frame
    void FixedUpdate()
    {
        if (!Input.GetButton("Jump"))
        {
            if (grounded)
            {
                Vector3 currentloc = GameObject.Find(groundedname).GetComponent<Transform>().position;
                Vector3 movement = currentloc - groundedlocation;
                //Debug.Log("Moved"+movement.ToString()+" From: "+groundedlocation.ToString()+" to "+currentloc.ToString());

                transform.position -= movement;
            }
        }
    }
}