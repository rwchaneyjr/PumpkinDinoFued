using UnityEngine;

public class PickUp : MonoBehaviour
{   
    public float pickUpRange = 5f;
    private GameObject heldObj;
    public Transform holdParent;
    public float moveForce = 1550f;
   // public Animator PlayAnim;
  public  void MoveObject()
    {
        if (Vector3.Distance(heldObj.transform.position, holdParent.position) > 3f)
        {
            Vector3 moveDirection = (holdParent.position - heldObj.transform.position);
            heldObj.GetComponent<Rigidbody>().AddForce(-moveDirection * moveForce);
        }
    }
    public void PickupObject(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            Rigidbody objRig = pickObj.GetComponent<Rigidbody>();
            objRig.useGravity = false;
            objRig.drag = 10;
            objRig.transform.parent = holdParent;
            heldObj = pickObj;
        }
    }
      public  void DropObject()
        {
            Rigidbody heldRig = heldObj.GetComponent<Rigidbody>();
            heldRig.useGravity = true;
            heldRig.drag = 1;
            heldObj.transform.parent = null;
            heldObj = null;
        }
    
    void Update()
    {
       // PlayAnim.speed = 0;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObj == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.InverseTransformDirection(Vector3.up), out hit, pickUpRange))
                {
                    PickupObject(hit.transform.gameObject);
                }

                else
                {
                    DropObject();
                }

                if (heldObj != null)
                {
                    MoveObject();
                }
            }
        }
    }
}