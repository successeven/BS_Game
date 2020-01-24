using System.Linq;
using BSGame.Model;
using BSGame.Model.Units;
using BSGame.ScriptableObject;

namespace BSGame.Components
{
    public class PlayerParamsComponent : BaseComponent<UnitModel>
    {
        public PlayerParamsComponent(UnitModel model) : base(model)
        {
            var playerParams = GameBalance.Instance.Player;
            Model.CreateNewParams(
                (UnitParam.Speed, playerParams.Speed),
                // (UnitParam.MaxHP, playerParams.MaxHP),
                // (UnitParam.CurrentHP, playerParams.MaxHP),
                (UnitParam.CountPoint, 0)
            ); //GameBalance.Instance.Player.DefaultSpeedAttack));
        }
    }
}
