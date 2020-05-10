using Godot;
using System;

public class Menu : CanvasLayer
{
    [Signal] public delegate void Playing();
    [Signal] public delegate void Stopped();
    
    public override void _Ready()
    {
        
    }

    public void _on_Play_toggled(bool pressed)
    {
        if(pressed)
        {
            EmitSignal("Playing");
        }
        else
        {
            EmitSignal("Stopped");
        }

    }

    public void _on_Reset_pressed()
    {
        GetTree().ReloadCurrentScene();
    }
}
