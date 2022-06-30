using UnityEngine;

namespace Player
{
    public class PlayerRotation : MonoBehaviour
    {
        [SerializeField] private float _speedVertical;
        [SerializeField] private float _speedHorisontal;
        [SerializeField] private Transform _head;
        [SerializeField] private float _maxAngle = 80f;
        private void Update()
        {
            RotateBody();
            RotateHead();
        }
        private void RotateHead()
        {
            float x = _speedVertical * -Input.GetAxis("Mouse Y");
            _head.Rotate(x, 0, 0);
        }
        private void RotateBody()
        {
            float y = _speedHorisontal * Input.GetAxis("Mouse X");
            transform.Rotate(0, y, 0);
        }
    }
}