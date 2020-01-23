using System.Collections.Generic;
using System.Linq;
using BSGame.Input;
using BSGame.Manager;
using BSGame.Model.Projectiles;
using BSGame.Model.Units;
using BSGame.ScriptableObject;
using UnityEngine;

namespace BSGame.Components.Weapons
{
    public class AutoMachineShootingComponent : BaseShootingComponent
    {
        private float _delay;
        private float _timer;
        private ProjectilesContainerModel _projectiles;

        public AutoMachineShootingComponent(UnitModel model) : base(model)
        {
            _projectiles = MainApp.Instance.GameModel.Projectiles; 
            CurrentBalance = GameBalance.Instance.Weapons.FirstOrDefault(w => w.Type == UnitWeaponType.AutoMachine);
            _delay = 0.1f;
        }

        public override void Start()
        {
        }

        public override void Update(float deltaTime)
        {
            _timer -= deltaTime;
            if (GameInput.Shoot() && _timer <= 0)
                Shoot();
        }

        protected override void Shoot()
        {
            var bulletPrototype = _projectiles.GetProjectile(Model)
                .SetParams(CurrentBalance);

            for (var i = 0; i < CurrentBalance.NumberBullets ; i++)
            {
                var delta = Random.Range(-CurrentBalance.AngleSinght, CurrentBalance.AngleSinght);
                var direction = Quaternion.Euler(0, 0, delta) * Model.CurrentDirection;
                bulletPrototype.InstantiateProjectile()
                    .SetStartPos(Model.View.FirePoint.position)
                    .SetDirection(direction)
                    .Start()
                    .Activate();
            }

            _timer = _delay;
        }
    }
}