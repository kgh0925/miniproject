using System.Collections;
using UnityEngine;

public class EnergyPack : MonoBehaviour, IInteractable
{
    [Header("Ref")]
    [SerializeField] private SpriteRenderer Sprite;
    [SerializeField] private Collider2D Collider;
    [Header("Settings")]
    [SerializeField] private float SpawnDelay = 5.0f;
    public void Interact(PlayerInteract Player)
    {
        PlayerFly player = Player.GetComponent<PlayerFly>();
        if (player == null) return;
        player.TimeAdd();
        StartCoroutine(RespawnObejctTime());
    }

    IEnumerator RespawnObejctTime()
    {

        Sprite.enabled = false;
        Collider.enabled = false;
        yield return new WaitForSeconds(SpawnDelay);
        Sprite.enabled = true;
        Collider.enabled = true;
    }
}
