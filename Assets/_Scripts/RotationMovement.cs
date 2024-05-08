using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{
    public class RotationMovement : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 400f;

        private void FixedUpdate()
        {
            transform.Rotate(0f, 0f, _rotationSpeed * Time.deltaTime);
        }
    }
}