using System;
using UnityEngine;

namespace Platform
{
    public class Gate : MonoBehaviour
    {
        public VoidEventChannel gateTriggerEvent;
        public void OnEnable()
        {
            gateTriggerEvent.AddListener(Open);
        }

        public void OnDisable()
        {
            gateTriggerEvent.RemoveListener(Open);
        }

        private void Open()
        {
            Destroy(gameObject);
        }
    }
}