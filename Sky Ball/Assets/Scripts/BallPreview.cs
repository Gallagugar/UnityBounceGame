using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPreview : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 180f * Time.deltaTime, 0);
	}
}
