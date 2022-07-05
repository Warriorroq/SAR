using System;
using ObjectAttributes;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(HealthBehavior))]
    [RequireComponent(typeof(PlayerParams))]
    public class PlayerMovement : MonoBehaviour
    { 
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private HealthBehavior _playersHealth;
        [SerializeField] private Vector3 _moveDirection;
        [SerializeField] private Vector3 _forceDirection;
        [SerializeField] private PlayerParams _params;
        [SerializeField] private PlayerMovementState _movementState;
        public void AddForce(Vector3 force)
            =>_forceDirection += transform.TransformDirection(force);
        private void Awake()
        {
            if (_params == null)
                _params = GetComponent<PlayerParams>();
            if(_characterController == null)
                _characterController = GetComponent<CharacterController>();
            if (_playersHealth == null)
                _playersHealth = GetComponent<HealthBehavior>();
        }
        private void Update()
        {
            SetMoveDirection();
            Grounded();
            if (_moveDirection.sqrMagnitude < .1f && _forceDirection.sqrMagnitude < .1f)
                _movementState = PlayerMovementState.Idle;
            if (_characterController.enabled)
                _characterController.Move((_moveDirection + _forceDirection) * Time.deltaTime);
        }
        private void Grounded()
        {
            if (_characterController.isGrounded)
            {
                _forceDirection = Vector3.zero;
                FallDamage();
                Jump();
            }
            else
            {
                if (_forceDirection.y > 1f)
                    _movementState = PlayerMovementState.Jump;
                else if(_forceDirection.y < -1f)
                    _movementState = PlayerMovementState.Falling;
                
                _forceDirection.y += Physics.gravity.y * Time.deltaTime;
            }
        }
        private void FallDamage()
        {
            if (_characterController.velocity.y < -_params.maxFreeFallVelocityWithoutDamage)
                _playersHealth.TakeDamage(-_characterController.velocity.y -_params.maxFreeFallVelocityWithoutDamage);
        }
        private void Jump()
        {
            if (Input.GetButton("Jump"))
                _forceDirection.y = _params.jumpStrength;
        }
        private void SetMoveDirection()
        {
            var speed = _params.currentSpeed;
            if (_characterController.isGrounded)
            {
                if (_movementState != PlayerMovementState.Running && Input.GetKey(KeyCode.LeftShift) && _params.currentStamina > _params.maxStamina/4)
                    _movementState = PlayerMovementState.Running;
                else if(_params.currentStamina < 0)
                    _movementState = PlayerMovementState.Idle;
            }
            if (_movementState == PlayerMovementState.Running)
            {
                speed = _params.SprintSpeed;
                _params.currentStamina -= Time.deltaTime * speed;
            }
            else
            {
                _params.currentStamina += Time.deltaTime;
                if (_params.currentStamina > _params.maxStamina)
                    _params.currentStamina = _params.maxStamina;
                if (_moveDirection.sqrMagnitude > .1f && _forceDirection.sqrMagnitude < .1f)
                    _movementState = PlayerMovementState.Walking;
            }
            Debug.Log($"{speed} {_params.currentStamina}");
            _moveDirection.x = Input.GetAxis("Horizontal") * speed;
            _moveDirection.z = Input.GetAxis("Vertical") * speed;
            _moveDirection = transform.TransformDirection(_moveDirection);
        }

        private enum PlayerMovementState
        {
            Idle = 0,
            Running = 1,
            Falling = 2,
            Jump = 3,
            Walking = 4
        }
    }
}