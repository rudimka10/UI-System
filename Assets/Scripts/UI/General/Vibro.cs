using UnityEngine;
using UnityEngine.UI;

namespace UI.General
{
    [RequireComponent(typeof(Button))]
    public class Vibro : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = gameObject.GetComponent<Button>();
            _button.onClick.AddListener(Play);
        }

        private void Play()
        {
                Handheld.Vibrate();
        }
    }
}