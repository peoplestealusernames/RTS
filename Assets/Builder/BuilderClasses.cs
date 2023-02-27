using System.Collections.Generic;

[System.Serializable]
public class UnitInfo
{
    public string name;
    public string displayName;

    public string model;
    public List<string> noCollider = new List<string>();
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