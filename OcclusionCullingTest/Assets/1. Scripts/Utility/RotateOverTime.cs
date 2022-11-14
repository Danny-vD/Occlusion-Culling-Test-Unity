using UnityEngine;
using VDFramework;

namespace Utility
{
    public class RotateOverTime : BetterMonoBehaviour
    {
        [SerializeField]
        private float rotationSpeed = 1.0f;
        
        private void FixedUpdate()
        {
            CachedTransform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
        }
    }
}
