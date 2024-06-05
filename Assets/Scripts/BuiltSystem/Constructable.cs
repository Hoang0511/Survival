using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constructable : MonoBehaviour
{
    public bool isGrounded;
    public bool isOverLappingItems;
    public bool isValidToBeBuilt;
    public bool detectedGhostMember;

    private Renderer mRenderer;
    public Material redMaterial;
    public Material greenMaterial;
    public Material defaultMaterial;

    public List<GameObject> ghostList = new List<GameObject>();

    public BoxCollider solidCollider;


    private void Start()
    {
        mRenderer = GetComponent<Renderer>();
        mRenderer.material = defaultMaterial;
        foreach (Transform child in transform)
        {
            ghostList.Add(child.gameObject);
        }
    }
    private void Update()
    {
        if (isGrounded && isOverLappingItems == false)
        {
            isValidToBeBuilt = true;
        }
        else
        {
            isValidToBeBuilt = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground") && gameObject.CompareTag("activeConstructable"))
        {
            isGrounded = true;
        }
        if (other.CompareTag("Tree") || other.CompareTag("pickable") && gameObject.CompareTag("activeConstructable"))
        {
            isOverLappingItems = true;
        }
        if (other.gameObject.CompareTag("ghost") && gameObject.CompareTag("activeConstructable"))
        {
            detectedGhostMember = true;
        }



    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground") && gameObject.CompareTag("activeConstructable"))
        {
            isGrounded = false;
        }
        if (other.CompareTag("Tree") || other.CompareTag("pickable") && gameObject.CompareTag("activeConstructable"))
        {
            isOverLappingItems = false;
        }
        if (other.gameObject.CompareTag("ghost") && gameObject.CompareTag("activeConstructable"))
        {
            detectedGhostMember = false;
        }
    }


    public void SetInValidColor()
    {
        if(mRenderer != null)
        {
            mRenderer.material = redMaterial;
        }
    }


    public void SetValidColor()
    {
        mRenderer.material = greenMaterial;
    }


    public void SetDefaultColor()
    {
        mRenderer.material = defaultMaterial;
    }

    public void ExtractGhostMembers()
    {
        foreach(GameObject item in ghostList)
        {
            item.transform.SetParent(transform.parent, true);
            item.gameObject.GetComponent<GhostItem>().solidCollider.enabled = false;
            item.gameObject.GetComponent<GhostItem>().isPlaced = true;
        }
    }


}
