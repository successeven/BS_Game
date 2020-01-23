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
    public class ShotgunShootingComponent : BaseShootingComponent
    {
        private bool _wasShoot;
        private ProjectilesContainerModel _projectiles;

        public ShotgunShootingComponent(UnitModel model) : base(model)
        {
            _projectiles = MainApp.Instance.GameModel.Projectiles; 
            CurrentBalance = GameBalance.Instance.Weapons.FirstOrDefault(w => w.Type == UnitWeaponType.Shotgun);

        }

        public override void Start()
        {
        }

        public override void Update(float deltaTime)
        {
            if (GameInput.Shoot() && !_wasShoot)
                Shoot();
            else if (!GameInput.Shoot() && _wasShoot)
                _wasShoot = false;
        }

        protected override void Shoot()
        {
            _wasShoot = true;
            var projectiles = new List<ProjectileModel>();
            for (var i = 0; i < CurrentBalance.NumberBullets ; i++)
            {
                var bulletPrototype = _projectiles.GetProjectile(Model)
                    .SetParams(CurrentBalance);
                projectiles.Add(bulletPrototype);
            }

            foreach (var projectile in projectiles)
            {
                var delta = Random.Range(-CurrentBalance.AngleSinght, CurrentBalance.AngleSinght);
                var direction = Quaternion.Euler(0, 0, delta) * Model.CurrentDirection;
                projectile.InstantiateProjectile()
                    .SetStartPos(Model.View.FirePoint.position)
                    .SetDirection(direction)
                    .Start()
                    .Activate();
            }
        }
    }
}
