using System.Collections;
using UnityEngine;

public class JumpingPlatform : MonoBehaviour, IInteractable
{
    [Header("Ref")]
    [SerializeField] private SpriteRenderer Sprite;
    [SerializeField] private Collider2D Collider;
    [Header("Settings")]
    [Tooltip("Script Default : 10.0f")][SerializeField] private float JumpPower = 10.0f;
    [Tooltip("Repeat or not")][SerializeField]private bool isRepeat;

    private void Awake()
    {
        if (Sprite == null)
        {
            Sprite = GetComponentInChildren<SpriteRenderer>();
        }
        if(Collider == null)
        {
            Collider = GetComponent<Collider2D>();
        }
        if(Sprite != null && !isRepeat)
        {
            Sprite.color = Color.purple;
        }
    }

    public void Interact(PlayerInteract Player)
    {
        Rigidbody2D playerRb = Player.GetComponent<Rigidbody2D>();
        if(playerRb != null)
        {
            playerRb.linearVelocityY = JumpPower;
            if (!isRepeat)
            {
                if (Sprite == null || Collider == null) return;
                StartCoroutine(RespawnObejctTime());
            }
        }
    }
    IEnumerator RespawnObejctTime()
    {
        
        Sprite.enabled = false;
        Collider.enabled = false;
        yield return new WaitForSeconds(5.0f);
        Sprite.enabled = true;
        Collider.enabled = true;
    }
}
