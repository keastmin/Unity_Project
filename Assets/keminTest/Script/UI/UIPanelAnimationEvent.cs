using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace keastmin
{
    public class UIPanelAnimationEvent : MonoBehaviour
    {
        public void OnUIObjectSetFalse()
        {
            gameObject.SetActive(false);
        }
    }
}