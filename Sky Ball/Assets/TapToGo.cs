using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TapToGo : MonoBehaviour, IPointerDownHandler
{

    [SerializeField] GM gm;

    public void OnPointerDown(PointerEventData eventData)
    {
        gm.StartGame();
    }
}
