using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageBox : MonoBehaviour
{
    public bool playerInRange;

    [SerializeField] public List<string> items;

    public enum BoxType
    {
        smallBox,
        bixBox
    }

    public BoxType thisboxType;



    private void Update()
    {
        float distance = Vector3.Distance(PlayerState.Instance.playerBody.transform.position, transform.position);
        if(distance < 10f)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange= false;
        }
    }
}
