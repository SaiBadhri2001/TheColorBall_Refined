using UnityEngine;
using UnityEngine.Events;
namespace Essentails
{
    public class ColliderEvents : MonoBehaviour
    {
        [Space]
        [Header("Collision Funtions")]
        public UnityEvent _onCollsionEnter;
        public UnityEvent _onCollisionExit;
        public UnityEvent _onCollisionStay;
        [Space]
        [Header("Trigger Funtions")]
        public UnityEvent _onTriggerEnter;
        public UnityEvent _onTriggerExit;
        public UnityEvent _onTriggerStay;
        [Space(5)]
        [SerializeField] bool _printLog;
        [SerializeField] bool _checkForPlayer;
        [SerializeField] string _playerTag = "MainCamera";
        private bool _isPlayerFound;

        public UnityEvent TriggerStay { get { return _onTriggerStay; } }
        public UnityEvent TriggerEnter { get { return _onTriggerEnter; } }
        public UnityEvent TriggerExit { get { return _onTriggerExit; } }
        //collision funitons
        private void OnCollisionEnter(Collision other)
        {
            PrintLog();
            if (!_checkForPlayer)
                _onCollsionEnter?.Invoke();
            else
            {
                if (CheckForPlayerCollider(other))
                {
                    _onCollsionEnter?.Invoke();
                }
            }
        }
        private void OnCollisionExit(Collision other)
        {
            PrintLog();
            if (!_checkForPlayer)
                _onCollisionExit?.Invoke();
            else
            {
                if (CheckForPlayerCollider(other))
                {
                    _onCollisionExit?.Invoke();
                }
            }
        }
        private void OnCollisionStay(Collision other)
        {
            PrintLog();
            if (!_checkForPlayer)
                _onCollisionStay?.Invoke();
            else
            {
                if (CheckForPlayerCollider(other))
                {
                    _onCollisionStay?.Invoke();
                }
            }
        }

        //trigger funtions
        private void OnTriggerEnter(Collider other)
        {
            PrintLog();
            if (!_checkForPlayer)
                _onTriggerEnter?.Invoke();
            else
            {
                if (CheckForPlayerTrigger(other))
                {
                    _onTriggerEnter?.Invoke();
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            PrintLog();
            if (!_checkForPlayer)
                _onTriggerExit?.Invoke();
            else
            {
                if (CheckForPlayerTrigger(other))
                {
                    _onTriggerExit?.Invoke();
                }
            }
        }
        private void OnTriggerStay(Collider other)
        {
            PrintLog();
            if (!_checkForPlayer)
                _onTriggerStay?.Invoke();
            else
            {
                if (CheckForPlayerTrigger(other))
                {
                    _onTriggerStay?.Invoke();
                }
            }
        }

        public void PrintLog()
        {
            if (_printLog)
                InteractiveLogger.Print("Collision Funtionallity Called", Color.green, TextType.Bold);
            else
                return;
        }
        public bool CheckForPlayerTrigger(Collider trigger)
        {
            if (trigger.gameObject.tag == _playerTag)
                _isPlayerFound = true;
            else
                _isPlayerFound = false;
            return _isPlayerFound;
        }
        private bool CheckForPlayerCollider(Collision collider)
        {
            if (collider.gameObject.tag == _playerTag)
                _isPlayerFound = true;
            else
                _isPlayerFound = false;
            return _isPlayerFound;
        }
    }
}