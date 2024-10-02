using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float height =5.95f;
    [SerializeField] private float distance = 6.11f;
    
    public float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private Transform target;

    // Update is called once per frame
    void Update()
    {
        
        Vector3 targetPosition = new Vector3(target.position.x,height,target.position.z -distance);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        
    }
}
