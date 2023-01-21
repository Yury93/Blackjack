using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Audio
{
  //  [RequireComponent(typeof(Button))]
    public class ButtonSound : MonoBehaviour,IPointerDownHandler
    {

        [SerializeField] Clip clickClip;
        [SerializeField] bool withoutBtn;

        public void OnPointerDown(PointerEventData eventData)
        {
          if(withoutBtn)  SoundCreator.Create(clickClip);
        }

        void Awake()
        {
            if (!withoutBtn)
            {
                var b = GetComponent<Button>();
               
                if (b == null) b= gameObject.AddComponent<Button>();
                b.onClick.AddListener(OnClick);
            }
        }

        private void OnClick()
        {
            SoundCreator.Create(clickClip);
        }

    }
}