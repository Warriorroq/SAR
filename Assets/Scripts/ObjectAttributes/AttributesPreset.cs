using System.Collections.Generic;
using UnityEngine;

namespace ObjectAttributes
{
    [CreateAssetMenu(fileName = "PlayerPreset", menuName = "Player/Attributes", order = 0)]
    public class AttributesPreset : ScriptableObject
    {
        [SerializeField] private List<Attribute> _attributes;

        public static implicit operator Dictionary<Attribute.AttributeType, Attribute>(AttributesPreset preset)
        {
            Dictionary<Attribute.AttributeType, Attribute> dictionary = new();
            foreach (var attribute in preset._attributes)
                dictionary.Add(attribute._type, attribute.Clone());
            return dictionary;
        }
    }
}