using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractable Object = collision.GetComponent<IInteractable>();
        if(Object != null )
        {
            Object.Interact(this);
        }
    }
}
