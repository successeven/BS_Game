using System;
using BSGame.Components.Weapons;
using BSGame.Manager;
using BSGame.ScriptableObject;
using BSGame.Tools;
using BSGame.View;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BSGame.Model.Units
{
    public class UnitModel : ModelWithView<UnitView>
    {
        public event Action<Vector2, float> OnMove;
        public UnitType UnitType { get; }
        public Vector2 CurrentDirection { get; private set; }
        public Vector2 Position => _position;
        public Quaternion Rotation => _rotation;

        private Vector2 _position;
        private Quaternion _rotation;
        private BaseShootingComponent _currentWeaponComponent;
        private float _hitDelayTimer;

        public UnitModel(UnitType unitType)
        {
            UnitType = unitType;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            if (View != null)
                View.UpdateMe(deltaTime);
        }

        public UnitModel SetCurrentWeapon(UnitWeaponType weaponType)
        {
            if (_currentWeaponComponent != null)
                this.RemoveComponent(_currentWeaponComponent);

            switch (weaponType)
            {
                case UnitWeaponType.None:
                    break;
                case UnitWeaponType.Pistol:
                    _currentWeaponComponent = this.AddComponent<PistolShootingComponent>();
                    break;
                case UnitWeaponType.Shotgun:
                    _currentWeaponComponent = this.AddComponent<ShotgunShootingComponent>();
                    break;
                case UnitWeaponType.AutoMachine:
                    _currentWeaponComponent = this.AddComponent<AutoMachineShootingComponent>();
                    break;
            }
            _currentWeaponComponent?.ChangeGun();

            return this;
        }

        public UnitModel DestroyView()
        {
            Disable();
            View.DestroyGO();
            return this;
        }

        public override void FixedUpdate(float deltaTime)
        {
            base.FixedUpdate(deltaTime);
            if (View != null)
                View.FixedUpdateMe(deltaTime);
        }

        public UnitModel InitViewModel(Vector2 spawnPos)
        {
            UnitView unit;
            switch (UnitType)
            {
                case UnitType.Player:
                    unit = Resources.Load<UnitView>("Prefabs/Player");
                    View = Object.Instantiate(unit, MainApp.RootTransform, false)
                        .SetModel(this);
                    break;
                case UnitType.Enemy:
                    unit = Resources.Load<UnitView>("Prefabs/Enemy");
                    View = Object.Instantiate(unit, MainApp.RootTransform, false)
                        .SetModel(this);

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            View.transform.position = _position = spawnPos;


            View.SetMaxHealthValue(this[UnitParam.MaxHP])
                .SetCurrentHealthValue(this[UnitParam.CurrentHP]);

            return this;
        }


        public void Move(Vector2 direction, float deltaTime)
        {
            OnMove?.Invoke(direction, deltaTime);
        }


        public void ChangeRotation(Vector2 direction)
        {
            if (View == null) return;
            //var newRotate = Quaternion.LookRotation(direction);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            CurrentDirection = direction.normalized;
        }


        public UnitModel ChangePos(Vector2 delta)
        {
            if (View == null)
                return this;

            View.MovePosition(delta);
            _position = View.transform.localPosition;
            return this;
        }
        
        
        public void TakeDamage(float damage)
        {
            this[UnitParam.CurrentHP] -= damage;
            
            if (this[UnitParam.CurrentHP] > 0)
                return;
            
            MainApp.Instance.GameModel.Monsters.DestroyUnit(this);
        }
    }
}