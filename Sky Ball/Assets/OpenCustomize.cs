using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OpenCustomize : MonoBehaviour, IPointerDownHandler
{

    [SerializeField] GM gm;

    public void OnPointerDown(PointerEventData eventData)
    {
        gm.SwitchScene("Customize");
    }
}
