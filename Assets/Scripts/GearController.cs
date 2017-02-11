using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearController : MonoBehaviour {

    private float xAngle = 0;
    private float yAngle = 0;
    private float zAngle = 0;

    [Header("Percent of speed in axis")]
    [Range(0.0f, 1.0f)]
    public float x = 0;
    [Range(0.0f, 1.0f)]
    public float y = 0;
    [Range(0.0f, 1.0f)]
    public float z = 0;

    [Header("Direction")]
    public bool xAnticlockwise = false;
    public bool yAnticlockwise = false;
    public bool zAnticlockwise = false;

    [Header("Speed")]
    public float speed = 1;
    public float xTiltAngle = 5;
    public float yTiltAngle = 5;
    public float zTiltAngle = 5;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        xTiltAngle = xAnticlockwise && xTiltAngle > 0 ? -xTiltAngle : xTiltAngle;
        yTiltAngle = yAnticlockwise && yTiltAngle > 0 ? -yTiltAngle : yTiltAngle;
        zTiltAngle = zAnticlockwise && zTiltAngle > 0 ? -zTiltAngle : zTiltAngle;


        Vector3 target = new Vector3(
            x * speed * xTiltAngle * Time.deltaTime,
            y * speed * yTiltAngle * Time.deltaTime,
            z * speed * zTiltAngle * Time.deltaTime);
        transform.Rotate(target);
    }

}
