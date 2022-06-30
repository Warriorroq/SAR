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
        private void Awake()
        {
            var stats = GetComponent<ObjectStats>();
            _maxHp = stats.getLevelOf(Attribute.AttributeType.health) * 20;
            _currentHp = _maxHp;
        }
        public void TakeDamage(float Amount)
        {
            if (_godMode)
                return;
            _currentHp -= Amount;
            hpChanged.Invoke(_currentHp);
            if (_currentHp < 0) 
                onDeath.Invoke();
        }
    }
}
