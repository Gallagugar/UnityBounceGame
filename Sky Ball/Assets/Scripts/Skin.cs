using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : MonoBehaviour {

    [SerializeField] int sortingNumber;
    [SerializeField] Sprite storeImage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int GetNum()
    {
        return sortingNumber;
    }

    public Sprite GetImage()
    {
        return storeImage;
    }
}
