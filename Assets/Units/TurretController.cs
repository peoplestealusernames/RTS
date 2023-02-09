using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurretController : MonoBehaviour
{
    public Transform Elevator;
    public Transform Base;


    public float TurnSpeed = 2;
    public float ReloadTime = 2;

    public UnityEvent Fire = new UnityEvent();

    private GameObject Target;
    private Transform TargetHull;
    private UnitData TargetData;
    public void UpdateTarget(GameObject NewTarget)
    {
        if (NewTarget)
        {
            TargetData = NewTarget.GetComponent<UnitData>();
            TargetHull = TargetData.Hull;
            Target = NewTarget;
        }
        else
        {
            TargetData = null;
            TargetHull = null;
            Target = null;
        }
        //TODO: Optimize to remove if target in fixed update
    }

    void FixedUpdate()
    {
        if (Target)
            LookAtTarget();
        else
            Zero();
    }

    void Zero()
    {
        //TODO: Optimize to run once
        Base.localRotation = Quaternion.Euler(0, 0, 0);
        Elevator.localRotation = Quaternion.Euler(0, 0, 0);
    }

    void LookAtTarget()
    {
        //TODO: Change Base to gun barrel
        Vector3 pos = TargetHull.transform.position;
        pos.y = Base.transform.position.y;
        Base.LookAt(pos);
        Elevator.LookAt(TargetHull.transform.position);

        FireCheck();
    }

    private float NextShot = -1;
    void FireCheck()
    {
        //TODO: Optimize
        if (Time.unscaledTime >= NextShot)
        {
            NextShot = Time.unscaledTime + ReloadTime;
            Fire.Invoke();
        }
    }
}
