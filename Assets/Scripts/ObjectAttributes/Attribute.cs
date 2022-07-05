using System;
using UnityEngine;
using UnityEngine.Events;

namespace ObjectAttributes
{
    [Serializable]
    public class Attribute : IDisposable, ICloneable<Attribute>
    {
        public AttributeType _type;
        public int Level
        {
            get => _level;
        }

        public UnityEvent<int> levelUp;
        [SerializeField]private int _level = 1;
        [SerializeField]private int _maxLevel = 10;
        public Attribute()
        {
            levelUp = new();
        }
        public void LevelUp()
        {
            if (_level == _maxLevel)
                return;
            _level++;
            levelUp.Invoke(_level);
        }

        public void SetLevel(int level)
        {
            if(_level > _maxLevel)
                return;
            _level = level;
            levelUp.Invoke(_level);
        }
        public void Dispose()
            =>levelUp.RemoveAllListeners();
        public Attribute Clone()
        {
            var attribute = new Attribute();
            attribute._level = _level;
            attribute._maxLevel = _maxLevel;
            attribute._type = _type;
            return attribute;
        }
        public enum AttributeType : int
        {
            health = 0,
            strenght = 1,
            endurance = 2,
            agility = 3,
            perception = 4,
            defence = 5,
        }
    }
}