using System;
using System.Collections.Generic;
using System.Linq;
using BSGame.Manager;
using BSGame.Model.Units;
using BSGame.ScriptableObject;
using BSGame.Tools;
using BSGame.View;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BSGame.Model.Projectiles
{
    public class ProjectileModel : ModelWithView<ProjectileView>
    {
        public Vector3 Position => _position;

        public UnitModel Owner { get; private set; }
        public bool IsUse { get; set; }
        public Vector3 StartPos => _startPos;
        public Vector3 Direction => _direction;
        public event Action<bool> OnActive;
        private float _leftLifeTime = -1;
        private float _defaultLifeTime;

        public bool Active
        {
            get => _active;
            set
            {
                OnActive?.Invoke(value);
                _active = value;
            }
        }

        private ProjectilesContainerModel _containerModel;
        private Vector3 _position;
        private bool _active;
        private Vector3 _startPos;
        private Vector3 _direction;

        public ProjectileModel(UnitModel unitModel)
        {
            Owner = unitModel;
        }

        public new ProjectileModel Start()
        {
            base.Start();
            return this;
        }

        public override void Update(float deltaTime)
        {
            if (!Active)
                return;

            base.Update(deltaTime);
            if (View != null)
                View.UpdateMe(deltaTime);


            _leftLifeTime -= deltaTime;

            if (_leftLifeTime > 0f) return;

            Disable();
        }

        public ProjectileModel InstantiateProjectile()
        {
            if (View == null)
            {
                var projectilePref = Resources.Load<ProjectileView>("Prefabs/SimpleProjectile");
                View = Object.Instantiate(projectilePref, MainApp.RootTransform, false)
                    .SetModel(this);
            }

            return this;
        }

        public virtual ProjectileModel SetLifeTime(float lifeTime)
        {
            _leftLifeTime = _defaultLifeTime = lifeTime;
            return this;
        }

        public ProjectileModel ChangePos(Vector3 delta)
        {
            View.SetPosition(delta);
            _position = View.transform.localPosition;
            return this;
        }

        public ProjectileModel SetStartPos(Vector3 pos)
        {
            _startPos = _position = pos;
            if (View != null)
                View.transform.position = pos;
            return this;
        }

        public ProjectileModel SetContainerModel(ProjectilesContainerModel projectilesContainerModel)
        {
            _containerModel = projectilesContainerModel;
            return this;
        }

        public ProjectileModel Activate()
        {
            View?.Activate();
            View?.StartMe();
            Active = true;
            IsUse = true;
            _leftLifeTime = _defaultLifeTime;
            return this;
        }

        public void Disable()
        {
            IsUse = false;
            Active = false;
            View.Deactivate();
        }

        public ProjectileModel SetDirection(Vector3 direction)
        {
            _direction = direction;
            return this;
        }

        public void OnUnitCollision(UnitView unit)
        {
            unit.Model[UnitParam.CurrentHP] -= this[WeaponParam.Damage];
            if (unit.Model[UnitParam.CurrentHP] > 0)
                return;

            if (unit.Model.ContainsParam(EnemyParam.Points))
                Owner[UnitParam.CountPoint] += unit.Model[EnemyParam.Points];

            MainApp.Instance.GameModel.Monsters.DestroyUnit(unit.Model);
        }

        public void OnCollision(BaseBehaviour behaviour)
        {
            if (behaviour is UnitView unitView)
            {
                if (Owner.View == unitView)
                    return;

                OnUnitCollision(unitView);
            }

            Disable();
        }

        public ProjectileModel SetParams(WeaponBalance balanceParam)
        {
            this.CreateNewParam(WeaponParam.Speed, balanceParam.Speed);
            this.CreateNewParam(WeaponParam.Damage, balanceParam.Damage);
            this.SetLifeTime(balanceParam.LifeTime);
            return this;
        }
    }
}