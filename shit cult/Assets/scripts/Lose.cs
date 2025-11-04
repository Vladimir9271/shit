using UnityEngine;

public class Lose : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] loseScripts;
    [SerializeField] public Transform TPpositionCamera;
    [SerializeField] public GameObject Info;
    [SerializeField] public float speed = 5f;
    [SerializeField] public GameObject startPosition;   // Начальная позиция (например, справа за экраном)
    [SerializeField] public Vector3 targetPosition;  // Конечная позиция (например, середина экрана)
    private bool isSliding = false;
    [SerializeField] public GameObject Pivot;

    public void Start()
    {
        foreach (MonoBehaviour script in loseScripts)
        {
            if (script.enabled)
                script.enabled = false;
        }
        Camera mainCam = Camera.main;
        Vector3 cameraPos = TPpositionCamera.position;
        cameraPos.z = -5;
        mainCam.transform.position = cameraPos;
        Pivot.transform.position = new Vector3(0, 10000, 0);
        isSliding = true;

    }
    void Update()
    {
        if (isSliding)
        {
            Info.transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                Info.transform.position = targetPosition;
                isSliding = false;
            }
        }
    }
}
