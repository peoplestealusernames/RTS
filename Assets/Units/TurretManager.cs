using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public TurretController Turret;
    private HashSet<Collider> targets = new HashSet<Collider>();

    //TODO: Targer pro

    public HashSet<Collider> GetColliders()
    {
        return targets;
    }

    private void OnTriggerEnter(Collider other)
    {
        Turret.UpdateTarget(other.gameObject);
        targets.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (targets.Remove(other))
        {
            Turret.UpdateTarget(null);
            //TODO: Cycle Targets
        }
    }

    //End of target gathering

    //Start of turret controls
}
