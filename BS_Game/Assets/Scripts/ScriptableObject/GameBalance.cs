using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace BSGame.ScriptableObject
{
    [CreateAssetMenu(fileName = "GameBalance", menuName = "_Shooter/Resources/Balance")]
    public class GameBalance : SingletonScriptableObject<GameBalance>
    {
        public int CountMonsterSpawns;
        public float SpawnTimer;
        public int MaxMonstersOnScene;
        public PlayerBalance Player;
        public List<EnemyBalance> Enemys;
        public List<WeaponBalance> Weapons;

#if UNITY_EDITOR
        [MenuItem("_Shooter/GameBalance")]
        public static void OpenGameBalance()
        {
            Selection.activeObject = Instance;
        }
#endif
    }

    [Serializable]
    public class PlayerBalance
    {
        public float MaxHP;
        public float Speed;
    }

    [Serializable]
    public class WeaponBalance
    {
        public UnitWeaponType Type;
        public Sprite ImageWeapon;
        public float Speed;
        public float Damage;
        public bool IsDefault;
        public float LifeTime;
        public float AngleSinght;
        public int NumberBullets;
    }

    [Serializable]
    public class EnemyBalance
    {
        public Sprite Image;
        public float MaxHP;
        public float Speed;
        public int CountPoint;
    }
}