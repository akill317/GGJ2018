using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
	public SerialHandler serialHandler;
	
	// Use this for initialization
	void Start () {
		serialHandler.OnDataReceived += OnDataReceived;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDataReceived(string message)
    {
        var data = message.Split(new string[]{"\t"}, System.StringSplitOptions.None);
        
		if (data.Length < 4) { return; }

        try {
			Debug.Log(data[0] + ", " + data[1] + ", " + data[2] + ", " + data[3]);
        } 
		catch (System.Exception e) {
            Debug.LogWarning(e.Message);
        }
    }
}
