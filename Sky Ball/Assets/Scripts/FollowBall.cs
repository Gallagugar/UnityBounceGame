using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBall : MonoBehaviour {

    [SerializeField] GameObject player;
    Vector3 pos;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        pos = new Vector3(player.transform.position.x, 4.5f, player.transform.position.z - 10);
        transform.position = pos;
	}
}
