using UnityEngine;

namespace BSGame
{
    public enum UnitType
    {
        None = 0,
        Player = 1,
        Enemy = 2
    }

    public enum EnemyParam
    {
        None = 0,
        Points = 1
    }
    
    public enum UnitParam
    {
        None = 0,
        MaxHP = 1,
        CurrentHP = 2,
        Speed = 3,
        CountPoint = 4,
        HitDelay = 5
    }

    public enum WeaponParam
    {
        None = 0, 
        Type = 1,
        Speed = 2,
        Damage = 3
    }

    public enum UnitWeaponType
    {
        None = 0,
        Pistol = 1,
        Shotgun = 2,
        AutoMachine = 3
    }
}
