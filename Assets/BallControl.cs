using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    public float power = 10f;
    public float maxDrag = 5f;
    public Rigidbody2D rb;
    public LineRenderer lr;
    public ParticleSystem releaseParticles;
    public bool idle = false;

    Vector3 dragStartPos;
    Touch touch;

    // Toggles the time scale between 1 and 0.7
    // whenever the user hits the Fire1 button.

    private float fixedDeltaTime;




    public float slowIntensity = .01f; // Between 1-0

    // Start Idle, Set DeltaTime for slowing
    void Awake()
    {
        // Make a copy of the fixedDeltaTime, it defaults to 0.02f, but it can be changed in the editor
        this.fixedDeltaTime = Time.fixedDeltaTime;

        idle = true;
    }

    private void Update()
    {

        // Touch Input
        if (Input.touchCount > 0) // If there are touchs
        {
            touch = Input.GetTouch(0); // Get input from 1 finger
       
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    DragStart();
                    break;
       
                case TouchPhase.Moved:
                    Dragging();
                    break;
       
                case TouchPhase.Ended:
                    DragRelease();
                    break;
       
                default:
                    break;
            }
       
        }

        // If Idle, set velocity to 0
        if (idle)
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
        
    }

    // States of Touch Controls
    void DragStart()
    {
        // Get Drag start position
        dragStartPos = Camera.main.ScreenToWorldPoint(touch.position);
        dragStartPos.z = 0f; // Stop 3D dragging

        // Get Line Renderer start position
        lr.positionCount = 1;
        lr.SetPosition(0, dragStartPos);

    }    

    void Dragging()
    {

        Vector3 draggingPos = Camera.main.ScreenToWorldPoint(touch.position);
        draggingPos.z = 0f;
        lr.positionCount = 2;
        lr.SetPosition(1, draggingPos);

        // Slow Time
        Time.timeScale = slowIntensity;

        // Adjust fixed delta time according to timescale
        // The fixed delta time will now be 0.02 frames per real-time second
        Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
    }

    void DragRelease()
    {
        // If idle, no longer
        if (idle) idle = false;

        // Resume normal time
        Time.timeScale = 1.0f;

        // Delete lineRenderer line
        lr.positionCount = 0;

        // Get Last touch position
        Vector3 dragReleasePos = Camera.main.ScreenToWorldPoint(touch.position);
        dragReleasePos.z = 0f;

        // Calculate clamped Force to apply to Rigidbody
        Vector3 force = dragStartPos - dragReleasePos;
        Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;

        // Reset Velocity
        rb.velocity = new Vector3(0f, 0f, 0f);

        // Add the Force
        rb.AddForce(clampedForce, ForceMode2D.Impulse);


        //releaseParticles.transform.position = dragStartPos;
        releaseParticles.Play();


        // Choose random Pop sound
        int rand = Random.Range(1, 3);
        switch (rand)
        {
            case 1:
                SoundManager.PlaySound("pop1");
                break;

            case 2:
                SoundManager.PlaySound("pop2");
                break;

            case 3:
                SoundManager.PlaySound("pop3");
                break;

            default:
                Debug.Log(rand);
                break;
        }

        
    }

    // Colliding with RigidBodies
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Choose random Bounce sound
        int rand = Random.Range(1, 2);
        switch (rand)
        {
            case 1:
                SoundManager.PlaySound("pop1");
                break;
        
            case 2:
                SoundManager.PlaySound("pop2");
                break;
        }
        
    }


    
    
    
    
        
}
