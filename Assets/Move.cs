using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.position += transform.forward;
    }
}
