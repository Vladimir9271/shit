using UnityEngine;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject[] allItems; // 4 возможных товара (префабы)
    [SerializeField] private Transform shopTransform; // объект магазина
    [SerializeField] private float itemSpacing = 1.5f; // рассто€ние между товарами
    [SerializeField] private float heightAboveShop = 2f; // высота над магазином

    private List<GameObject> currentItems = new List<GameObject>();
    int j = 0;

    void Start()
    {
        GenerateRandomItems();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
             var item = currentItems[j];
            
                Destroy(item);
            j++;
            if (j == 3)
            {
                currentItems.Clear();
                j = 0;
            }
        }

        if (currentItems.Count == 0)
        {
            GenerateRandomItems();
        }


    }

    public void GenerateRandomItems()
    {
        // создаЄм список индексов
        List<int> indices = new List<int> { 0, 1, 2, 3 };

        // перемешиваем индексы (чтобы выбрать случайные)
        for (int i = 0; i < indices.Count; i++)
        {
            int rand = Random.Range(i, indices.Count);
            (indices[i], indices[rand]) = (indices[rand], indices[i]);
        }

        // берЄм первые 3 товара
        for (int i = 0; i < 3; i++)
        {
            int index = indices[i];

            // позици€ спавна Ч над магазином с отступом по X
            Vector3 spawnPos = shopTransform.position + new Vector3((i - 1) * itemSpacing, heightAboveShop, 0);

            // создаЄм товар
            GameObject newItem = Instantiate(allItems[index], spawnPos, Quaternion.identity);
            currentItems.Add(newItem);
        }
    }
}
