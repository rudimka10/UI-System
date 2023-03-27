using UI.Control;
using UI.Control.PopUps;
using UnityEngine;
using UnityEngine.UI;

namespace UI.General
{
    [RequireComponent(typeof(Button))]
    public class PopupOpener : MonoBehaviour
    {
        [SerializeField] private PopUpType _popUpType;
        private Button _button;

        public void OpenPopup()
        {
            UIManager.Instance.OpenPopup(_popUpType);
        }
        
        private void Awake()
        {
            _button = gameObject.GetComponent<Button>();
            _button.onClick.AddListener(OpenPopup);
        }
    }
}
