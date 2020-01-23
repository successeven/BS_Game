using BSGame.Model;
using BSGame.Model.Units;
using BSGame.ScriptableObject;
using UnityEngine;

namespace BSGame.Components
{
    public class MonsterParamsComponent  : BaseComponent<UnitModel>
    {
        private EnemyBalance _balance;
        public MonsterParamsComponent(UnitModel model) : base(model)
        {
            _balance = GameBalance.Instance.Enemys[
                Random.Range(0, GameBalance.Instance.Enemys.Count -1)];
            
            Model.CreateNewParams(
                (UnitParam.Speed, _balance.Speed),
                (UnitParam.MaxHP, _balance.MaxHP),
                (UnitParam.CurrentHP, _balance.MaxHP),
                (EnemyParam.Points, _balance.CountPoint)
            );
        }

        public override void Start()
        {
            if (Model.View.SkinSpriteRenderer == null)
                return;
            
            Model.View.SkinSpriteRenderer.sprite = _balance.Image;
        }
    }
}