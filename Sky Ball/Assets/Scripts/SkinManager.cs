using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour {

    [SerializeField] Transform ballPreview;
    [SerializeField] GameObject[] skins;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeSkin(int index)
    {
        PlayerPrefs.SetInt("Skin", index);
        Destroy(ballPreview.GetChild(0).gameObject);
        Instantiate(skins[index], ballPreview);
    }
}
