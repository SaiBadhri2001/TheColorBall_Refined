using UnityEngine;
namespace TheColorBall.Utils
{
    public class CameraSizeFit : MonoBehaviour
    {
        public float sceneWidth = 10;

        Camera _camera;
        void Start()
        {
            SetCameraSize();
        }

        public void SetCameraSize()
        {
            _camera = GetComponent<Camera>();
            float unitsPerPixel = sceneWidth / Screen.width;
            float desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;
            _camera.orthographicSize = desiredHalfHeight;
        }
    }
}
