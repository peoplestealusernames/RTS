using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage = 0;
    public float Speed = 20;

    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag.Contains("Unit"))
        {
            UnitCollide(col.rigidbody.gameObject);
        }
        Destroy(this.gameObject);
        //TODO: Effect
    }

    private void UnitCollide(GameObject UnitObj)
    {
        UnitData Unit = UnitObj.GetComponent<UnitData>();
        Unit.Damage(Damage);
    }
}
