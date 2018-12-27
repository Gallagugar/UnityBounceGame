using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coin : MonoBehaviour {

    GM gm;

    void Start()
    {
        gm = FindObjectOfType<GM>();
        transform.Rotate(-90, 0, 0);
    }

    // Update is called once per frame
    void Update () {
        transform.Rotate(0, 0, 180f * Time.deltaTime);
	}

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            gm.MoneyUp();
            Destroy(gameObject);
        }
    }
}
