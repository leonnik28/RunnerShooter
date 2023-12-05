using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private int _timeDelay = 1000;
    [SerializeField] private Animator _fireGunAnimator;

    private Camera _camera;

    private bool _isDelayInProgress = false;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {
        Ray ray = _camera.ViewportPointToRay(Vector2.one / 2);

        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

        TryShoot();
    }

    private async void TryShoot()
    {

        Ray ray = _camera.ViewportPointToRay(Vector2.one / 2);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.TryGetComponent(out IDamageable damageable))
            {
                if (_isDelayInProgress == false)
                {
                    _isDelayInProgress = true;
                    _fireGunAnimator.SetTrigger("StartFireAnimation");
                    await Task.Delay(_timeDelay);

                    damageable.GetDamage();
                    _isDelayInProgress = false;
                }
            }
        }
    }

}

