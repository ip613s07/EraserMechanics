using UnityEngine;

namespace Plugin.EraserMechanic.Core.Scripts
{
    using System;

    public sealed class ErasablePlane : MonoBehaviour
    {
        public event Action OnInited;
        
        public bool IsInited { get; private set; }
        
        public Texture2D ErasedTexture => eraser.ErasedTexture;
        
        [SerializeField]
        private Eraser eraser;

        [SerializeField]
        private Pointer pointer;
        
        private void OnEnable()
        {
            Init();

            pointer.OnChangedPointerPosition += Erase;
        }

        private void OnDisable()
        {
            pointer.OnChangedPointerPosition -= Erase;
        }

        private void Init()
        {
            if (IsInited)
            {
                return;
            }

            eraser.Init();

            IsInited = true;
            OnInited?.Invoke();
        }

        private void OnMouseOver()
        {
            pointer.CheckPointerPosition();
        }

        private void Erase()
        {
            var localPoint = transform.InverseTransformPoint(pointer.PointerPosition);
            
            eraser.EraseInPoint(localPoint);
        }
    }
}