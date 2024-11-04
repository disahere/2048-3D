using TMPro;
using UnityEngine;

namespace CodeBase.Handlers
{
    public class CubeValueHandler : MonoBehaviour
    {
        [SerializeField] private TextMeshPro[] texts;

        public void SetCubeValue(int value)
        {
            var valueText = value.ToString();
            foreach (var text in texts) 
                text.text = valueText;
        }
    }
}