using UnityEngine;

public abstract class Interactable : MonoBehaviour {
    public bool useEvents;
    [SerializeField] public string promptMessage;

    public virtual string OnLook() {
        return promptMessage;
    }

    public void BaseInteract(GameObject interactingObject) {
        Interact(interactingObject);
    }

    protected virtual void Interact(GameObject interactingObject) {

    }
}
