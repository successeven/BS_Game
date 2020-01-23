using System.Collections.Generic;
using System.Linq;
using BSGame.Input;
using BSGame.Manager;
using BSGame.Model.Projectiles;
using BSGame.Model.Units;
using BSGame.ScriptableObject;
using BSGame.View;
using UnityEngine;
using Grid = BSGame.Model.AStar.Grid;

namespace BSGame.Model
{
    public class GameModel : BaseModel
    {
        public ProjectilesContainerModel Projectiles => _projectiles;
        public Grid GridAStar => _gridAStar;
        public UnitsContainerModel Monsters => _monsters;
        private readonly List<IModelsContainer> _containers = new List<IModelsContainer>();
        private readonly UnitsContainerModel _players, _monsters;
        private readonly ProjectilesContainerModel _projectiles;
        private CursorView _cursor;
        private GameObject[] _spawns;
        private Grid _gridAStar = null;

        private float _timerSpawn;

        public GameModel()
        {
            _players = new UnitsContainerModel(UnitType.Player);
            _monsters = new UnitsContainerModel(UnitType.Enemy);
            _projectiles = new ProjectilesContainerModel();

            _containers.Add(_players);
            _containers.Add(_monsters);
            _containers.Add(_projectiles);
        }

        public override BaseModel Start()
        {
            _cursor = Object.FindObjectOfType<CursorView>();
            _spawns = GameObject.FindGameObjectsWithTag("SpawnPos");
            return this;
        }


        public GameModel RestartGame()
        {
            foreach (var container in _containers)
            {
                container.Start();
            }

            SpawnPlayer();
            //_timerSpawn = GameBalance.Instance.SpawnTimer;
            return this;
        }

        private void SpawnPlayer()
        {
            var player = _players.SpawnUnit(Vector2.zero);
            SetWeaponComponents(player);
        }


        private static void SetWeaponComponents(UnitModel unit)
        {
            var defautWeapon = GameBalance.Instance.Weapons.FirstOrDefault(w => w.IsDefault);
            if (defautWeapon == null)
                return;

            unit.SetCurrentWeapon(defautWeapon.Type);
        }
        public override void Update(float deltaTime)
        {
            foreach (var container in _containers)
            {
                container.Update(deltaTime);
            }

            CheckChangeGun();

            _cursor.UpdateMe(deltaTime);

            _timerSpawn -= deltaTime;
            if (_timerSpawn > 0)
                return;

            SpawnMonsters();
            _timerSpawn = GameBalance.Instance.SpawnTimer;
        }

        private void CheckChangeGun()
        {
            if (UnityEngine.Input.GetKey(KeyCode.Alpha1))
                _players.GetFirstUnit().SetCurrentWeapon(UnitWeaponType.Pistol);
            
            if (UnityEngine.Input.GetKey(KeyCode.Alpha2))
                _players.GetFirstUnit().SetCurrentWeapon(UnitWeaponType.Shotgun);
            
            if (UnityEngine.Input.GetKey(KeyCode.Alpha3))
                _players.GetFirstUnit().SetCurrentWeapon(UnitWeaponType.AutoMachine);
        }

        public override void FixedUpdate(float deltaTime)
        {
            foreach (var container in _containers)
            {
                container.FixedUpdate(deltaTime);
            }

            _cursor.FixedUpdateMe(deltaTime);
        }


        private void SpawnMonsters()
        {
            if (_monsters.CountUnits >= GameBalance.Instance.MaxMonstersOnScene)
                return;
            
            var countMonsterSpawns = Mathf.Min(GameBalance.Instance.MaxMonstersOnScene -_monsters.CountUnits,
                GameBalance.Instance.CountMonsterSpawns);
            
            for (var i = 0; i < countMonsterSpawns; i++)
            {
                var randomSpawn = _spawns[Random.Range(0, _spawns.Length - 1)];
                _monsters.SpawnUnit(randomSpawn.transform.position);
            }
        }

        public UnitModel GetPlayerModel()
        {
            return _players.GetFirstUnit();
        }
        
        public void InitAstarGrid()
        {
            var nodeSize = MainApp.Instance.NodeRadius;
            if (_gridAStar == null)
            {
                 
                var gridSize = new Vector2(60,60);
                
                _gridAStar = new Grid();
                _gridAStar.SetGridSize(gridSize)
                    .SetNodeRadius(nodeSize)
                    .SetMask(GameLayers.WallMask );
            }
            
            _gridAStar.CreateGrid();
            Debug.Log("Перестройка АСтара");
        }
    }
}