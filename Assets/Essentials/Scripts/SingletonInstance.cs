using UnityEngine;
namespace Essentails
{
    public class SingletonInstance<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));

                    if (instance == null)
                    {
                        GameObject gameObject = new GameObject(nameof(T));
                        instance = gameObject.AddComponent<T>();
                    }
                }
                else
                {
                    InteractiveLogger.Print("There is two or more singleton for the same component!!!" + nameof(T), Color.red, TextType.Bold);
                }
                return instance;
            }
        }
    }
}
