using System.Collections.Generic;
using UnityEngine;

namespace BSGame.Input
{
    public static class GameInput
    {
        private static List<IInput> _inputs = new List<IInput>();

        public static void RegisterInput(IInput input)
        {
            input.Init();
            _inputs.Add(input);
        }

        public static Vector2? GetDirection()
        {
            foreach (var input in _inputs)
            {
                var direction = input.GetDirection();
                if (direction.HasValue) return direction;
            }

            return null;
        }
        
        public static Vector3? GetCursorPos()
        {
            foreach (var input in _inputs)
            {
                var pos = input.GetCursorPos();
                if (pos.HasValue) return pos;
            }

            return null;
        }

        public static bool Shoot()
        {
            foreach (var input in _inputs)
            {
                return input.Shoot();
            }
            return false;
        }
    }
}