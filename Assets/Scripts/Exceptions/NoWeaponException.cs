using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoWeaponException : Exception {

    string message = "Please wield a weapon to use this ability.";

    public NoWeaponException(string message)
    {
        this.message = message;
    }
    public NoWeaponException()
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
