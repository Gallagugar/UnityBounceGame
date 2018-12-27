using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinPreview : MonoBehaviour
{

    public void removeSkin()
    {
        if (transform.childCount > 0)
            Destroy(transform.GetChild(0).gameObject);
    }

    public void Update()
    {
        if (transform.GetChild(0) == null)
            return;

        transform.GetChild(0).Rotate(0, 60 * Time.deltaTime, 0);
    }
}
