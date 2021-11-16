using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class RoundInfoText : MonoBehaviour
    {
        public TextMeshProUGUI numberText;

        private void Update()
        {
            if (MainStateManager.Instance.CurrentRound != null)
            {
                numberText.SetText(MainStateManager.Instance.CurrentRound.RoundNumber.ToString());
            }
        }
    }
}
