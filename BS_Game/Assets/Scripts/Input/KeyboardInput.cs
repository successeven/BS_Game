using UnityEngine;

namespace BSGame.Input
{
    public class KeyboardInput : IInput
    {
        public void Init()
        {
        }

        public Vector2? GetDirection()
        {
            var direction = new Vector2(UnityEngine.Input.GetAxis("Horizontal"), 
                UnityEngine.Input.GetAxis("Vertical"));
			
            if (direction.magnitude > 0f)
            {
                return direction;
            }
            
            return null;
        }

        public Vector3? GetCursorPos()
        {
            var mousePos = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            mousePos.z = 0;
            return mousePos ;
        }

        public bool Shoot()
        {
            return UnityEngine.Input.GetAxis("Fire1").Equals(1);
        }

    }
}