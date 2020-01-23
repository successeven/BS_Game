using System;
using System.Linq;
using BSGame.Components;
using BSGame.Components.AI;
using BSGame.Components.Weapons;
using BSGame.ScriptableObject;
using UnityEngine;

namespace BSGame.Model.Units
{
    public static class UnitsFactory
    {
        public static UnitModel CreateUnit(UnitType unitType)
        {
            var unit = new UnitModel(unitType);
            if (unitType == UnitType.Player)
            {
                SetPlayerComponents(unit);
            }
            else
                SetMonsterComponents(unit);

            return unit;
        }

        private static void SetMonsterComponents(UnitModel unit)
        {
            unit.AddComponent<MovementComponent>();
            unit.AddComponent<AiBaseComponent>();
            unit.AddComponent<MonsterParamsComponent>();
        }

        private static void SetPlayerComponents(UnitModel unit)
        {
            unit.AddComponent<InputComponent>();
            unit.AddComponent<MovementComponent>();
            unit.AddComponent<CameraComponent>();
            unit.AddComponent<PlayerParamsComponent>();
        }

    }
}