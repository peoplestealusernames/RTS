using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class UnitJSON
{
    public UnitInfo[] units;
}

public class UnitBuilder : MonoBehaviour
{
    public Transform AssetHolder;

    private void Start()
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

    private GameObject BuildUnit(UnitInfo _Unit)
    {
        //TODO: Optimize unit loading
        //TODO: Error handleing
        GameObject _Load = Resources.Load<GameObject>("Units/" + _Unit.name + "/" + _Unit.model);
        GameObject _Base = Instantiate(_Load, AssetHolder);
        _Base.name = _Unit.name;

        Transform _Hull = _Base.transform.Find(_Unit.hull);

        //Adds mesh collider to all
        AddCollidersRec(_Base, _Unit);

        //TODO: Hash for duplicates
        foreach (TurretInfo _Turret in _Unit.turrets)
            BuildTurret(_Base.transform, _Turret, _Hull);

        Rigidbody _RB = _Base.AddComponent<Rigidbody>();
        _RB.isKinematic = true;

        //TODO: No collider for certain objs


        return _Base;
    }

    private void AddCollidersRec(GameObject _Obj, UnitInfo _Unit)
    {
        foreach (Transform _child in _Obj.transform)
            AddCollidersRec(_child.gameObject, _Unit);


        if (_Unit.noCollider.Contains(_Obj.name))
            return;

        if (!_Obj.GetComponent<MeshFilter>())
            return;

        if (_Obj.GetComponent<MeshCollider>())
        {
            Debug.LogError("UnitBuild unit:" + _Unit.name + " collider:" + _Obj.name + " is duplicate");
            return;
        }

        MeshCollider _Col = _Obj.AddComponent<MeshCollider>();
        _Col.convex = true;
    }

    private void BuildTurret(Transform _Base, TurretInfo _Turret, Transform _Hull)
    {
        //TODO: more than 2 axis turret
        Transform _Hor = _Base.Find(_Turret.horizontal);
        Transform _Ele = _Base.Find(_Turret.elevator);

        _Hor.transform.parent = _Hull;
        _Ele.transform.parent = _Hor;

        if (_Turret.weaponType == "bullet")
            BulletTurret(_Base, _Turret, _Ele);
    }

    private void BulletTurret(Transform _Base, TurretInfo _Turret, Transform _Ele)
    {
        //TODO: MultiBullet/Barrel turret
        Transform _Bullet = _Base.Find(_Turret.bullet.name);

        _Bullet.SetParent(_Ele);
        _Bullet.gameObject.SetActive(false);

        BulletBuilder(_Base, _Turret.bullet, _Bullet.gameObject);
    }

    private void BulletBuilder(Transform _Base, BulletInfo _BulletInfo, GameObject _Bullet)
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
