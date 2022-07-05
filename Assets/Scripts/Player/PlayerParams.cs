using ObjectAttributes;
using UnityEngine;

[RequireComponent(typeof(ObjectStats))]
public class PlayerParams : MonoBehaviour
{
    public float jumpStrength = 5;
    public float currentSpeed = 12;
    public float maxFreeFallVelocityWithoutDamage = 10;
    public float currentStamina;
    public float maxStamina;
    public float SprintSpeed => currentSpeed + _additionalSprintSpeed;
    [SerializeField] private float _speedPerLevel = 2; 
    [SerializeField] private float _jumpStrengthPerLevel = 1.5f;
    [SerializeField] private float _staminaPerLevel = 10;
    [SerializeField] private float _additionalSprintSpeed = 1;
    
    private void Start()
    {
        var stats = GetComponent<ObjectStats>();
        var agility = stats.GetLevelOf(Attribute.AttributeType.agility);
        
        stats.GetLevelUpEventOf(Attribute.AttributeType.agility).AddListener(IncreaceParametersByAgility);
        stats.GetLevelUpEventOf(Attribute.AttributeType.strenght).AddListener(IncreaceJumpStrength);
        stats.GetLevelUpEventOf(Attribute.AttributeType.endurance).AddListener(IncreaceStamina);
        
        jumpStrength = stats.GetLevelOf(Attribute.AttributeType.strenght) * _jumpStrengthPerLevel;
        maxStamina = stats.GetLevelOf(Attribute.AttributeType.endurance) * _staminaPerLevel;
        currentSpeed = agility * _speedPerLevel;
        _additionalSprintSpeed += agility;
        currentStamina = maxStamina;
    }
    private void IncreaceParametersByAgility(int level)
    {
        currentSpeed = level * _speedPerLevel;
        maxFreeFallVelocityWithoutDamage++;
        _additionalSprintSpeed++;
    }
    private void IncreaceJumpStrength(int level)
        => jumpStrength = level * _jumpStrengthPerLevel;
    private void IncreaceStamina(int level)
    {
        maxStamina = level * 10;
    }
}
