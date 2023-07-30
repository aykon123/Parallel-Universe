using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    public Vector2 minPosition; // Die minimale Position der Kamera im Level
    public Vector2 maxPosition; // Die maximale Position der Kamera im Level

    void LateUpdate()
    {
        // Begrenze die x- und y-Koordinaten der Kamera-Position innerhalb der Grenzen
        float clampedX = Mathf.Clamp(transform.position.x, minPosition.x, maxPosition.x);
        float clampedY = Mathf.Clamp(transform.position.y, minPosition.y, maxPosition.y);

        // Setze die begrenzten x- und y-Koordinaten für die Kamera-Position
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
