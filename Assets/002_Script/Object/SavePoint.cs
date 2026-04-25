using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private Sprite SavedSprite;
    [SerializeField] private SpriteRenderer MySpriteRenderer;
    [SerializeField] private LayerMask TargetLayer;
    [SerializeField] private bool Active;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Active) return;
        if(SavePointManager.Instance != null && SavedSprite != null)
        {
            if(((1 << collision.gameObject.layer) & TargetLayer) != 0) 
            {
                SavePointManager.Instance.SavePointTrigger(this.transform.position);
                Active = true;
                if(MySpriteRenderer != null)
                {
                    MySpriteRenderer.sprite = SavedSprite;
                }
            }
        }
    }
}
