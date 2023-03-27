using UI.Control;
using UnityEngine;
using UnityEngine.UI;

namespace UI.General
{
    [RequireComponent(typeof(Button))]
    public class WindowOpener : MonoBehaviour
    {
        [SerializeField] private WindowType _windowType;
        
        private Button _button;

        private void Awake()
        {
            Debug.Log("here");
            _button = gameObject.GetComponent<Button>();
            _button.onClick.AddListener(OpenWindow);
        }
        
        private void OpenWindow()
        {
            UIManager.Instance.OpenWindow(_windowType);
        }


    }
}
