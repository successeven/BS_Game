using System;
using System.Collections.Generic;
using System.Linq;
using BSGame.Components.Projectiles;
using BSGame.Model.Units;
using BSGame.ScriptableObject;
using BSGame.Tools;

namespace BSGame.Model.Projectiles
{
    public class ProjectilesContainerModel  : BaseModel, IModelsContainer
    {
        private List<ProjectileModel> _projectiles = new List<ProjectileModel>();

        public void Start()
        {
        }

        public override void Update(float deltaTime)
        {
            foreach (var item in _projectiles)
                item.Update(deltaTime);
        }

        public void Clear()
        {
            foreach (var item in _projectiles)
                item.View.DestroyGO();
            _projectiles.Clear();
        }

        public void RemoveProjectile(ProjectileModel projectile)
        {
            _projectiles.Remove(projectile);
        }

        public ProjectileModel GetProjectile(UnitModel unitModelOwner)
        {
            var freeProjectile =
                _projectiles.FirstOrDefault(x => x.IsUse == false &&  !x.Active);

            var result =  freeProjectile == null ? CreateProjectile(unitModelOwner) : freeProjectile;

            result.IsUse = true;
            return result;
        }

        private ProjectileModel CreateProjectile(UnitModel unitModelOwner)
        {
            var projectile = new ProjectileModel(unitModelOwner)
                .SetContainerModel(this);

            projectile.AddComponent<ProjectileMoveComponent>();
            projectile.AddComponent<ProjectileCollisionComponent>();
            
            _projectiles.Add(projectile);
            return projectile;
        }

    }
}