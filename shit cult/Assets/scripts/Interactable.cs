using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool isInteractable = true;
    public virtual void Use()
    {
        Debug.Log("Объект " + gameObject.name + " использован");
    }
}
