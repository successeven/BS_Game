using BSGame.Model;
using BSGame.Model.Units;
using BSGame.ScriptableObject;
using UnityEngine;

namespace BSGame.Components.Weapons
{
    public class BaseShootingComponent : BaseComponent<UnitModel>
    {
        protected WeaponBalance CurrentBalance;
        public BaseShootingComponent(UnitModel model) : base(model)
        {
        }

        protected virtual void Shoot()
        {
            
        }

        public virtual void ChangeGun()
        {
            if (Model.View.CurrentGunSprite != null)
                Model.View.CurrentGunSprite.sprite = CurrentBalance.ImageWeapon;
        }
    }
}
