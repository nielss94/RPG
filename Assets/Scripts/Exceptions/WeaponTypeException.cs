using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTypeException : Exception {

    string message = "Please wield the correct type of weapon to use this ability.";

    public WeaponTypeException(string message)
    {
        this.message = message;
    }
    public WeaponTypeException()
    {
    }

    public void LogAsWarning()
    {
        Debug.LogWarning(message);
    }

    public void LogAsError()
    {
        Debug.LogError(message);
    }
}
