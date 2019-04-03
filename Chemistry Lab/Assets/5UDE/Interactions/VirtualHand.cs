/*
Copyright ©2017. The University of Texas at Dallas. All Rights Reserved. 

Permission to use, copy, modify, and distribute this software and its documentation for 
educational, research, and not-for-profit purposes, without fee and without a signed 
licensing agreement, is hereby granted, provided that the above copyright notice, this 
paragraph and the following two paragraphs appear in all copies, modifications, and 
distributions. 

Contact The Office of Technology Commercialization, The University of Texas at Dallas, 
800 W. Campbell Road (AD15), Richardson, Texas 75080-3021, (972) 883-4558, 
otc@utdallas.edu, https://research.utdallas.edu/otc for commercial licensing opportunities.

IN NO EVENT SHALL THE UNIVERSITY OF TEXAS AT DALLAS BE LIABLE TO ANY PARTY FOR DIRECT, 
INDIRECT, SPECIAL, INCIDENTAL, OR CONSEQUENTIAL DAMAGES, INCLUDING LOST PROFITS, ARISING 
OUT OF THE USE OF THIS SOFTWARE AND ITS DOCUMENTATION, EVEN IF THE UNIVERSITY OF TEXAS AT 
DALLAS HAS BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

THE UNIVERSITY OF TEXAS AT DALLAS SPECIFICALLY DISCLAIMS ANY WARRANTIES, INCLUDING, BUT 
NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR 
PURPOSE. THE SOFTWARE AND ACCOMPANYING DOCUMENTATION, IF ANY, PROVIDED HEREUNDER IS 
PROVIDED "AS IS". THE UNIVERSITY OF TEXAS AT DALLAS HAS NO OBLIGATION TO PROVIDE 
MAINTENANCE, SUPPORT, UPDATES, ENHANCEMENTS, OR MODIFICATIONS.
*/

using UnityEngine;
using System.Collections;

public class VirtualHand : MonoBehaviour
{

    // Enumerate states of virtual hand interactions
    public enum VirtualHandState
    {
        Open,
        Touching,
        Holding,
        Closed,
        Displace
    };

    // Inspector parameters
    [Tooltip("The tracking device used for tracking the real hand.")]
    public CommonTracker tracker;

    [Tooltip("The controller joystick used to determine relative direction (forward/backward) and speed.")]
    public CommonAxis joystick;

    [Tooltip("The interactive used to represent the virtual hand.")]
    public Affect hand;

    [Tooltip("The button required to be pressed to grab objects.")]
    public CommonButton button;

    [Tooltip("The button required to be pressed to grab objects.")]
    public CommonButton button1;

    [Tooltip("The speed amplifier for thrown objects. One unit is physically realistic.")]
    public float speed = 1.0f;

    // Private interaction variables
    VirtualHandState state;
    FixedJoint grasp;
    FixedJoint grasp1;

    // Called at the end of the program initialization
    void Start()
    {

        // Set initial state to open
        state = VirtualHandState.Open;

        // Ensure hand interactive is properly configured
        hand.type = AffectType.Virtual;
    }

