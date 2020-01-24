using BSGame.Input;
using BSGame.Model;
using UnityEngine;

namespace BSGame.Manager
{
    public class MainApp : MonoBehaviour
    {
        public float NodeRadius;
        public static Transform RootTransform { get; private set; }
        public GameModel GameModel => _gameModel;
        public static MainApp Instance;
        private GameModel _gameModel;

        private void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            Init();
        }

        private void Init()
        {
            RootTransform = transform;
            GameInput.RegisterInput(new KeyboardInput());
            _gameModel = new GameModel();
            _gameModel.Start();
            _gameModel.RestartGame();
        }

        // Update is called once per frame
        private void Update()
        {
            _gameModel.Update(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _gameModel.FixedUpdate(Time.fixedDeltaTime);
        }
    }
}