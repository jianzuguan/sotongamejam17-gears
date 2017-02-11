using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

public class MocapAPI : MonoBehaviour {

	// pointer to the Mocap C++ Class
	private static IntPtr MocapAPIObject;

	// datatype for skeleton bone angle
	public class myQuaternion {
		private Quaternion theQuaternion;
		public Quaternion value {
		get {
		  return theQuaternion;
		}
		set {
			theQuaternion = value;
		}
	  }
	}

	// variables used for the key quaternions
	public myQuaternion baseQuaternion = new myQuaternion();
	public myQuaternion calibrationQuaternion = new myQuaternion();
	public myQuaternion rotationQuaternion = new myQuaternion();
	public myQuaternion northQuaternion = new myQuaternion();

	// Create a new enum of the types you want to appear in the drop down menu
	public enum SensorType
	{
		Back,Chest,Head,Right_shoulder,Right_arm,Right_wrist,Right_hand,Right_thigh,Right_calf,Right_foot,Left_shoulder,Left_arm,Left_wrist,Left_hand,Left_thigh,Left_calf,Left_foot
	}

	// Create a new variable of that enum type above.
	public SensorType Sensor;

	// sensorID to bind against
	private int SensorID;

    // Constructor
    [DllImport("MocapAPI")]
	static public extern IntPtr CreateMocapInterface();
	// Destructor
	[DllImport("MocapAPI")]
	static public extern void DisposeMocapInterface(IntPtr MocapAPIObject);
	// Setup
	[DllImport("MocapAPI")]
	static public extern int SetupMocapInterface(IntPtr MocapAPIObject);
	// QuaternionFetch
	[DllImport("MocapAPI")]
	static public extern int ReadSensorWMocapInterface(IntPtr MocapAPIObject,int SensorID);
	[DllImport("MocapAPI")]
	static public extern int ReadSensorXMocapInterface(IntPtr MocapAPIObject,int SensorID);
	[DllImport("MocapAPI")]
	static public extern int ReadSensorYMocapInterface(IntPtr MocapAPIObject,int SensorID);
	[DllImport("MocapAPI")]
	static public extern int ReadSensorZMocapInterface(IntPtr MocapAPIObject,int SensorID);

	// Called once just before the Update methods are called for the first time
	void Start () {
		if (MocapAPIObject == IntPtr.Zero)					//if the class pointer is zero then it hasnt been linked to any object yet
        {
            MocapAPIObject = CreateMocapInterface();		//calls the API DLL and creates a control object
            SetupMocapInterface(MocapAPIObject);			//calls the setup functions
        }
        calibrationQuaternion.value = Quaternion.identity;	// initialise the calibrationQuaternion
		getSensorQuaternionID();							// get the sensor ID from the selected dropdown
	}

	// Called from MocapSkeleton script in order, once per frame
	public void updateBone () {
        transform.rotation = rotationQuaternion.value * getSensorQuaternion() * calibrationQuaternion.value;
    }

	// get the calibration quaternion from the T pose
	public void setNewCalibrationQuaternion(Quaternion facingQuaternion)
	{
		calibrationQuaternion.value = Quaternion.Inverse(getSensorQuaternion()) * facingQuaternion * baseQuaternion.value;
	}

	// Gets the sensor value from the sensor ID
    public Quaternion getSensorQuaternion()
    {
        float quatW = (float)ReadSensorWMocapInterface(MocapAPIObject, SensorID) / (float)(0x4000);		// fetch the quat elements
        float quatX = (float)ReadSensorXMocapInterface(MocapAPIObject, SensorID) / (float)(0x4000);
        float quatY = (float)ReadSensorYMocapInterface(MocapAPIObject, SensorID) / (float)(0x4000);
        float quatZ = (float)ReadSensorZMocapInterface(MocapAPIObject, SensorID) / (float)(0x4000);

		if ((quatW == 0) && (quatX == 0) && (quatY == 0) && (quatZ == 0))	// check if a sensor is connected
		{
			return (Quaternion.identity);
		}
		else
		{
			return (new Quaternion(quatX, -quatZ, quatY, quatW)*Quaternion.Euler(new Vector3(0,180,0)));
		}
    }

	// Set the sensor ID (long value) from the bone set in the dropdown menu
	public void getSensorQuaternionID()
	{
		switch(Sensor)
		{
			case SensorType.Back: SensorID = 1015;
				break;

			case SensorType.Chest:
				SensorID = 1016;
				break;

			case SensorType.Head:
				SensorID = 1017;
				break;

			case SensorType.Right_shoulder:
				SensorID = 1004;
				break;

			case SensorType.Right_arm:
				SensorID = 1003;
				break;

			case SensorType.Right_wrist:
				SensorID = 1002;
				break;

			case SensorType.Right_hand:
				SensorID = 1001;
				break;

			case SensorType.Right_thigh:
				SensorID = 1014;
				break;

			case SensorType.Right_calf:
				SensorID = 1013;
				break;

			case SensorType.Right_foot:
				SensorID = 1012;
				break;

			case SensorType.Left_shoulder:
				SensorID = 1008;
				break;

			case SensorType.Left_arm:
				SensorID = 1007;
				break;

			case SensorType.Left_wrist:
				SensorID = 1006;
				break;

			case SensorType.Left_hand:
				SensorID = 1005;
				break;

			case SensorType.Left_thigh:
				SensorID = 1011;
				break;

			case SensorType.Left_calf:
				SensorID = 1010;
				break;

			case SensorType.Left_foot:
				SensorID = 1009;
				break;
		}
	}
}
