using System;
using UnityEngine;
using UnityEngine.Events;
namespace ObjectAttributes
{
    [RequireComponent(typeof(ObjectStats))]
    public class HealthBehavior : MonoBehaviour
    {
        public UnityEvent onDeath;
        public UnityEvent<float> hpChanged;
        [SerializeField] private bool _godMode = false;
        [SerializeField] private float _maxHp;
        [SerializeField] private float _currentHp;
        [SerializeField] private float _hpPerLevel = 20;
        public void TakeDamage(float Amount)
        {
            if (_godMode)
                return;
            _currentHp -= Mathf.Clamp(Amount,0, _maxHp);
            hpChanged.Invoke(_currentHp);
            if (_currentHp <= 0) 
                onDeath.Invoke();
        }
        private void Start()
        {
            var stats = GetComponent<ObjectStats>();
            _maxHp = stats.GetLevelOf(Attribute.AttributeType.health) * _hpPerLevel;
            _currentHp = _maxHp;
            stats.GetLevelUpEventOf(Attribute.AttributeType.health).AddListener(IncreaceMaxHealth);
        }
        private void IncreaceMaxHealth(int level)
            =>_maxHp = level * _hpPerLevel;
    }
}
