using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class DamagePopUpGenerator : MonoBehaviour
{
    public static DamagePopUpGenerator current;
    public GameObject prefab;
    private Camera cam;
    
    private void Awake()
    {
        current = this;
        cam = Camera.main;
    }

    void Update()
    {
        transform.forward = cam.transform.forward;
    }

    public void CreatePopUp(Vector3 position, string text,Color color)
    {
        var popup = Instantiate(prefab, position, Quaternion.identity);
        var temp = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        temp.text = text;
        temp.faceColor = color;
        
        Destroy(popup,1f);
    }
}
