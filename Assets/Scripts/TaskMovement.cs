using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M : MonoBehaviour
{
    public GameObject DarkItems;
    private bool moving;
    private bool finished;
    private float startX;
    private float startY;
    private Vector3 resetPosition;
    // Start is called before the first frame update
    void Start()
    {
        resetPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (!finished)
        {

            if (moving)
            {
                Vector3 pos;
                pos = Input.mousePosition;
                pos.z = 10;
                pos = Camera.main.ScreenToWorldPoint(pos);
                this.gameObject.transform.localPosition = new Vector3(pos.x - startX, pos.y - startY, this.gameObject.transform.localPosition.z);
            }
        }
    }
    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector3 pos;
            pos = Input.mousePosition;
            pos.z = 10;
            pos = Camera.main.ScreenToWorldPoint(pos);
            startX = pos.x - transform.localPosition.x;
            startY = pos.y - transform.localPosition.y;
            moving = true;
        }
    }
    private void OnMouseUp()
    {
        moving = false;
        if (Mathf.Abs(transform.localPosition.x - DarkItems.transform.localPosition.x) <= 0.5f &&
            Mathf.Abs(transform.localPosition.y - DarkItems.transform.localPosition.y) <= 0.5f)
        {
            this.transform.localPosition = new Vector3(DarkItems.transform.position.x, DarkItems.transform.position.y, DarkItems.transform.position.z);
            finished = true;
            GameObject.Find("PointHandler").GetComponent<WinTask>().AddPoints();
        }
        else
        {
            transform.localPosition = new Vector3(resetPosition.x, resetPosition.y, resetPosition.z);
        }
    }
}
