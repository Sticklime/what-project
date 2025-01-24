using UnityEngine;

namespace CodeBase.Installer
{
    public class DontDestroy : MonoBehaviour
    {
        private void Awake() => 
            DontDestroyOnLoad(this);
    }
}