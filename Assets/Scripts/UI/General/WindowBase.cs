using UI.Control;
using UnityEngine;

namespace UI.General
{
    public class WindowBase : MonoBehaviour
    {
        [SerializeField] private WindowType _windowType;

        public WindowType WindowType => _windowType;

        public virtual void Open()
        {
            gameObject.SetActive(true);
        }
        
        protected virtual void UpdateContent()
        {
            
            
            
        }

        public virtual void Close()
        {
            gameObject.SetActive(false);
            
            
        }
    }
}