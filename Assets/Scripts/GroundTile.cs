using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using mechanism;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(ParticleSystem))]
public class GroundTile : MonoBehaviour
{
    [SerializeField] private bool isEnlighten = true;
    [SerializeField] private bool isForceEnlighten = true;
    [SerializeField] private bool isSelected;
    [SerializeField] private ParticleSystem haloToSelect;
    [SerializeField] private FloorManager floorManager;
    [SerializeField] private List<Mechanism> mechanisms = default;
    private Tuple<int, int> id;
    private Renderer thisRenderer;
    private List<GameObject> listLight = new List<GameObject>();
    private Camera mainCamera;

    public void SetFloorManager(FloorManager floorManager)
    {
        this.floorManager = floorManager;
    }

    public void SetID(int x, int z)
    {
        id = new Tuple<int, int>(x, z);
    }

    // Start is called before the first frame update
    private void Start()
    {
        thisRenderer = GetComponent<Renderer>();
        haloToSelect = GetComponent<ParticleSystem>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        Ray screenRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(screenRay, out RaycastHit resultHit))
        {
            if (resultHit.collider.transform.GetInstanceID() == transform.GetInstanceID())
            {
                haloToSelect.Play();
                if (Input.GetMouseButtonDown(0))
                {
                    if (floorManager.IsTileSelected() && floorManager.GetSelectedTile().GetInstanceID() != gameObject.GetInstanceID())
                    {
                        floorManager.UnSelectTile();
                    }
                    if (isSelected)
                    {
                        floorManager.ValidateTile();
                    }
                    else
                    {
                        isSelected = true;
                        floorManager.TileSelect(this);
                    }
                }
            }
            else
            {
                if (!isSelected)
                {
                    haloToSelect.Clear();
                    haloToSelect.Stop();
                }
            }
        }
        else
        {
            if (!isSelected)
            {
                haloToSelect.Clear();
                haloToSelect.Stop();
            }
        }




        if (!isForceEnlighten)
        {
            UpdateListLight();
            switch (isEnlighten)
            {
                case true when !thisRenderer.enabled:
                    thisRenderer.enabled = true;
                    break;
                case false when thisRenderer.enabled:
                    thisRenderer.enabled = false;
                    break;
            }
        }
    }

    private void UpdateListLight()
    {
        foreach (GameObject light in listLight.Where(light => !light.activeSelf))
        {
            listLight.Remove(light);
        }

        if (listLight.Count == 0)
        {
            isEnlighten = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Light"))
        {
            listLight.Add(other.gameObject);
            print("In");
            isEnlighten = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Light"))
        {
            listLight.Remove(other.gameObject);
            print("Out");
        }
    }

    public void Unselect()
    {
        isSelected = false;
    }
    public void ActivateSwitch()
    {
        foreach (Mechanism mechanism in mechanisms)
        {
            mechanism.ActivateMechanism();
        }
    }
    public void DeactivateSwitch()
    {
        foreach (Mechanism mechanism in mechanisms)
        {
            mechanism.DeactivateMechanism();
        }
    }
}
