using BSGame.Model;
using BSGame.Model.Units;
using UnityEngine;

namespace BSGame.Components
{
    public class MovementComponent : BaseComponent<UnitModel>
    {
        
        public MovementComponent(UnitModel model) : base(model)
        {
        }
        
        public override void Start()
        {
            base.Start();
            Model.OnMove += Move;
        }

        private void Move(Vector2 direction, float deltaTime)
        {
            if (!Enabled) 
                return;

            var velocity = Model[UnitParam.Speed] * deltaTime * direction;
            Model.ChangePos(velocity);
        }
    }
}