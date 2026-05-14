using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    [Header("Dead Zone")]
    public float deadZoneWidth = 2f;
    public float deadZoneHeight = 1f;

    [Header("Smooth")]
    public float smoothSpeed = 5f;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 cameraPos = transform.position;
        Vector3 targetPos = target.position;

        // X축 Dead Zone
        if (Mathf.Abs(targetPos.x - cameraPos.x) > deadZoneWidth)
        {
            cameraPos.x = Mathf.Lerp(
                cameraPos.x,
                targetPos.x,
                smoothSpeed * Time.deltaTime
            );
        }

        // Y축 Dead Zone
        if (Mathf.Abs(targetPos.y - cameraPos.y) > deadZoneHeight)
        {
            cameraPos.y = Mathf.Lerp(
                cameraPos.y,
                targetPos.y,
                smoothSpeed * Time.deltaTime
            );
        }

        // Z축 유지
        cameraPos.z = transform.position.z;

        transform.position = cameraPos;
    }
}
