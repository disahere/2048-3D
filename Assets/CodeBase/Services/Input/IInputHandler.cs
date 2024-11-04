using UnityEngine;

namespace CodeBase.Services.Input
{
    internal interface IInputHandler
    {
        void OnTouchBegin(Vector2 position);
        void OnTouchMove(Vector2 position);
        void OnTouchEnd(Vector2 position);
    }
}