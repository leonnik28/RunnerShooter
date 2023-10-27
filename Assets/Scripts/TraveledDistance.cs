using UnityEngine;

public class TraveledDistance : MonoBehaviour
{
    public float Distance { get; private set; }

    private float _startPositionZ;

    private void Start()
    {
        _startPositionZ = transform.position.z;
    }

    private void LateUpdate()
    {
        Distance = transform.position.z - _startPositionZ;
    }
}
