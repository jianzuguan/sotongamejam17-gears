using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MocapSkeleton : MonoBehaviour {

    //Main core
    public GameObject Pelvis;
    public GameObject Spine1;
    public GameObject Spine2;
    public GameObject Chest;
    public GameObject Head;

    //R arm
    public GameObject Rhand;
    public GameObject Rforearm;
    public GameObject Rforetwist;
    public GameObject Rforetwist1;
    public GameObject Rupperarm;
    public GameObject Rclavicle;

    //R leg
    public GameObject RThigh;
    public GameObject RCalf;
    public GameObject RFoot;

    //L arm
    public GameObject Lhand;
    public GameObject Lforearm;
    public GameObject Lforetwist;
    public GameObject Lforetwist1;
    public GameObject Lupperarm;
    public GameObject Lclavicle;

    //L leg
    public GameObject LThigh;
    public GameObject LCalf;
    public GameObject LFoot;

	//The model rotations file
	public TextAsset modelFileName;

	private float lastModelRotation;

    Dictionary<string, Quaternion> Offsets;

	// Tells us whether the T pose sequence is running
    bool TposeRunning;

	// Use this for initialization
	void Start () {
		OffsetsFromFile();
    }

	// Update is called once per frame
	void Update () {

		updateModelRotation();

        if (Input.GetButtonDown("Tpose"))						// if the button is pressed
        {
            if(!TposeRunning)									// if T pose not already running
            {
                StartCoroutine(setNewCalibrationQuaternionsDelay());	// start a coroutine (a coroutine can suspect execution
            }
        }
        if (Input.GetButtonDown("TposeQuick"))
        {
            if (!TposeRunning)
            {
                setNewCalibrationQuaternions();
            }
        }

		updateBonePositions();
    }

	// Update all bone positions
    private void updateBonePositions()
	{
		Pelvis.GetComponent<MocapAPI>().updateBone();
        Spine1.GetComponent<MocapAPI>().updateBone();
        Spine2.GetComponent<MocapAPI>().updateBone();
        Chest.GetComponent<MocapAPI>().updateBone();
        Head.GetComponent<MocapAPI>().updateBone();

        Rclavicle.GetComponent<MocapAPI>().updateBone();
        Rupperarm.GetComponent<MocapAPI>().updateBone();
        Rforearm.GetComponent<MocapAPI>().updateBone();
        Rforetwist.GetComponent<MocapAPI>().updateBone();
        Rforetwist1.GetComponent<MocapAPI>().updateBone();
        Rhand.GetComponent<MocapAPI>().updateBone();

        RThigh.GetComponent<MocapAPI>().updateBone();
        RCalf.GetComponent<MocapAPI>().updateBone();
        RFoot.GetComponent<MocapAPI>().updateBone();

        Lclavicle.GetComponent<MocapAPI>().updateBone();
        Lupperarm.GetComponent<MocapAPI>().updateBone();
        Lforearm.GetComponent<MocapAPI>().updateBone();
        Lforetwist.GetComponent<MocapAPI>().updateBone();
        Lforetwist1.GetComponent<MocapAPI>().updateBone();
        Lhand.GetComponent<MocapAPI>().updateBone();

        LThigh.GetComponent<MocapAPI>().updateBone();
        LCalf.GetComponent<MocapAPI>().updateBone();
        LFoot.GetComponent<MocapAPI>().updateBone();
	}

	// Update all rotations from the arrow key changes
	private void updateModelRotation()
	{
		float newModelRotation;

		newModelRotation = GameObject.Find("ModelRotation").GetComponent<Transform>().eulerAngles.y;

		if (newModelRotation != lastModelRotation)
		{
			lastModelRotation = newModelRotation;

			Pelvis.GetComponent<MocapAPI>().rotationQuaternion.value = Quaternion.Euler (new Vector3 (0, newModelRotation, 0));
			Spine1.GetComponent<MocapAPI>().rotationQuaternion.value = Quaternion.Euler (new Vector3 (0, newModelRotation, 0));
			Spine2.GetComponent<MocapAPI>().rotationQuaternion.value = Quaternion.Euler (new Vector3 (0, newModelRotation, 0));
			Chest.GetComponent<MocapAPI>().rotationQuaternion.value = Quaternion.Euler (new Vector3 (0, newModelRotation, 0));
			Head.GetComponent<MocapAPI>().rotationQuaternion.value = Quaternion.Euler (new Vector3 (0, newModelRotation, 0));

			Rclavicle.GetComponent<MocapAPI>().rotationQuaternion.value = Quaternion.Euler (new Vector3 (0, newModelRotation, 0));
			Rupperarm.GetComponent<MocapAPI>().rotationQuaternion.value = Quaternion.Euler (new Vector3 (0, newModelRotation, 0));
			Rforearm.GetComponent<MocapAPI>().rotationQuaternion.value = Quaternion.Euler (new Vector3 (0, newModelRotation, 0));
            if (Rforetwist != null)
			    Rforetwist.GetComponent<MocapAPI>().rotationQuaternion.value = Quaternion.Euler (new Vector3 (0, newModelRotation, 0));
            if (Rforetwist1 != null)
			    Rforetwist1.GetComponent<MocapAPI>().rotationQuaternion.value = Quaternion.Euler (new Vector3 (0, newModelRotation, 0));
			Rhand.GetComponent<MocapAPI>().rotationQuaternion.value = Quaternion.Euler (new Vector3 (0, newModelRotation, 0));

			RThigh.GetComponent<MocapAPI>().rotationQuaternion.value = Quaternion.Euler (new Vector3 (0, newModelRotation, 0));
			RCalf.GetComponent<MocapAPI>().rotationQuaternion.value = Quaternion.Euler (new Vector3 (0, newModelRotation, 0));
			RFoot.GetComponent<MocapAPI>().rotationQuaternion.value = Quaternion.Euler (new Vector3 (0, newModelRotation, 0));

			Lclavicle.GetComponent<MocapAPI>().rotationQuaternion.value = Quaternion.Euler (new Vector3 (0, newModelRotation, 0));
			Lupperarm.GetComponent<MocapAPI>().rotationQuaternion.value = Quaternion.Euler (new Vector3 (0, newModelRotation, 0));
			Lforearm.GetComponent<MocapAPI>().rotationQuaternion.value = Quaternion.Euler (new Vector3 (0, newModelRotation, 0));
            if (Lforetwist != null)
			    Lforetwist.GetComponent<MocapAPI>().rotationQuaternion.value = Quaternion.Euler (new Vector3 (0, newModelRotation, 0));
            if (Lforetwist1 != null)
			    Lforetwist1.GetComponent<MocapAPI>().rotationQuaternion.value = Quaternion.Euler (new Vector3 (0, newModelRotation, 0));
			Lhand.GetComponent<MocapAPI>().rotationQuaternion.value = Quaternion.Euler (new Vector3 (0, newModelRotation, 0));

			LThigh.GetComponent<MocapAPI>().rotationQuaternion.value = Quaternion.Euler (new Vector3 (0, newModelRotation, 0));
			LCalf.GetComponent<MocapAPI>().rotationQuaternion.value = Quaternion.Euler (new Vector3 (0, newModelRotation, 0));
			LFoot.GetComponent<MocapAPI>().rotationQuaternion.value = Quaternion.Euler (new Vector3 (0, newModelRotation, 0));
		}
	}

	// Reset all the T pose positions
    private IEnumerator setNewCalibrationQuaternionsDelay()
    {
        TposeRunning = true;										// make sure this class knows T pose is running

        yield return new WaitForSeconds(5);

        setNewCalibrationQuaternions();
    }

    private void setNewCalibrationQuaternions()
    {
		// halt this function for 5 seconds
        Quaternion referenceQuaternion = Quaternion.identity;

		referenceQuaternion = Quaternion.Euler (new Vector3 (0, Chest.GetComponent<MocapAPI>().getSensorQuaternion().eulerAngles.y + 90, 0 ));
		GameObject.Find("ModelRotation").GetComponent<ModelOrientation>().Offset(Quaternion.Inverse(referenceQuaternion));

        Pelvis.GetComponent<MocapAPI>().setNewCalibrationQuaternion(referenceQuaternion);
		Spine1.GetComponent<MocapAPI>().setNewCalibrationQuaternion(referenceQuaternion);
		Spine2.GetComponent<MocapAPI>().setNewCalibrationQuaternion(referenceQuaternion);
		Chest.GetComponent<MocapAPI>().setNewCalibrationQuaternion(referenceQuaternion);
		Head.GetComponent<MocapAPI>().setNewCalibrationQuaternion(referenceQuaternion);

		Rclavicle.GetComponent<MocapAPI>().setNewCalibrationQuaternion(referenceQuaternion);
		Rupperarm.GetComponent<MocapAPI>().setNewCalibrationQuaternion(referenceQuaternion);
		Rforearm.GetComponent<MocapAPI>().setNewCalibrationQuaternion(referenceQuaternion);
		Rforetwist.GetComponent<MocapAPI>().setNewCalibrationQuaternion(referenceQuaternion);
		Rforetwist1.GetComponent<MocapAPI>().setNewCalibrationQuaternion(referenceQuaternion);
		Rhand.GetComponent<MocapAPI>().setNewCalibrationQuaternion(referenceQuaternion);

		RThigh.GetComponent<MocapAPI>().setNewCalibrationQuaternion(referenceQuaternion);
		RCalf.GetComponent<MocapAPI>().setNewCalibrationQuaternion(referenceQuaternion);
		RFoot.GetComponent<MocapAPI>().setNewCalibrationQuaternion(referenceQuaternion);

		Lclavicle.GetComponent<MocapAPI>().setNewCalibrationQuaternion(referenceQuaternion);
		Lupperarm.GetComponent<MocapAPI>().setNewCalibrationQuaternion(referenceQuaternion);
		Lforearm.GetComponent<MocapAPI>().setNewCalibrationQuaternion(referenceQuaternion);
		Lforetwist.GetComponent<MocapAPI>().setNewCalibrationQuaternion(referenceQuaternion);
		Lforetwist1.GetComponent<MocapAPI>().setNewCalibrationQuaternion(referenceQuaternion);
		Lhand.GetComponent<MocapAPI>().setNewCalibrationQuaternion(referenceQuaternion);

		LThigh.GetComponent<MocapAPI>().setNewCalibrationQuaternion(referenceQuaternion);
		LCalf.GetComponent<MocapAPI>().setNewCalibrationQuaternion(referenceQuaternion);
		LFoot.GetComponent<MocapAPI>().setNewCalibrationQuaternion(referenceQuaternion);

        TposeRunning = false;										// T pose has finished
    }

	// Load all the offsets from the file
	private void OffsetsFromFile () {
		var arrayString = modelFileName.text.Split('\n');
		foreach (var line in arrayString)
		{
			var fields = line.Split(',');
			switch(fields[0])
			{
				case "Pelvis":
					Pelvis.GetComponent<MocapAPI>().baseQuaternion.value = Quaternion.Euler (new Vector3 (int.Parse(fields[1].Trim()), int.Parse(fields[2].Trim()), int.Parse(fields[3].Trim())));
					break;
				case "Chest":
					Spine1.GetComponent<MocapAPI>().baseQuaternion.value = Quaternion.Euler (new Vector3 (int.Parse(fields[1].Trim()), int.Parse(fields[2].Trim()), int.Parse(fields[3].Trim())));
					Spine2.GetComponent<MocapAPI>().baseQuaternion.value = Quaternion.Euler (new Vector3 (int.Parse(fields[1].Trim()), int.Parse(fields[2].Trim()), int.Parse(fields[3].Trim())));
					Chest.GetComponent<MocapAPI>().baseQuaternion.value = Quaternion.Euler (new Vector3 (int.Parse(fields[1].Trim()), int.Parse(fields[2].Trim()), int.Parse(fields[3].Trim())));
					break;
				case "Head":
					Head.GetComponent<MocapAPI>().baseQuaternion.value = Quaternion.Euler (new Vector3 (int.Parse(fields[1].Trim()), int.Parse(fields[2].Trim()), int.Parse(fields[3].Trim())));
					break;
				case "Rclavicle":
					Rclavicle.GetComponent<MocapAPI>().baseQuaternion.value = Quaternion.Euler (new Vector3 (int.Parse(fields[1].Trim()), int.Parse(fields[2].Trim()), int.Parse(fields[3].Trim())));
					break;
				case "Rupperarm":
					Rupperarm.GetComponent<MocapAPI>().baseQuaternion.value = Quaternion.Euler (new Vector3 (int.Parse(fields[1].Trim()), int.Parse(fields[2].Trim()), int.Parse(fields[3].Trim())));
					break;
				case "Rforearm":
					Rforearm.GetComponent<MocapAPI>().baseQuaternion.value = Quaternion.Euler (new Vector3 (int.Parse(fields[1].Trim()), int.Parse(fields[2].Trim()), int.Parse(fields[3].Trim())));
					Rforetwist.GetComponent<MocapAPI>().baseQuaternion.value = Quaternion.Euler (new Vector3 (int.Parse(fields[1].Trim()), int.Parse(fields[2].Trim()), int.Parse(fields[3].Trim())));
					Rforetwist1.GetComponent<MocapAPI>().baseQuaternion.value = Quaternion.Euler (new Vector3 (int.Parse(fields[1].Trim()), int.Parse(fields[2].Trim()), int.Parse(fields[3].Trim())));
					break;
				case "Rhand":
					Rhand.GetComponent<MocapAPI>().baseQuaternion.value = Quaternion.Euler (new Vector3 (int.Parse(fields[1].Trim()), int.Parse(fields[2].Trim()), int.Parse(fields[3].Trim())));
					break;
				case "RThigh":
					RThigh.GetComponent<MocapAPI>().baseQuaternion.value = Quaternion.Euler (new Vector3 (int.Parse(fields[1].Trim()), int.Parse(fields[2].Trim()), int.Parse(fields[3].Trim())));
					break;
				case "RCalf":
					RCalf.GetComponent<MocapAPI>().baseQuaternion.value = Quaternion.Euler (new Vector3 (int.Parse(fields[1].Trim()), int.Parse(fields[2].Trim()), int.Parse(fields[3].Trim())));
					break;
				case "RFoot":
					RFoot.GetComponent<MocapAPI>().baseQuaternion.value = Quaternion.Euler (new Vector3 (int.Parse(fields[1].Trim()), int.Parse(fields[2].Trim()), int.Parse(fields[3].Trim())));
					break;
				case "Lclavicle":
					Lclavicle.GetComponent<MocapAPI>().baseQuaternion.value = Quaternion.Euler (new Vector3 (int.Parse(fields[1].Trim()), int.Parse(fields[2].Trim()), int.Parse(fields[3].Trim())));
					break;
				case "Lupperarm":
					Lupperarm.GetComponent<MocapAPI>().baseQuaternion.value = Quaternion.Euler (new Vector3 (int.Parse(fields[1].Trim()), int.Parse(fields[2].Trim()), int.Parse(fields[3].Trim())));
					break;
				case "Lforearm":
					Lforearm.GetComponent<MocapAPI>().baseQuaternion.value = Quaternion.Euler (new Vector3 (int.Parse(fields[1].Trim()), int.Parse(fields[2].Trim()), int.Parse(fields[3].Trim())));
					Lforetwist.GetComponent<MocapAPI>().baseQuaternion.value = Quaternion.Euler (new Vector3 (int.Parse(fields[1].Trim()), int.Parse(fields[2].Trim()), int.Parse(fields[3].Trim())));
					Lforetwist1.GetComponent<MocapAPI>().baseQuaternion.value = Quaternion.Euler (new Vector3 (int.Parse(fields[1].Trim()), int.Parse(fields[2].Trim()), int.Parse(fields[3].Trim())));
					break;
				case "Lhand":
					Lhand.GetComponent<MocapAPI>().baseQuaternion.value = Quaternion.Euler (new Vector3 (int.Parse(fields[1].Trim()), int.Parse(fields[2].Trim()), int.Parse(fields[3].Trim())));
					break;
				case "LThigh":
					LThigh.GetComponent<MocapAPI>().baseQuaternion.value = Quaternion.Euler (new Vector3 (int.Parse(fields[1].Trim()), int.Parse(fields[2].Trim()), int.Parse(fields[3].Trim())));
					break;
				case "LCalf":
					LCalf.GetComponent<MocapAPI>().baseQuaternion.value = Quaternion.Euler (new Vector3 (int.Parse(fields[1].Trim()), int.Parse(fields[2].Trim()), int.Parse(fields[3].Trim())));
					break;
				case "LFoot":
					LFoot.GetComponent<MocapAPI>().baseQuaternion.value = Quaternion.Euler (new Vector3 (int.Parse(fields[1].Trim()), int.Parse(fields[2].Trim()), int.Parse(fields[3].Trim())));
					break;
				default :
					break;
			}
		}

	}

}
