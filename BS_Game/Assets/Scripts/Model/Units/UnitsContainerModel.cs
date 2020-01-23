using System.Collections.Generic;
using System.Linq;
using BSGame.Tools;
using UnityEngine;

namespace BSGame.Model.Units
{
    public class UnitsContainerModel : BaseModel, IModelsContainer
    {
        private UnitType _unitType;
        private List<UnitModel> _units = new List<UnitModel>();

        public int CountUnits => _units.Count;

        public UnitsContainerModel(UnitType unitType)
        {
            _unitType = unitType;
        }

        public override BaseModel Start()
        {
            return this;
        }
        
        void IModelsContainer.Start()
        {
            Start();
        }

        public void Clear()
        {
            foreach (var unit in _units.Where(unit => unit.View != null))
                unit.View.DestroyGO();

            _units.Clear();
        }

        public override void Update(float deltaTime)
        {
            foreach (var unit in _units)
            {
                unit.Update(deltaTime);
            }
        }

        public override void FixedUpdate(float deltaTime)
        {
            foreach (var unit in _units)
            {
                unit.FixedUpdate(deltaTime);
            }
        }
        
        public void DestroyUnit(UnitModel unit)
        {
            if (!_units.Contains(unit))
                return;

            unit.DestroyView();
            _units.Remove(unit);
        }

        public UnitModel SpawnUnit(Vector2 spawnPos)
        {
            var newUnit = UnitsFactory.CreateUnit(_unitType)
                .InitViewModel(spawnPos);
            if (newUnit.View == null)
            {
                Debug.LogError("ошибка при создании юнита");
                return null;
            }

            newUnit.Start();
            newUnit.View.Activate();
            
            _units.Add(newUnit);
            return newUnit;
        }
        
        public UnitModel GetFirstUnit()
        {
            return _units.FirstOrDefault();
        }

    }
}
