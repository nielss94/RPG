using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoCommand : Command
{
    public override void Execute(PlayableCharacter character)
    {
        Debug.Log("Not implemented");
    }
}
