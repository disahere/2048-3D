using System.Collections.Generic;
using CodeBase.Game;
using UnityEngine;

namespace CodeBase.Handlers
{
    public class CubeMaterialHandler : MonoBehaviour
    {
        private Renderer _cubeRenderer;
        private Dictionary<CubeValue, Color> _colorMapping;

        private void Awake()
        {
            _cubeRenderer = GetComponent<Renderer>();

            _colorMapping = new Dictionary<CubeValue, Color>
            {
                { CubeValue.Two, Color.blue },
                { CubeValue.Four, new Color(0, 0, 0.5f) },
                { CubeValue.Eight, Color.green },
                { CubeValue.Sixteen, new Color(1, 0.5f, 0) },
                { CubeValue.ThirtyTwo, new Color(0.5f, 0, 0.5f) },
                { CubeValue.SixtyFour, Color.red }
            };
        }

        public void SetMaterial(int value)
        {
            var cubeValue = (CubeValue)value;

            if (_colorMapping.TryGetValue(cubeValue, out var color)) 
                _cubeRenderer.material.color = color;
        }
    }
}