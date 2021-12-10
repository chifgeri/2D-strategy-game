using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI { 
    public class MessagePanel : Singleton<MessagePanel>
    {
        [SerializeField]
        private TMP_Text message;

        private int timeToDismiss = 3;

        private float counter = 0;

        protected override void Awake()
        {
            if (Instance != null)
            {
                return;
            }
            base.Awake();
            DontDestroyOnLoad(gameObject);
            gameObject.SetActive(false);
        }

        public void ShowMessage(string text, int seconds = 3)
        {
            if (!gameObject.activeInHierarchy)
            {
                gameObject.SetActive(true);
                message.text = text;
                timeToDismiss = seconds;
            }
        }

        public void Dismiss()
        {
            gameObject.SetActive(false);
            counter = 0;
            message.text = "";
        }

        private void Update()
        {
            if (gameObject.activeInHierarchy)
            {
                counter += Time.deltaTime;
                // Dismiss after dismiss time
                if(counter >= timeToDismiss)
                {
                    Dismiss();
                }
            }
        }
    }
}
