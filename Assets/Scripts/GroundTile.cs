using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class GroundTile : MonoBehaviour
{
    [SerializeField] private bool isEnlighten = true;
    [SerializeField] private bool isForceEnlighten = true;
    [SerializeField] private bool isSelected;
    [SerializeField] private bool isForceDeactivate;
    [SerializeField] private ParticleSystem haloToSelect;
    [SerializeField] private FloorManager floorManager;
    [SerializeField] private List<Mechanism> mechanisms = default;
    [SerializeField] private List<Vector3> possibleNeighborsPosition = new List<Vector3>();
    [SerializeField] private int id;
    [SerializeField] private bool isActive;
    private Renderer thisRenderer;
    [SerializeField] private List<GameObject> listLight = new List<GameObject>();
    private Camera mainCamera;
    private MaterialPropertyBlock propertyBlock;
    [SerializeField] private List<GroundTile> neighbors;
    private static readonly int COLOR = Shader.PropertyToID("_BaseColor");
    private Color baseColor;
    private bool baseColorSet;
    private bool isPlayerOnTile;
    private bool isRemoving;

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
        return !isForceDeactivate && isActive;
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
        //haloToSelect = GetComponent<ParticleSystem>();
        mainCamera = Camera.main;
        propertyBlock = new MaterialPropertyBlock();
        baseColor = thisRenderer.material.color;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!isForceEnlighten)
        {
            UpdateRenderer();
        }
        else
        {
            foreach (Renderer componentsInChild in GetComponentsInChildren<Renderer>())
            {
                componentsInChild.enabled = true;
            }
            thisRenderer.enabled = true;
            isActive = true;
        }

        if(isPlayerOnTile)
        {
            SetDefaultColor();
        }
    }

    private void UpdateRenderer()
    {
        UpdateListLight();
        switch (isEnlighten || isPlayerOnTile)
        {
            case true when !thisRenderer.enabled:
                foreach (Renderer componentsInChild in GetComponentsInChildren<Renderer>())
                {
                    componentsInChild.enabled = true;
                }
                thisRenderer.enabled = true;
                isActive = true;
                break;
            case false when thisRenderer.enabled:
                foreach (Renderer componentsInChild in GetComponentsInChildren<Renderer>())
                {
                    componentsInChild.enabled = false;
                }
                thisRenderer.enabled = false;
                isActive = false;
                SetDefaultColor();
                haloToSelect.Clear();
                haloToSelect.Stop();
                break;
        }
    }

    private void UpdateListLight()
    {
        List<GameObject> LightToRemove = new List<GameObject>(listLight.Where(l => !l.activeSelf).ToList());
        foreach (GameObject light in LightToRemove)
        {
            RemoveLight(light);
        }
        if (listLight.Count == 0)
        {
            isEnlighten = false;
        }
    }

    private void RemoveLight(GameObject light)
    {
        if(listLight.Contains(light))
            listLight.Remove(light);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Light"))
        {
            listLight.Add(other.gameObject);
            isEnlighten = true;
        }

        if (other.CompareTag("Player"))
        {
            isPlayerOnTile = true;
            floorManager.SetPlayerTile(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Light"))
        {
            RemoveLight(other.gameObject);
        }

        if (other.CompareTag("Player"))
        {

            isPlayerOnTile = false;
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
    public void ActivateMechanisms()
    {
        foreach (Mechanism mechanism in mechanisms)
        {
            mechanism.ActivateMechanism();
        }
    }
    public void DeactivateMechanisms()
    {
        foreach (Mechanism mechanism in mechanisms)
        {
            mechanism.DeactivateMechanism();
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

    public void SetPreSelectedColor()
    {
        if(!isPlayerOnTile)
        {
            thisRenderer.GetPropertyBlock(propertyBlock);
            propertyBlock.SetColor(COLOR, new Color(0, 0.5f, 0));
            thisRenderer.SetPropertyBlock(propertyBlock);
        }
    }

    public void SetDefaultColor()
    {
        thisRenderer.GetPropertyBlock(propertyBlock);
        propertyBlock.SetColor(COLOR, baseColor);
        thisRenderer.SetPropertyBlock(propertyBlock);
    }

    public void ForceEnlighten()
    {
        isForceEnlighten = true;
    }

    public void StopParticle()
    {
        haloToSelect.Clear();
        haloToSelect.Stop();
    }


    public void PlayParticle()
    {
        haloToSelect.Play();
    }
}
