using UnityEngine;

namespace UI.Control.PopUps
{
    public class PopUpBase : MonoBehaviour
    {
        [SerializeField] private PopUpType _popUpType;

        public PopUpType PopUpType => _popUpType;

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void AskClose()
        {
            UIManager.Instance.ClosePopups();
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
