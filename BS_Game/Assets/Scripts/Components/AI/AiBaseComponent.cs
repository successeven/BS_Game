using System.Collections.Generic;
using BSGame.Manager;
using BSGame.Model;
using BSGame.Model.AStar;
using BSGame.Model.Units;
using UnityEngine;
using UnityEngine.UI;

namespace BSGame.Components.AI
{
    public class AiBaseComponent : BaseComponent<UnitModel>
    {
        protected UnitModel Player;
        protected List<Vector2> CurrentPath = new List<Vector2>();
        protected int CurrentWaypointIndex = 0;
        protected float NextWaypointDistance = 1f;
        protected float UpdatePathDelayTimer = 2f;
        protected float Timer = -1;
        protected Vector2 _targetPos;

        private GameModel _gameModel;

        public AiBaseComponent(UnitModel model) : base(model)
        {
        }

        public override void Start()
        {
            _gameModel = MainApp.Instance.GameModel;
            Player = _gameModel.GetPlayerModel();
        }

        public override void Update(float deltaTime)
        {
            Timer -= deltaTime;
            //
            // if (Player == null)
            //     Player = _gameModel.GetPlayerModel();

            if (_gameModel.GridAStar == null || _gameModel.GridAStar.Nodes == null)
                _gameModel.InitAstarGrid();

            if (CurrentPath.Count == 0 || Timer < 0)
            {
                UpdatePath();
                return;
            }

            UpdateCurrentWaypointIndex();
            if (CurrentWaypointIndex > CurrentPath.Count - 1)
                return;
            
            if (Vector2.Distance( CurrentPath[CurrentPath.Count -1], Model.Position) < .5f)
                return;

            var direction = CurrentPath[CurrentWaypointIndex] - Model.Position;
            Model.Move(direction.normalized, deltaTime);
            Model.ChangeRotation(direction);
        }

        private void UpdatePath()
        {
            Timer = UpdatePathDelayTimer;
            if (CurrentPath.Count == 0)
            {
                _targetPos = Player.Position;
                SearchPath();
                return;
            }

            if (_targetPos == Player.Position )
                return;

            if (Vector2.Distance(CurrentPath[CurrentWaypointIndex], Player.Position) < 2f &&
                CurrentWaypointIndex != CurrentPath.Count - 1)
            {
                return;
            }

            _targetPos = Player.Position;
            SearchPath();
        }

        private void SearchPath()
        {
            var finalPath = Pathfinding.FindPath(_gameModel.GridAStar,
                Model.Position,
                _targetPos);
            
            if (finalPath == null)
                return;
            
            CurrentPath.Clear();
            foreach (var node in finalPath)
                CurrentPath.Add(node.Position);

            CurrentWaypointIndex = 0;
        }

        private void UpdateCurrentWaypointIndex()
        {
            var distance = Vector3.Distance(Model.Position, CurrentPath[CurrentWaypointIndex]);
            while (distance < NextWaypointDistance &&
                   (CurrentWaypointIndex < CurrentPath.Count - 1))
            {
                CurrentWaypointIndex++;
                distance = Vector3.Distance(Model.Position, CurrentPath[CurrentWaypointIndex]);
            }
        }
    }
}