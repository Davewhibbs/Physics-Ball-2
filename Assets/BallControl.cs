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

    Vector3 dragStartPos;
    Touch touch;

    public float slowIntensity = .01f; // Between 1-0

    private void Update()
    {
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
    }

    void DragStart()
    {
        dragStartPos = Camera.main.ScreenToWorldPoint(touch.position);
        dragStartPos.z = 0f; // Stop 3D dragging

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
        // This goes in Dragging
        Time.timeScale = slowIntensity;

        // Adjust fixed delta time according to timescale
        // The fixed delta time will now be 0.02 frames per real-time second
        Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;

        Debug.Log(Time.timeScale);
    }

    void DragRelease()
    {
        // Resume normal time
        Time.timeScale = 1.0f;

        lr.positionCount = 0;

        Vector3 dragReleasePos = Camera.main.ScreenToWorldPoint(touch.position);
        dragReleasePos.z = 0f;

        Vector3 force = dragStartPos - dragReleasePos;
        Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;

        rb.AddForce(clampedForce, ForceMode2D.Impulse);

        
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


    // Toggles the time scale between 1 and 0.7
    // whenever the user hits the Fire1 button.

    private float fixedDeltaTime;

    void Awake()
    {
        // Make a copy of the fixedDeltaTime, it defaults to 0.02f, but it can be changed in the editor
        this.fixedDeltaTime = Time.fixedDeltaTime;
    }
    
    
        
}