    // FixedUpdate is not called every graphical frame but rather every physics frame
    void FixedUpdate()
    {

        // If state is open
        if (state == VirtualHandState.Open)
        {

            // If the hand is touching something
            if (hand.triggerOngoing)
            {

                // Change state to touching
                state = VirtualHandState.Touching;
            }

            // If the hand is closed
            else if (button.GetPress())
            {
                // Change state to closed
                state = VirtualHandState.Closed;
                hand.type = AffectType.Physical;
            }
            else
            {
                // do nothing
            }
        }

        //If state is closed
        if (state == VirtualHandState.Closed)
        {
            if (!button.GetPress())
            {
                //change state to open
                state = VirtualHandState.Open;
                hand.type = AffectType.Virtual;
            }
            else
            {
                //do nothing
            }

        }

        // If state is touching
        else if (state == VirtualHandState.Touching)
        {

            // If the hand is not touching something
            if (!hand.triggerOngoing)
            {

                // Change state to open
                state = VirtualHandState.Open;
            }

            // If the hand is touching something and the button is pressed
            else if (hand.triggerOngoing && button.GetPress())
            {

                // Fetch touched target
                Collider target = hand.ongoingTriggers[0];
                // Create a fixed joint between the hand and the target
                grasp = target.gameObject.AddComponent<FixedJoint>();
                // Set the connection
                grasp.connectedBody = hand.gameObject.GetComponent<Rigidbody>();

                // Change state to holding
                state = VirtualHandState.Holding;
            }

            // Process current touching state
            else
            {

                // Nothing to do for touching
            }
        }

        // If state is holding
        else if (state == VirtualHandState.Holding)
        {

            // If grasp has been broken
            if (grasp == null)
            {

                // Update state to open
                state = VirtualHandState.Open;
            }

            // If button has been released and grasp still exists
            else if (!button.GetPress() && grasp != null)
            {

                // Get rigidbody of grasped target
                Rigidbody target = grasp.GetComponent<Rigidbody>();
                // Break grasp
                DestroyImmediate(grasp);

                // Apply physics to target in the event of attempting to throw it
                target.velocity = hand.velocity * speed;
                target.angularVelocity = hand.angularVelocity * speed;

                // Update state to open
                state = VirtualHandState.Open;
            }

            // If displace button is pressed along with grasp and trigger button
            else if (button1.GetPress() && grasp != null && button.GetPress())
            {

                //update state to delete
                state = VirtualHandState.Displace;
            }

            // Process current holding state
            else
            {

                // Nothing to do for holding
            }
        }

        // if state is displacing
        else if (state == VirtualHandState.Displace)
        {


            // If grasp has been broken
            if (grasp == null)
            {

                // Update state to open
                state = VirtualHandState.Open;
            }


            // If button has been released and grasp still exists
            else if (!button.GetPress() && grasp != null)
            {

                // Get rigidbody of grasped target
                Rigidbody target = grasp.GetComponent<Rigidbody>();
                // Break grasp

                DestroyImmediate(grasp);

                // Apply physics to target in the event of attempting to throw it
                target.velocity = hand.velocity * speed;
                target.angularVelocity = hand.angularVelocity * speed;

                // Update state to open
                state = VirtualHandState.Open;
            }

            //If displace button has been released
            else if (button.GetPress() && !button1.GetPress())
            {
                state = VirtualHandState.Holding;
            }


            else
            {

                Rigidbody target = grasp.GetComponent<Rigidbody>();
                DestroyImmediate(grasp);

                if (joystick.GetAxis().y < 0.0f && button.GetPress() && (Mathf.Abs(joystick.GetAxis().y) > Mathf.Abs(joystick.GetAxis().x)))
                    hand.transform.position += joystick.GetAxis().y * tracker.transform.forward * speed * Time.deltaTime;

                else if (joystick.GetAxis().y > 0.0f && button.GetPress() && (Mathf.Abs(joystick.GetAxis().y) > Mathf.Abs(joystick.GetAxis().x)))
                    hand.transform.position += joystick.GetAxis().y * tracker.transform.forward * speed * Time.deltaTime;

                else if (joystick.GetAxis().x < 0.0f && button.GetPress() && (Mathf.Abs(joystick.GetAxis().y) < Mathf.Abs(joystick.GetAxis().x)))
                    hand.transform.position += joystick.GetAxis().x * tracker.transform.right * speed * Time.deltaTime;

                else if (joystick.GetAxis().x > 0.0f && button.GetPress() && (Mathf.Abs(joystick.GetAxis().y) < Mathf.Abs(joystick.GetAxis().x)))
                    hand.transform.position += joystick.GetAxis().x * tracker.transform.right * speed * Time.deltaTime;

                grasp = target.gameObject.AddComponent<FixedJoint>();

                // Set the connection
                grasp.connectedBody = hand.gameObject.GetComponent<Rigidbody>();

                // Change state to holding
                state = VirtualHandState.Holding;

            }



        }
    }
}