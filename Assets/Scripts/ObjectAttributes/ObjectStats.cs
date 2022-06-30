using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ObjectAttributes
{
    public class ObjectStats : MonoBehaviour
    {
        [SerializeField] private AttributesPreset _attributesPreset;
        private Dictionary<Attribute.AttributeType, Attribute> _attributes;

        public void LevelUp(int type)
            => _attributes[(Attribute.AttributeType)type].LevelUp();
        public int GetLevelOf(Attribute.AttributeType type)
            => _attributes[type].Level;
        public UnityEvent<int> GetLevelUpEventOf(Attribute.AttributeType type)
            =>_attributes[type].levelUp;
        private void Awake()
        {
            _attributes = _attributesPreset;
        }
    }
}