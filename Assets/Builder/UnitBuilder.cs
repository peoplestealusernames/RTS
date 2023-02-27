using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class UnitInfo
{
    public string name;
    public string displayName;

    public string model;
    public string hull;

    public TurretInfo[] turrets;
}

[System.Serializable]
public class TurretInfo
{
    public string name;
    public string horizontal;
    public string elevator;
}

[System.Serializable]
class UnitJSON
{
    public UnitInfo[] units;
}

public class UnitBuilder : MonoBehaviour
{
    public Transform AssetHolder;

    void Start()
    {
        //TODO: Fetch from mods
        UnitInfo[] Units = JsonUtility.FromJson<UnitJSON>(
            Resources.Load<TextAsset>("Data/units").text
        ).units;

        //TODO: Hash for dupes/mods
        for (int i = 0; i < Units.Length; i++)
        {
            GameObject _t = GameObject.Instantiate(BuildUnit(Units[i]));
            _t.transform.position = new Vector3(i * 20, 0, 0);
        }
    }

    GameObject BuildUnit(UnitInfo _Unit)
    {
        //TODO: Optimize unit loading
        //TODO: Error handleing
        GameObject _Load = Resources.Load<GameObject>("Units/" + _Unit.name + "/" + _Unit.model);
        GameObject _Base = Instantiate(_Load, AssetHolder);
        _Base.name = _Unit.name;

        Transform _Hull = _Base.transform.Find(_Unit.hull);

        //TODO: Hash for duplicates
        foreach (TurretInfo _Turret in _Unit.turrets)
        {
            //TODO: more than 2 axis turret
            Transform _Hor = _Base.transform.Find(_Turret.horizontal);
            Transform _Ele = _Base.transform.Find(_Turret.elevator);

            _Hor.transform.parent = _Hull;
            _Ele.transform.parent = _Hor;
        }

        return _Base;
    }
}
