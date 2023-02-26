using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Update_Projectors : MonoBehaviour
{
    public Selected_Units Selected;

    public bool AllIntel = false;
    public bool AllRange = false;
    public bool AllAA = false;

    private Transform UnitHolder;

    private void Start()
    {
        UnitHolder = GameObject.Find("Units").transform;
        UpdateAll();
    }

    public void UpdateAll()
    {
        //TODO: Team checker
        //TODO: Options for what to show
        foreach (Transform _unit in UnitHolder)
        {
            Projectors _pro = _unit.GetComponent<Projectors>();
            if (_pro.Intel)
                _pro.Intel.enabled = Selected.isSelected(_unit) ? true : AllIntel;
            if (_pro.Range)
                _pro.Range.enabled = Selected.isSelected(_unit) ? true : AllRange;
            if (_pro.AA)
                _pro.AA.enabled = Selected.isSelected(_unit) ? true : AllAA;
        }
    }
}
