using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData : MonoBehaviour
{
    //TODO: Impliment
    public Transform Hull;
    public float Health;
    public float Sheild;

    public void Damage(float Damage)
    {
        //TODO: Optimize damage calc
        // Rename functions so when sheild is gone only health is checked?
        // event for sheild regen ^
        //TODO: on death event
        if (Damage > Sheild)
        {
            Sheild = 0;
            Health -= Damage;
            if (Health <= 0)
                Destroy(this.gameObject);
        }
        else
        {
            Sheild -= Damage;
        }
    }
}
