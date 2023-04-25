using UnityEngine;
using UnityEngine.UI;

public class LoadScreenScroller : MonoBehaviour
{
    [SerializeField] private RawImage image;
    [SerializeField] private float _x, _y;

    private void Update() => image.uvRect = new Rect(image.uvRect.position + new Vector2(_x, _y) * Time.fixedUnscaledDeltaTime, image.uvRect.size);
    
    
}
