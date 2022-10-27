using UnityEngine;

namespace Utility
{
    public class FPSLimiter : MonoBehaviour
    {
        [SerializeField]
        private int fpsLimit = -1;
        
        private void Awake()
        {
            Application.targetFrameRate = fpsLimit;
        }
    }
}
