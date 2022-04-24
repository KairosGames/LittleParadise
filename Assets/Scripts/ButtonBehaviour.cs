using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonBehaviour : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] AudioSource onButton;

    public void OnPointerEnter(PointerEventData eventData)
    {
        onButton.Play();
    }
}
