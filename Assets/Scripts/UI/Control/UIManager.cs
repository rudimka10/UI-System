using System.Collections.Generic;
using System.Linq;
using UI.Control.PopUps;
using UI.General;
using UI.PopUp;
using UnityEngine;
using Utils.Singleton;

namespace UI.Control
{
    public class UIManager : SingletonMonoBehaviour<UIManager>
    {
        [SerializeField] private List<WindowBase> _windows;
        
        public void OpenWindow(WindowType windowType)
        {
            CloseAll();
            _windows.FirstOrDefault(x => x.WindowType == windowType)?.Open();
        }
        
        public void OpenPopup(PopUpType popUpType)
        {
            var window = (PopUpWindow)_windows.FirstOrDefault(x => x.WindowType == WindowType.Popups);
            window.Open(popUpType);
        }

        public void ClosePopups()
        {
            var window = (PopUpWindow)_windows.FirstOrDefault(x => x.WindowType == WindowType.Popups);
            window.Close();
        }
        
        private void CloseAll()
        {
            foreach (var window in _windows)
            {
                window.Close();
            }
        }
    }
}