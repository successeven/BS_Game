using System.Linq;
using BSGame.Input;
using BSGame.Manager;
using BSGame.Model;
using BSGame.Model.Projectiles;
using BSGame.Model.Units;
using BSGame.ScriptableObject;
using UnityEngine;

namespace BSGame.Components.Weapons
{
    public class PistolShootingComponent : BaseShootingComponent
    {
        private bool _wasShoot;
        private ProjectilesContainerModel _projectiles;

        public PistolShootingComponent(UnitModel model) : base(model)
        {
            _projectiles = MainApp.Instance.GameModel.Projectiles;
            CurrentBalance = GameBalance.Instance.Weapons.FirstOrDefault(w => w.Type == UnitWeaponType.Pistol);
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
        }
    }
}