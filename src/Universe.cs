using Godot;
using System;

public class Universe : Spatial
{
    public float G = 66.74f;

    bool Playing = true;
    public override void _Process(float delta)
    {
        if(Playing)
            SetPhysicsProcess(true);
        else
            SetPhysicsProcess(false);
    }

    public void _on_Menu_Playing()
    {
        Playing = false;
    }

    public void _on_Menu_Stopped()
    {
        Playing = true;
    }


}
