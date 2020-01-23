using System;
using BSGame.Input;
using BSGame.Tools;
using UnityEngine;

namespace BSGame.View
{
    public class CursorView : BaseBehaviour
    {
        public override void UpdateMe(float deltaTime)
        {
            transform.localPosition = GameInput.GetCursorPos().Value;
        }

    }
}
