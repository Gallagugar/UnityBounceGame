using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{

    Transform locked, selected;

    [SerializeField] string skinName;
    [SerializeField] int num;
    [SerializeField] GM gm;

    // Use this for initialization
    void Start()
    {
        locked = transform.FindChild("Locked");
        selected = transform.FindChild("Selected");

        if (PlayerPrefs.GetInt(skinName) == 1)
            locked.gameObject.SetActive(false);
        else
            locked.gameObject.SetActive(true);

        if (PlayerPrefs.GetString("Skin") == skinName)
        {
            selected.gameObject.SetActive(true);
            gm.currentSkinButton = this;
        }
        else
            selected.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (locked == null)
            return;

        if (PlayerPrefs.GetInt(skinName) == 1)
            locked.gameObject.SetActive(false);
        else
            locked.gameObject.SetActive(true);

        if (PlayerPrefs.GetString("Skin") == skinName)
        {
            selected.gameObject.SetActive(true);
            gm.currentSkinButton = this;
        }
        else
            selected.gameObject.SetActive(false);
    }

    public int GetNum()
    {
        return num;
    }

    public void Select()
    {
        selected.gameObject.SetActive(true);
    }

    public void Deselect()
    {
        selected.gameObject.SetActive(false);
    }

    /*public void SetUp()
    {
        if (PlayerPrefs.GetInt(skinName) == 1)
            locked.gameObject.SetActive(false);
        else
            locked.gameObject.SetActive(true);

        if (PlayerPrefs.GetInt("Skin") == num)
            selected.gameObject.SetActive(true);
        else
            selected.gameObject.SetActive(false);

        RectTransform rect = GetComponent<RectTransform>();
        float y = -70;
        float x = 0;
        float anc = .5f;
        int temp = num;
        while (temp >= 3)
        {
            temp -= 3;
            y -= -120;
        }
        if (temp == 0)
        {
            x = -25;
            anc = .25f;
        }
        else if (temp == 2)
        {
            x = 25;
            anc = .75f;
        }

        rect.anchorMin.Set(anc, 1);
        rect.anchorMax.Set(anc, 1);
        rect.localPosition.Set(x, y, 0);
    }*/

}
