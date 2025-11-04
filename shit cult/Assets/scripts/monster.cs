using UnityEngine;
using System.Collections;

public class monster : Interactable
{
    public Player playerScript;
    public PlayerInventory playerInventoryScript;
    [SerializeField] public Transform TPpositionPlayer;
    [SerializeField] public float cooldown = 2f;
    public bool work;

    [SerializeField] private Animator anim;
    private bool Work
    {
        get { return anim.GetBool("Work"); }
        set { anim.SetBool("Work", value); }
    }
    void Update()
    {
        Work = work;
    }
    public override void Use()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        playerScript = playerObj.GetComponent<Player>();
        playerInventoryScript = playerObj.GetComponent<PlayerInventory>();
        playerScript.isWork = true;
        work = playerScript.isWork;
        playerScript.speed = 0;
        playerObj.transform.position = TPpositionPlayer.position;
        StartCoroutine(Working());
    }
    private IEnumerator Working()
    {
        Debug.Log("Действие началось");
        yield return new WaitForSeconds(cooldown);
        Debug.Log("Действие завершено");
        playerScript.isWork = false;
        work = playerScript.isWork;
        playerInventoryScript.TryTakeItem(1);
    }
}
