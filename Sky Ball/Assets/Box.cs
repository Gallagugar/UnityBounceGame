using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

    private void OnDisable()
    {
        gameObject.SetActive(true);
    }
}
