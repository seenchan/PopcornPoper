using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject popcornTrigger;
    private Vector3 startPos;
    private bool isClick;
    private CapsuleCollider trigger;

    // Start is called before the first frame update
    void Start()
    {
        startPos = popcornTrigger.transform.position;
        isClick = false;
        trigger = popcornTrigger.GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.Instance.isRoundEnd == false)
        {
            MouseControl();
            ActivateTrigger();
            TriggerActivator();
        }
    }

    void MouseControl()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);

        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit))
        {
            Vector3 mousePos = new Vector3(hit.point.x, startPos.y, hit.point.z);
            popcornTrigger.transform.position = mousePos;
        }
    }

    void ActivateTrigger()
    {
        if (isClick == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isClick = true;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isClick = false;
        }
    }

    void TriggerActivator()
    {
        if (isClick == true)
        {
            trigger.enabled = true;
        }
        if (isClick == false)
        {
            trigger.enabled = false;
        }
    }
}
