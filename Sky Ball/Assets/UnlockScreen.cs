using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockScreen : MonoBehaviour {

    [SerializeField] GameObject box;
    [SerializeField] SkinPreview preview;
    [SerializeField] GameObject button1;
    [SerializeField] GameObject button2;

    private void OnEnable()
    {
        ResetScreen();
    }

    public void ResetScreen()
    {
        box.SetActive(true);
        preview.removeSkin();
        preview.gameObject.SetActive(false);
        button1.gameObject.SetActive(true);
        button2.gameObject.SetActive(false);
    }
}
