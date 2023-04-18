using UnityEngine;

public abstract class Interactable : MonoBehaviour {
    public bool useEvents;
    [SerializeField] public string promptMessage;

    public virtual string OnLook() {
        return promptMessage;
    }

    public void BaseInteract(GameObject interactingObject, InteractionType interactionType) {
        Interact(interactingObject, interactionType);
    }

    protected virtual void Interact(GameObject interactingObject, InteractionType interactionType) {

    }
}
