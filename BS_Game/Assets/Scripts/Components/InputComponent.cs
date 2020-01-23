using BSGame.Input;
using BSGame.Model;
using BSGame.Model.Units;
using UnityEngine;

namespace BSGame.Components
{
    public class InputComponent : BaseComponent<UnitModel>
    {
        public InputComponent(UnitModel model) : base(model)
        {
        }

        public override void FixedUpdate(float deltaTime)
        {
            var direction = GameInput.GetDirection();
            if (direction.HasValue && direction.Value != Vector2.zero)
            {
                Model.Move(direction.Value, deltaTime);
            }

            var targetPos = GameInput.GetCursorPos();
            if (targetPos.HasValue && targetPos.Value != Vector3.zero)
            {
                var targetDirection = (Vector2)targetPos.Value - Model.Position;
                Model.ChangeRotation(targetDirection);
            }
        }
    }
}