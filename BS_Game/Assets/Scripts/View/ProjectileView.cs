using System;
using System.Collections.Generic;
using BSGame.Model.Projectiles;
using BSGame.Tools;
using UnityEngine;

namespace BSGame.View
{
    public class ProjectileView : ViewWithModel<ProjectileModel>
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        public event Action<Collision2D> OnMyCollisionEnter2D;
        public event Action<Collider2D> OnMyTriggerEnter2D;

        private void OnCollisionEnter2D(Collision2D other)
        {
            OnMyCollisionEnter2D?.Invoke(other);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnMyTriggerEnter2D?.Invoke(other);
        }

        public override void StartMe()
        {
            transform.position = Model.Position;
        }

        public new ProjectileView SetModel(ProjectileModel model)
        {
            return base.SetModel(model) as ProjectileView;
        }

        public override void UpdateMe(float deltaTime)
        {
            transform.localPosition = Model.Position;
        }
        
        public ProjectileView SetPosition(Vector2 velosity)
        {
            _rigidbody.MovePosition(_rigidbody.position + velosity);
            return this;
        }
    }
}