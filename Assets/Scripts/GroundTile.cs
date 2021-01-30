using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using UnityEngine;

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
    [SerializeField] private List<Vector3> possibleNeighborsPosition = new List<Vector3>();
    [SerializeField] private int id;
    [SerializeField] private bool isActive;
    private Renderer thisRenderer;
    private List<GameObject> listLight = new List<GameObject>();
    private Camera mainCamera;
    private MaterialPropertyBlock propertyBlock;
    [SerializeField] private List<GroundTile> neighbors;
    private static readonly int COLOR = Shader.PropertyToID("_Color");
    private Color baseColor;
    private bool baseColorSet;

    public void SetFloorManager(FloorManager floorManager)
    {
        this.floorManager = floorManager;
    }

    private void OnDisable()
    {
        isActive = false;
    }

    private void OnEnable()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }

    public void Activate()
    {
        isActive = true;
    }

    public bool IsActive()
    {
        return isActive;
    }

    public int GetId()
    {
        return id;
    }

    public void SetID(int x)
    {
        id = x;
    }

    // Start is called before the first frame update
    private void Start()
    {
        thisRenderer = GetComponent<Renderer>();
        haloToSelect = GetComponent<ParticleSystem>();
        mainCamera = Camera.main;
        propertyBlock = new MaterialPropertyBlock();
        baseColor = thisRenderer.material.color;
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

        if (other.CompareTag("Player"))
        {
            floorManager.SetPlayerTile(this);
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
    public void ActivateSwitches()
    {
        foreach (Mechanism mechanism in mechanisms)
        {
            if (mechanism is Switch)
            {
                mechanism.ActivateMechanism();
            }
        }
    }
    public void DeactivateSwitches()
    {
        foreach (Mechanism mechanism in mechanisms)
        {
            if (mechanism is Switch)
            {
                mechanism.DeactivateMechanism();
            }
        }
    }
    public void FindNeighbors()
    {
        foreach (GroundTile neighborToFind in possibleNeighborsPosition.Select(possibleNeighborPosition =>
            floorManager.GetTileByPosition(possibleNeighborPosition + transform.position)).Where(neighborToFind => neighborToFind))
        {
            neighbors.Add(neighborToFind);
        }
    }

    public List<GroundTile> GetAllNeighbors()
    {
        return neighbors.Where(n => n.IsActive()).ToList();
    }

    public void SetIsPath()
    {
        thisRenderer.GetPropertyBlock(propertyBlock);
        propertyBlock.SetColor(COLOR, new Color(0, 0.5f, 0));
        thisRenderer.SetPropertyBlock(propertyBlock);
    }

    public void UnSetPathPathColor()
    {
        thisRenderer.GetPropertyBlock(propertyBlock);
        propertyBlock.SetColor(COLOR, baseColor);
        thisRenderer.SetPropertyBlock(propertyBlock);
    }
}
