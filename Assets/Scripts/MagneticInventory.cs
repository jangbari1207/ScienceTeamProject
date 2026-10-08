using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MagneticInventory : MonoBehaviour
{
    [SerializeField] private float fireInterval = 0.05f; // 다중 발사 연사 간격

    private List<MagneticObject> capturedObjects = new List<MagneticObject>();
    private PlayerPolarity playerPolarity;

    private void Awake()
    {
        playerPolarity = GetComponent<PlayerPolarity>();
    }

    private void Update()
    {
        AutoFireMatchingPolarity();
    }

    public void Add(MagneticObject magneticObject)
    {
        if (!capturedObjects.Contains(magneticObject))
        {
            capturedObjects.Add(magneticObject);
        }
    }

    private void AutoFireMatchingPolarity()
    {
        if (capturedObjects.Count == 0 || playerPolarity == null)
            return;

        Polarity currentPolarity = playerPolarity.CurrentPolarity;

        Vector2 mousePosition = Vector2.zero;
        if (Mouse.current != null)
        {
            mousePosition = Mouse.current.position.ReadValue();
        }
        Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 fireDirection = (worldMousePosition - (Vector2)transform.position).normalized;

        List<MagneticObject> objectsToFire = new List<MagneticObject>();

        for (int i = capturedObjects.Count - 1; i >= 0; i--)
        {
            if (capturedObjects[i] != null && capturedObjects[i].Polarity == currentPolarity)
            {
                objectsToFire.Add(capturedObjects[i]);
                capturedObjects.RemoveAt(i);
            }
        }

        if (objectsToFire.Count > 0)
        {
            StartCoroutine(FireRoutine(objectsToFire, fireDirection));
        }
    }
    private IEnumerator FireRoutine(List<MagneticObject> objects, Vector2 direction)
    {
        foreach (MagneticObject obj in objects)
        {
            if (obj != null)
            {
                obj.Fire(direction);
                yield return new WaitForSeconds(fireInterval);
            }
        }
    }
}
