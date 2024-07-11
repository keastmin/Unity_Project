using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace keastmin
{
    public class UpdateFloorUI : MonoBehaviour
    {
        TextMeshProUGUI floorText;
        
        void Start()
        {
            floorText = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Update()
        {
            int floor = MapCreatorTest.instance.GetCurrentActiveFloor();
            floorText.text = floor.ToString();
        }
    }
}