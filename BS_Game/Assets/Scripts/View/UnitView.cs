using System;
using System.Collections.Generic;
using BSGame.Manager;
using BSGame.Model;
using BSGame.Model.AStar;
using BSGame.Model.Units;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace BSGame.View
{
    public class UnitView : ViewWithModel<UnitModel>
    {
        public Transform FirePoint => _firePoint;
        public SpriteRenderer SkinSpriteRenderer => _skinSpriteRenderer;
        public Image CurrentGunSprite => _currentGunSprite;
        public event Action<Collider2D> OnMyTriggerEnter2D;

        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private SpriteRenderer _skinSpriteRenderer;
        [SerializeField] private Transform _asset;
        [SerializeField] private Slider _healthBar;
        [SerializeField] private Image _currentGunSprite;
        [SerializeField] private Text _counter;
        
        public new UnitView SetModel(UnitModel model)
        {
            model.ParamChanged += OnParamChanged;
            return base.SetModel(model) as UnitView;
        }

        private void OnParamChanged(Enum param, float value)
        {
            switch (param)
            {
                case UnitParam.CurrentHP:
                    _healthBar.value = value;
                    break;
                
                case UnitParam.CountPoint:
                    _counter.text = Mathf.RoundToInt(value).ToString();
                    break;
            }
        }

        public UnitView MovePosition(Vector2 velosity)
        {
            _rigidbody.MovePosition(_rigidbody.position + velosity);
            return this;
        }
        
        
        public override void UpdateMe(float deltaTime)
        {
            base.UpdateMe(deltaTime);
            _asset.localRotation = Model.Rotation;
        }
        
        public UnitView SetMaxHealthValue(float maxHealth)
        {
            _healthBar.maxValue = maxHealth;
            return this;
        }

        public UnitView SetCurrentHealthValue(float healthValue)
        {
            _healthBar.value = healthValue;
            return this;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            OnMyTriggerEnter2D?.Invoke(other);
        }

    }
}