using UnityEngine;
using VDFramework;

namespace Utility
{
    public class RotateWithCamera : BetterMonoBehaviour
    {
        private Transform cameraTransform;

        private void Awake()
        {
            ResetCameraTransform();
        }

        private void ResetCameraTransform()
        {
            cameraTransform = Camera.main.transform;
        }

        private void LateUpdate()
        {
            CachedTransform.rotation = cameraTransform.rotation;
        }
    }
}
