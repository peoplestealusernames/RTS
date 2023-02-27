using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public string weaponType;
    public BulletInfo bullet;
}

[System.Serializable]
public class BulletInfo
{
    public string name;
    public float damage;
    public float speed;
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
            BuildTurret(_Base.transform, _Turret, _Hull);

        return _Base;
    }

    void BuildTurret(Transform _Base, TurretInfo _Turret, Transform _Hull)
    {
        //TODO: more than 2 axis turret
        Transform _Hor = _Base.Find(_Turret.horizontal);
        Transform _Ele = _Base.Find(_Turret.elevator);

        _Hor.transform.parent = _Hull;
        _Ele.transform.parent = _Hor;

        if (_Turret.weaponType == "bullet")
            BulletTurret(_Base, _Turret, _Ele);
    }

    void BulletTurret(Transform _Base, TurretInfo _Turret, Transform _Ele)
    {
        //TODO: MultiBullet/Barrel turret
        Transform _Bullet = _Base.Find(_Turret.bullet.name);

        _Bullet.SetParent(_Ele);
        _Bullet.gameObject.SetActive(false);

        BulletBuilder(_Base, _Turret.bullet, _Bullet.gameObject);
    }

    void BulletBuilder(Transform _Base, BulletInfo _BulletInfo, GameObject _Bullet)
    {
        //TODO: Bullet Config
        Rigidbody _RB = _Bullet.AddComponent<Rigidbody>();
        _RB.mass = 20;

        MeshCollider _Col = _Bullet.AddComponent<MeshCollider>();
        _Col.convex = true;

        Bullet _script = _Bullet.AddComponent<Bullet>();
        _script.Damage = _BulletInfo.damage;
        _script.Speed = _BulletInfo.speed;
    }
}
