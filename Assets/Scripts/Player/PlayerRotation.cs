using Cinemachine;
using UnityEngine;
using ObjectAttributes;

namespace Player
{
    public class PlayerRotation : MonoBehaviour
    {
        [SerializeField] private float _speedVertical;
        [SerializeField] private float _speedHorisontal;
        [SerializeField] private CinemachineVirtualCamera _head;

        [SerializeField] private float _defauldFOV = 70f;

        [SerializeField] private float _fovPerLevel = 2f;
        //[SerializeField] private float _maxAngle = 80f;
        private void Start()
        {
            var stats = GetComponent<ObjectStats>();
            AddFOV(stats.GetLevelOf(Attribute.AttributeType.perception));
            stats.GetLevelUpEventOf(Attribute.AttributeType.perception).AddListener(AddFOV);
        }
        private void AddFOV(int level)
            => _head.m_Lens.FieldOfView = _defauldFOV + level * _fovPerLevel;
        private void Update()
        {
            RotateBody();
            RotateHead();
        }
        private void RotateHead()
        {
            float x = _speedVertical * -Input.GetAxis("Mouse Y");
            _head.transform.Rotate(x, 0, 0);
        }
        private void RotateBody()
        {
            float y = _speedHorisontal * Input.GetAxis("Mouse X");
            transform.Rotate(0, y, 0);
        }
    }
}