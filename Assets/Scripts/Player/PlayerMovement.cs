using ObjectAttributes;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(HealthBehavior))]
    public class PlayerMovement : MonoBehaviour
    { 
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private HealthBehavior _playersHealth;
        [SerializeField] private Vector3 _moveDirection;
        [SerializeField] private float _jumpSpeed = 5;
        [SerializeField] private float _currentSpeed = 12;
        [SerializeField] private float _maxFreeFallVelocityWithoutDamage = 10;
        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            if(_characterController == null)
                _characterController = GetComponent<CharacterController>();
            if (_playersHealth == null)
                _playersHealth = GetComponent<HealthBehavior>();
        }

        private void Update()
        {
            SetMoveDirection();
            Grounded();
            if(_characterController.enabled)
                _characterController.Move(_moveDirection * Time.deltaTime);
        }
        private void Grounded()
        {
            if (_characterController.isGrounded)
            {
                FallDamage();
                Jump();
                if(_moveDirection.y < 0)
                    _moveDirection.y = 0;
            }
            else
            {
                _moveDirection.y += Physics.gravity.y * Time.deltaTime;
            }
        }
        private void FallDamage()
        {
            if (_characterController.velocity.y < -_maxFreeFallVelocityWithoutDamage)
                _playersHealth.TakeDamage(-_characterController.velocity.y - _maxFreeFallVelocityWithoutDamage);
        }
        private void Jump()
        {
            if (Input.GetButton("Jump"))
                _moveDirection.y = _jumpSpeed;
        }
        private void SetMoveDirection()
        {
            _moveDirection.x = Input.GetAxis("Horizontal") * _currentSpeed;
            _moveDirection.z = Input.GetAxis("Vertical") * _currentSpeed;
            _moveDirection = transform.TransformDirection(_moveDirection);
        }
    }
}