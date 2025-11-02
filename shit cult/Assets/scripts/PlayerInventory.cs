using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    [Header("Настройки")]
    [SerializeField] private GameObject[] itemPrefabs; // Префабы товаров
    [SerializeField] private Vector3 offset = new Vector3(1f, 0.5f, 0f); // Смещение от игрока
    [SerializeField] private float followSpeed = 5f;   // Скорость следования
    [SerializeField] private float hoverAmplitude = 0.2f; // Амплитуда “парения”
    [SerializeField] private float hoverFrequency = 2f;   // Частота “парения”

    private int currentItemIndex = -1;
    private GameObject heldItemObject;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        // Взятие товара
        if (Input.GetKeyDown(KeyCode.Alpha1)) TryTakeItem(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) TryTakeItem(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) TryTakeItem(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) TryTakeItem(3);

        UpdateItemFollow();
    }

    public void TryTakeItem(int index)
    {
        if (currentItemIndex != -1)
        {
            Debug.Log("Уже несем товар, нельзя взять другой.");
            return;
        }

        if (index < 0 || index >= itemPrefabs.Length) return;

        heldItemObject = Instantiate(itemPrefabs[index], transform.position + offset, Quaternion.identity);
        currentItemIndex = index;
        Debug.Log($"Взяли товар {index}");
    }

    private void UpdateItemFollow()
    {
        if (heldItemObject == null) return;

        // Целевая позиция (рядом с игроком)
        Vector3 targetPos = transform.position + offset;

        // Добавляем эффект парения
        targetPos.y += Mathf.Sin(Time.time * hoverFrequency) * hoverAmplitude;

        // Плавное движение к цели
        heldItemObject.transform.position = Vector3.Lerp(
            heldItemObject.transform.position,
            targetPos,
            Time.deltaTime * followSpeed
        );
    }

    public bool HasItem => currentItemIndex != -1;
    public int CurrentItemIndex => currentItemIndex;

    public void RemoveHeldItem()
    {
        if (heldItemObject != null) Destroy(heldItemObject);
        heldItemObject = null;
        currentItemIndex = -1;
    }
}
