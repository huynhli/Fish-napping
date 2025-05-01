using UnityEngine;
using Cinemachine;

public class VirtualCamera : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float offsetX = 2f;

    private CinemachineFramingTransposer transposer;
    private Transform camFollowTarget;

    private void Start() {
        if (virtualCamera != null) {
            transposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        }
    }

    private void Update() {
        float direction = transform.localScale.x > 0 ? 1f : -1f;
        transposer.m_TrackedObjectOffset.x = offsetX * direction;
    }
}
