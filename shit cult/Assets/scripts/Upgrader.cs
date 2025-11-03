using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrader : Interactable
{
    public PlayerInventory playerScript;
    [SerializeField] public Transform TPpositionPlayer;
    [SerializeField] public float cooldown = 10f;

    private bool isProcessing = false;
    private bool haveResource = false;
    public override void Use()
    {
        if (!isProcessing) {
            GameObject playerObj = GameObject.FindWithTag("Player");
            playerScript = playerObj.GetComponent<PlayerInventory>();
            StartCoroutine(Working());
        }

    }

    int indexUpgrad = 0;
    private IEnumerator Working()
    {
        if (haveResource)
        {
            playerScript.TryTakeItem(indexUpgrad);
            Debug.Log($"заказ с индексом {indexUpgrad} выдан");
            haveResource = false;
        }
        Debug.Log("Действие началось");
        isProcessing = true;
       

        List<int> indices = playerScript.GetCurrentItemIndices();
        int targetIndex = -1;

        // Ищем предмет с индексом 1 с конца
        for (int i = indices.Count - 1; i >= 0; i--)
        {
            if (indices[i] == 0)
            {
                targetIndex = i;
                indexUpgrad = 3;
                break;
            }
            if (indices[i] == 1)
            {
                targetIndex = i;
                indexUpgrad = 2;
                break;
            }
        }

        if (targetIndex == -1)
        {
            Debug.Log("❌ У игрока нет предмета с индексом");
            isProcessing = false;
            yield break;
        }

        // Удаляем предмет
        Debug.Log($"🔻 Удаляем предмет с индексом {(indexUpgrad == 2 ? 1 : 0)}");
        playerScript.RemoveHeldItem(targetIndex);

        yield return new WaitForSeconds(cooldown);

        // Создаём новый предмет с индексом 2
        Debug.Log($"заказ с индексом {indexUpgrad} готов");
        haveResource = true;
        isProcessing = false;
        //playerScript.TryTakeItem(indexUpgrad);

        isProcessing = false;
        Debug.Log("Действие завершено");
    }
}
