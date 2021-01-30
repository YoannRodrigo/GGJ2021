﻿using UnityEngine;

public class MechanismLight : Mechanism
{

    [SerializeField] private Light _light = default;
    private ObjectPooler _objectPooler;
    private ObjectPoolItem _pooledLightItem = null;

    private void Start()
    {
        _objectPooler = ObjectPooler.GetInstance();
    }

    public override void ActivateMechanism()
    {
        base.ActivateMechanism();
        if (_pooledLightItem == null)
        {
            if (_objectPooler == null)
            {
                Start();
            }
            _pooledLightItem = _objectPooler.GetPooledObjectByTag("Light");
            _pooledLightItem.Object.transform.position = transform.position;
            _pooledLightItem.Object.transform.parent = transform;
            _light = _pooledLightItem.Object.GetComponent<Light>();
        }

        _light.gameObject.SetActive(true);
    }

    public override void DeactivateMechanism()
    {
        base.DeactivateMechanism();
        if (_pooledLightItem == null || _pooledLightItem.Object == null)
        {
            return;
        }

        _pooledLightItem.ReturnItemToPool();
        _pooledLightItem = null;

    }
}