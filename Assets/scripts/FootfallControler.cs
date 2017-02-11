using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootfallControler : MonoBehaviour {

    private bool grounded = false;
    private int state = 0;
    private int stateLeft;
    private int stateRight;

    //control options
    public bool useDebugCubes;
    public bool useLowestFootMode;

    private Vector3 groundedlocation;
    private string groundedname;

    private string lastLowest;
    private Vector3 lastlowestpoint;

    private Vector3 modeloc;

    public GameObject badEndUI;


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

            if (contact.thisCollider.name != groundedname && 
                (contact.thisCollider.name == "EthanLeftFoot" | contact.thisCollider.name == "EthanRightFoot") &&
                contact.otherCollider.name != "Gear_000" && contact.otherCollider.name != "Gear_001" && contact.otherCollider.name != "Gear_002")
            {

                groundedname = contact.thisCollider.name;
                groundedlocation.x = GameObject.Find(groundedname).GetComponent<Transform>().position.x;
                groundedlocation.y = GameObject.Find(groundedname).GetComponent<Transform>().position.y;
                groundedlocation.z = GameObject.Find(groundedname).GetComponent<Transform>().position.z;
                Debug.Log("New collision point " + groundedname + " at " + groundedlocation.ToString() + "\r\n"
                    + "Last footfall moved " + (modeloc-transform.position).ToString() 
                    + "Colcount = " + stateLeft.ToString() + " / " + stateRight.ToString() );

                modeloc = transform.position;   //update model position

                if (useDebugCubes)
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = groundedlocation;
                    cube.transform.localScale = new Vector3((float)0.2, (float)0.2, (float)0.2);
                    cube.GetComponent<Collider>().enabled = false;
                    cube.GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0);
                }
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
        if(useLowestFootMode)
        {
            //if left foot lower
            if (GameObject.Find("EthanLeftFoot").GetComponent<Transform>().position.y < GameObject.Find("EthanRightFoot").GetComponent<Transform>().position.y)
            {
                if (lastLowest != "EthanLeftFoot")
                {
                    lastLowest = "EthanLeftFoot";
                    lastlowestpoint = GameObject.Find("EthanLeftFoot").GetComponent<Transform>().position;
                }
            }
            else //right foot lower
            {
                if (lastLowest != "EthanRightFoot")
                {
                    lastLowest = "EthanRightFoot";
                    lastlowestpoint = GameObject.Find("EthanRightFoot").GetComponent<Transform>().position;
                }
            }
        }

        if (!Input.GetButton("Jump"))
        {
            if(useLowestFootMode)
            {
            Vector3 currentloc = GameObject.Find(lastLowest).GetComponent<Transform>().position;
            Vector3 movement = currentloc - lastlowestpoint;
            //Debug.Log("Moved"+movement.ToString()+" From: "+groundedlocation.ToString()+" to "+currentloc.ToString());

            movement.y = 0;
            transform.position -= movement;
            }
            else if (grounded)
            {
                Vector3 currentloc = GameObject.Find(groundedname).GetComponent<Transform>().position;
                Vector3 movement = currentloc - groundedlocation;
                //Debug.Log("Moved"+movement.ToString()+" From: "+groundedlocation.ToString()+" to "+currentloc.ToString());

                movement.y = 0;
                transform.position -= movement;
            }
        }

        groundedlocation += transform.forward*(Input.GetAxis("Vertical") * Time.fixedDeltaTime);

        if(Input.GetButton("Reset"))
        {
            transform.position = new Vector3((float)-1.1, (float)-22, (float)-1.1);
            clearGrounded();
            this.badEndUI.SetActive(false);
        }
    }

    public void clearGrounded()
    {
        this.state = 0;
        this.grounded = false;
        lastLowest = "EthanRightFoot";
        lastlowestpoint = GameObject.Find("EthanRightFoot").GetComponent<Transform>().position;
    }
}