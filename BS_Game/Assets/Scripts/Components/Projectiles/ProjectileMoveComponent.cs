using BSGame.Model;
using BSGame.Model.Projectiles;
using BSGame.ScriptableObject;
using UnityEngine;

namespace BSGame.Components.Projectiles
{
    public class ProjectileMoveComponent : BaseComponent<ProjectileModel>
    {
        public ProjectileMoveComponent(ProjectileModel model) : base(model)
        {
        }

        public override void Update(float deltaTime)
        {
            if (!Model.Active)
                return;

            Model.ChangePos(deltaTime * Model[WeaponParam.Speed] * Model.Direction);
        }
    }
}