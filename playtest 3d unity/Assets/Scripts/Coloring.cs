using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Coloring : MonoBehaviour, IPointerDownHandler
{
    public GameObject Body;
    public GameObject Legs;
    public Material Material;

    public void OnPointerDown(PointerEventData eventData)
    {
        Body.GetComponent<Renderer>().material.color = Material.color;
        Legs.GetComponent<Renderer>().material.color = Material.color;
    }
}
