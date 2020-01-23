using BSGame.Model;
using BSGame.Model.Projectiles;
using BSGame.Tools;
using BSGame.View;
using UnityEngine;

namespace BSGame.Components.Projectiles
{
    public class ProjectileCollisionComponent : BaseComponent<ProjectileModel>
    {
        public ProjectileCollisionComponent(ProjectileModel model) : base(model)
        {
            Model.OnActive += Activate;
        }

        private void Activate(bool value)
        {
            if (value)
                Model.View.OnMyTriggerEnter2D += OnCollisionEnter2D;
            else
                Model.View.OnMyTriggerEnter2D -= OnCollisionEnter2D;
        }

        private void OnCollisionEnter2D(Collider2D other)
        {
            var behaviour = other.GetComponentInParent<BaseBehaviour>();

            if (behaviour is ProjectileView) return;

            Model.OnCollision(behaviour);
        }

    }
}
