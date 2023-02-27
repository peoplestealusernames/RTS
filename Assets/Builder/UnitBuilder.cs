using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitInfo
{
    public string name;
    public string displayName;
    public string model;
}
[System.Serializable]
class UnitJSON
{
    public UnitInfo[] units;
}

public class UnitBuilder : MonoBehaviour
{

    void Start()
    {
        //TODO: Fetch from mods
        UnitInfo[] Units = JsonUtility.FromJson<UnitJSON>(
            Resources.Load<TextAsset>("Data/units").text
        ).units;

        for (int i = 0; i < Units.Length; i++)
        {
            GameObject _t = GameObject.Instantiate(BuildUnit(Units[i]));
            _t.transform.position = new Vector3(i * 20, 0, 0);
        }
    }

    GameObject BuildUnit(UnitInfo _Unit)
    {
        GameObject _Base = Resources.Load<GameObject>("Units/" + _Unit.name + "/" + _Unit.model);

        return _Base;
    }
}
