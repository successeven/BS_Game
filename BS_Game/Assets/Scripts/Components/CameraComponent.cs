using BSGame.Model;
using BSGame.Model.Units;
using BSGame.ScriptableObject;
using UnityEngine;

namespace BSGame.Components
{
    public class CameraComponent : BaseComponent<UnitModel>
    {
        private Camera _camera;

        public CameraComponent(UnitModel model) : base(model)
        {
        }

        public override void Start()
        {
            _camera = Camera.main;
        }

        public override void Update(float deltaTime)
        {
            var newCamPos = Model.View.transform.localPosition;
            newCamPos.z = -10;
            _camera.transform.position = newCamPos;
        }
    }
}