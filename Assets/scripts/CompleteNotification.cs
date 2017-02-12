using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CompleteNotification : MonoBehaviour {

    public string message;
    public Text msgbox;

    private bool displayed;

	// Use this for initialization
	void Start () {
        msgbox.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (!displayed)
        {
            Debug.Log(other);
            Debug.Log("Complete L1");
            msgbox.text = message;
            msgbox.enabled = true;
            StartCoroutine(cleanup());
            displayed = true;
        }
    }

    public IEnumerator cleanup()
    {
        yield return new WaitForSeconds(5);
        msgbox.text = "";
    }
}
