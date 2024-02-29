using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class Controls 
{
    public static XRIDefaultInputActions actions;

   
    public static void Initialize()
    {
        actions = new XRIDefaultInputActions();
        actions.XRIRightHandInteraction.Enable();
        actions.XRILeftHandInteraction.Enable();
        actions.XRILeftHandLocomotion.Enable();
        actions.XRIRightHandLocomotion.Enable();
       
    }
   

   
}
