using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follow_Nav : MonoBehaviour
{
    public Transform Follower;
    public NavMeshAgent Nav;
    public float SmoothingT = 10f;

    private void FixedUpdate()
    {
        Vector3 _Level = Nav.transform.position;
        _Level.y = Follower.position.y;
        Follower.LookAt(_Level);

        Follower.position = Vector3.Lerp(Follower.position, _Level, 1 / SmoothingT);
    }
}
