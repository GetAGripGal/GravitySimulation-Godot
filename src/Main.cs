using Godot;
using System;

public class Main : KinematicBody
{
    Vector3 Motion;
    bool dragging;

    public override void _Ready()
    {   
        MoveAndSlide(Motion);
    }


}