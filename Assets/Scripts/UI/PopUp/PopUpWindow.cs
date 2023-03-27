using System.Collections.Generic;
using System.Linq;
using UI.Control.PopUps;
using UI.General;
using UnityEngine;
using Utils.Extensions;

namespace UI.PopUp
{
    public class PopUpWindow : WindowBase
    {
        [SerializeField] private List<PopUpBase> _popUps;

        public void Open(PopUpType popUpType)
        {
            base.Open();
            _popUps.FirstOrDefault(x => x.PopUpType == popUpType)?.Open();

        }

        public override void Close()
        {
            foreach (var popUp in _popUps)
            {
                popUp.SetActive(false);
            }
            
            base.Close();
        }
    }
}
