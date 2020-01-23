using UnityEngine;

namespace BSGame.Input
{
    public interface IInput
    {
        void Init();
        Vector2? GetDirection();
        Vector3? GetCursorPos();
        bool Shoot();
    }
}