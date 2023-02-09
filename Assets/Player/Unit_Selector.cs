using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit_Selector : MonoBehaviour
{
    public Selected_Units selected;
    public Camera Cam;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //TODO: Drag
        }
        else if (Input.GetMouseButtonDown(1))
        {
            int layerMask = 1 << 0;

            RaycastHit hit;
            if (Physics.Raycast(Cam.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, layerMask))
            {
                foreach (Transform unit in selected.Get())
                {
                    NavMeshAgent _Nav = unit.GetComponent<NavMeshAgent>();
                    _Nav.destination = hit.point;
                }
            }
        }
    }
}
