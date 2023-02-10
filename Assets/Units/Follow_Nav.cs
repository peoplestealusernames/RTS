using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follow_Nav : MonoBehaviour
{
    public Transform Follower;
    public NavMeshAgent Nav;
    public float SmoothingT = 2f;

    public float MaxRotSpeed = 20f;

    private Vector3 vel = Vector3.zero;
    private Vector3 aVel = Vector3.zero;

    private void Start()
    {
        Nav.updatePosition = false;
        Nav.updateRotation = false;
    }

    private void FixedUpdate()
    {
        Vector3 _Level = Nav.nextPosition;
        Vector3 _Dif = Follower.transform.InverseTransformPoint(_Level);

        if (_Dif.magnitude > 1)
        {
            Quaternion _Dir = Quaternion.LookRotation(_Dif);
            Follower.rotation = Quaternion.RotateTowards(Follower.rotation, _Dir, MaxRotSpeed);
            Follower.position = Vector3.SmoothDamp(Follower.position, _Level, ref vel, SmoothingT);
        }
    }
}
