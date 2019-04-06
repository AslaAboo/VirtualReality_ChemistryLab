using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Interactive_isKinematic : Element
{

    // Members available in the Inspector
    public bool useGravity = true;

    // Collision-related members
    public bool collisionEntered { get; protected set; }
    public bool collisionOngoing { get; protected set; }
    public bool collisionExited { get; protected set; }
    public List<Collision> enteredCollisions { get; protected set; }
    public List<Collision> ongoingCollisions { get; protected set; }
    public List<Collision> exitedCollisions { get; protected set; }

    // Private state-related variables
    protected bool onCollision;

    // Called at the end of the program initialization
    protected override void Start()
    {

        // Create collision states
        onCollision = collisionEntered = collisionOngoing = collisionExited = false;
        // Create collision lists
        enteredCollisions = new List<Collision>();
        ongoingCollisions = new List<Collision>();
        exitedCollisions = new List<Collision>();

        // Call update to set the appropriate settings
        Update();
    }

    // Updates the behaviors of the element's rigidbody and colliders
    protected override void UpdateBehaviors()
    {

        // Ensure physics control the rigidbody
        rigidbody.isKinematic = true;
        // Check for a change in gravity and set it
        if (rigidbody.useGravity != useGravity)
        {
            rigidbody.useGravity = useGravity;
        }

        // Ensure all the colliders are set for collisions
        Collider[] colliders = gameObject.GetComponents<Collider>();
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].isTrigger = false;
        }
    }

    // FixedUpdate is not called every graphical frame but rather every physics frame
    protected virtual void FixedUpdate()
    {

        // OnCollision state has not been reset yet because FixedUpdate occurs first
        onCollision = false;
    }

    // Called first by every OnCollision function
    protected virtual void OnCollisionUpdate()
    {

        // If an OnCollision function has not already been called this physics frame
        if (!onCollision)
        {
            // One has now been called
            onCollision = true;
            // Reset collision states
            collisionEntered = collisionOngoing = collisionExited = false;
            // Clear previous collisions entered
            enteredCollisions.Clear();
            // Clear previous collisions ongoing
            ongoingCollisions.Clear();
            // Clear previous collisions exited
            exitedCollisions.Clear();
        }
    }

    // Called when a collider has begun touching another collider
    protected virtual void OnCollisionEnter(Collision collision)
    {

        // Update all the states
        OnCollisionUpdate();
        // Update the current state value
        collisionEntered = true;
        // Keep track of the current collision
        enteredCollisions.Add(collision);
    }

    // Called once per frame for every collider touching another collider
    protected virtual void OnCollisionStay(Collision collision)
    {

        // Update all the states
        OnCollisionUpdate();
        // Update the current state value
        collisionOngoing = true;
        // Keep track of the current collision
        ongoingCollisions.Add(collision);
    }

    // Called when a collider has stopped touching another collider
    protected virtual void OnCollisionExit(Collision collision)
    {

        // Update all the states
        OnCollisionUpdate();
        // Update the current state value
        collisionExited = true;
        // Keep track of the current collision
        exitedCollisions.Add(collision);
    }
}