using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostItem : MonoBehaviour
{
    public BoxCollider solidCollider;

    public Renderer mRenderer;
    private Material semiTransparentMat;
    private Material fullTransparentMat;
    private Material selectedMaterial;

    public bool isPlaced;


    public bool hasSamePosition = false;

    private void Start()
    {
        mRenderer = GetComponent<Renderer>();

        semiTransparentMat = ConstructionManager.Instance.ghostSemiTransparentMat;
        fullTransparentMat = ConstructionManager.Instance.ghostFullTransparentMat;
        selectedMaterial = ConstructionManager.Instance.ghostSelectedMat;

        mRenderer.material = fullTransparentMat;

        solidCollider.enabled = false;
    }

    private void Update()
    {

        if(ConstructionManager.Instance.inConstructionMode)
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), ConstructionManager.Instance.player.GetComponent<Collider>());
        }



        if(ConstructionManager.Instance.inConstructionMode && isPlaced)
        {
            solidCollider.enabled = true;
        }
        if (!ConstructionManager.Instance.inConstructionMode)
        {
            solidCollider.enabled = false;

        }
        if (ConstructionManager.Instance.selectedGhost == gameObject)
        {
            mRenderer.material = selectedMaterial;
        }
        else
        {
            mRenderer.material = fullTransparentMat;
        }
    }
}
