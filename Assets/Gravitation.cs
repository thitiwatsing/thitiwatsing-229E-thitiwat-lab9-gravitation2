using System.Collections.Generic;
using UnityEngine;

public class Gravitation : MonoBehaviour
{
    public static List<Gravitation> otherObjects;
    private Rigidbody rb;
    const float G = 0.00673f;

    [SerializeField] bool planet = false;
    [SerializeField] int orbitSpeed = 1000;

    void Awake()
    {
        rb= GetComponent<Rigidbody>();
        if (otherObjects == null) {otherObjects = new List<Gravitation>();
        }
        otherObjects.Add(this);

        if (!planet)
        { rb.AddForce(Vector3.left * orbitSpeed); }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (Gravitation obj in otherObjects)
        {
            if (obj != this) //เช็คว่าต้องไม่ใช่วัตถุตนเอง เพื่อไม่ให้เกิดแรงดึงดูดตนเอง
            {
                AttractionForce(obj);
            }    
        }
    }
    void AttractionForce(Gravitation other)
    {
        Rigidbody otherRb = other.rb;
        Vector3 direction = rb.position - otherRb.position;
        float distance = direction.magnitude;
        if (distance == 0f) return;
        // สูตรแรงดึงดูด G = (m1 * m2)/r2
        float forceMagnitude = G * ((rb.mass * otherRb.mass) / Mathf.Pow(distance, 2));
        Vector3 gravitionalForce = forceMagnitude * direction.normalized;
        otherRb.AddForce(gravitionalForce);
        }
    }
