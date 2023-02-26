using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit_Selector : MonoBehaviour
{
    public Selected_Units Selected;
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
                foreach (Transform unit in Selected.Get())
                {
                    NavMeshAgent _Nav = unit.GetComponent<UnitData>().Nav;
                    if (_Nav)
                        _Nav.destination = hit.point;
                }
            }
        }
    }
}
