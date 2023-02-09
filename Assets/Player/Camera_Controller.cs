using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public Transform cameraTransform;

    public float smoothTime = 0.2f;
    public float moveSpeed = 20;
    public float scrollSpeed = 300;

    public float sens = 2;
    public float sensSmooth = 0.25f;

    private Vector3 vel = Vector3.zero;

    void Update()
    {
        UpdateMotion();
        //UpdateRotation();
    }

    void UpdateMotion()
    {
        Vector3 motion = new Vector3(
            Input.GetAxisRaw("Horizontal"),
            -Input.GetAxisRaw("Scroll") * scrollSpeed,
            Input.GetAxisRaw("Vertical")
        );
        Vector3 target = transform.TransformPoint(motion * moveSpeed);
        transform.position = Vector3.SmoothDamp(transform.position, target, ref vel, smoothTime);
    }

    void UpdateRotation()
    {
        Vector3 current = cameraTransform.rotation.eulerAngles;

        Vector3 rotation = new Vector3(
            Input.GetAxisRaw("Mouse Y"),
            0,
            Input.GetAxisRaw("Mouse X")
        );

        Quaternion target = Quaternion.Euler(current + rotation * sens * 1000);

        // Dampen towards the target rotation
        cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, target, Time.deltaTime * sensSmooth);
    }
}
