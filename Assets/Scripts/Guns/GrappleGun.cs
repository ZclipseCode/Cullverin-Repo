using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GrappleGun : MonoBehaviour
{
    LineRenderer lr;
    Vector3 grapplePoint;
    [Header("Grapple")]
    [SerializeField] LayerMask whatIsGrappleable;
    [SerializeField] Transform ropePos, cam, player;
    [SerializeField] float maxDistance;
    SpringJoint joint;

    [Header("Reset")]
    [SerializeField] int maxGrapples;
    int grapplesLeft;
    bool grappling = false;
    bool once = false;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject reset;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] PlayerMovement pm;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Start()
    {
        grapplesLeft = maxGrapples;
    }

    void Update()
    {
            if (Input.GetMouseButtonDown(1) && grapplesLeft > 0)
            {
                StartGrapple();
                grappling = true;
            }
            else if (Input.GetMouseButtonUp(1))
            {
                StopGrapple();
                grappling = false;
                once = false;
            }

        ResetGrappleCount();
    }

    // called after Update()
    private void LateUpdate()
    {
        DrawRope();
    }

    // call whenever we want to start a grapple
    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            // the distance grapple will try to keep from grapple point
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            // change these values to fit your game
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
        }
        else
        {
            // if missed
            grapplesLeft++;
        }
    }

    void DrawRope()
    {
        // if not grappling, don't draw rope
        if (!joint)
        {
            return;
        }

        lr.SetPosition(0, ropePos.position);
        lr.SetPosition(1, grapplePoint);
    }

    // call whenever we want to stop a grapple
    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }

    void ResetGrappleCount()
    {
        text.SetText(grapplesLeft + " / " + maxGrapples);

        if (grappling && !once)
        {
            grapplesLeft--;
            once = true;
        }

        if (!grappling && pm.grounded)
        {
            grapplesLeft = maxGrapples;
        }
    }
}
