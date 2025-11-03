using UnityEngine;
using System.Collections;

public class Upgrader : Interactable
{
    public PlayerInventory playerScript;
    [SerializeField] public Transform TPpositionPlayer;
    [SerializeField] public float cooldown = 10f;
    public override void Use()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        playerScript = playerObj.GetComponent<PlayerInventory>();
        for (int i = 0; i < playerScript.heldItems.Count; i++)
        {
        }
            StartCoroutine(Working());
    }
    private IEnumerator Working()
    {
        Debug.Log("Действие началось");
        yield return new WaitForSeconds(cooldown);
        Debug.Log("Действие завершено");
    }
}
