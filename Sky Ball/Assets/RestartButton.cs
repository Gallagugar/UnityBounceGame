using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RestartButton : MonoBehaviour, IPointerDownHandler
{

    [SerializeField] GM gm;

    public void OnPointerDown(PointerEventData eventData)
    {
        gm.ResetGame();
    }
}
