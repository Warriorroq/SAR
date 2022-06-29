using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    { 
        [SerializeField] private CharacterController _characterController;
        private void Awake()
        {
            if(_characterController == null)
                _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {

        }
    }
}