using UnityEngine;

public class MechanismLight : Mechanism
{

    [SerializeField] private Light _light = default;
    [SerializeField] private bool poolIsNeeded;

    private ObjectPooler _objectPooler;
    private ObjectPoolItem _pooledLightItem = null;
    [SerializeField] private SoundManager _soundManager;


    public override void Start()
    {
        base.Start();
        _objectPooler = ObjectPooler.GetInstance();
        _soundManager = SoundManager.instance;
    }

    public override void ActivateMechanism()
    {
        base.ActivateMechanism();
        SoundManager.instance.PlayRandomSound(new string[]{"SpotLight_On_1","SpotLight_On_2","SpotLight_On_3","SpotLight_On_4","SpotLight_On_5"});
        if (poolIsNeeded)
        {
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