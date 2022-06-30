using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ObjectAttributes
{
    public class ObjectStats : MonoBehaviour
    {
        [SerializeField] private AttributesPreset _attributesPreset;
        private Dictionary<Attribute.AttributeType, Attribute> _attributes;
        public int GetLevelOf(Attribute.AttributeType type)
            => _attributes[type].Level;
        public void ConnectToLevelUpOf(Attribute.AttributeType type, UnityAction<int> action)
            =>_attributes[type].levelUp.AddListener(action);
        private void Awake()
        {
            _attributes = _attributesPreset;
        }
    }
}