using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boss : MonoBehaviour
{
    public bool done = false;
    public Player playerScript;
    public PlayerInventory playerInventoryScript;
    [SerializeField] public GameObject Bed;
    [SerializeField] public GameObject Cabinet;
    [SerializeField] public GameObject Wardrode;
    [SerializeField] public float cooldown = 10f;
    [SerializeField] public float cooldownglobal = 30f;
    [SerializeField] public float animcooldown = 10f;

    private List<System.Func<bool>> conditions = new List<System.Func<bool>>();
    private List<System.Func<bool>> conditionsglobal = new List<System.Func<bool>>();

    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        playerScript = playerObj.GetComponent<Player>();
        playerInventoryScript = playerObj.GetComponent<PlayerInventory>();
        conditions.Add(Condition1);
        conditions.Add(Condition2);
        conditions.Add(Condition3);

        StartCoroutine(Cooldown());
    }

    private bool Condition1()
    {
        if (playerScript.speed == 0)
        {
            return true;
        }
        else return false;
    }

    private bool Condition2()
    {
        if (playerInventoryScript.heldItems.Count != 0)
        {
            return false;
        }
        else return true;
    }

    private bool Condition3()
    {
        if (playerInventoryScript.heldItems.Count >= 3)
        {
            return true;
        }
        else return false;
    }

    private IEnumerator Cooldown()
    {
        while (true)
        {
            Debug.Log("Действие началось");
            yield return new WaitForSeconds(cooldown);

            if (conditions.Count == 0)
            {
                Debug.LogWarning("Список условий пуст.");
                continue;
            }

            int randomIndex = Random.Range(0, conditions.Count);
            bool result = conditions[randomIndex].Invoke();

            Debug.Log($"Проверка условия #{randomIndex + 1}: результат = {result}");

            if (result)
            {
                Debug.Log("Условие выполнено!");
            }
        }

    }
    private IEnumerator CooldownGlobal()
    {
        Debug.Log("Действие началось");
        yield return new WaitForSeconds(cooldownglobal);
        Debug.Log("Действие завершено");


    }
}
