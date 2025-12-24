using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    private Interactable currentInteractable;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Interactable interactable=other.GetComponent<Interactable>();
        if(interactable!=null)
        {
            currentInteractable=interactable;
        }
    }

    public void OnInteract(InputValue value)
    {
        if(currentInteractable!=null)
        {
            currentInteractable.Interact();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Interactable interactable=other.GetComponent<Interactable>();
        if(interactable!=null && interactable==currentInteractable)
        {
            currentInteractable=null;
        }
    }
}
