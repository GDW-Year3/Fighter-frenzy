using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIAnimations : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 hoverSize = new Vector3(1.2f, 1.2f, 1.2f);
    public Vector3 normSize = Vector3.one;
    public float aniTime = 0.5f;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //Scale button to 1.2x size
        transform.LeanScale(hoverSize,aniTime).setEaseOutQuart();
        //Debug
        Debug.Log("The cursor entered the selectable UI element.");
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Scale button to normal size
        transform.LeanScale(normSize, aniTime).setEaseOutQuart();
        //Debug
        Debug.Log("The cursor exited the selectable UI element.");
    }
}
