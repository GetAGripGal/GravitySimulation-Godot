using Godot;
using System;

public class Player : GravityObject
{
    MeshInstance Head;
    [Export]
    public float MouseSensitivity = 0.05f;

    [Export]
    public float Gravity = -24.8f;
    [Export]
    public float MaxSpeed = 20.0f;
    [Export]
    public float JumpSpeed = 18.0f;
    [Export]
    public float Accel = 4.5f;
    [Export]
    public float Deaccel = 16.0f;
    [Export]
    public float MaxSlopeAngle = 40.0f;
    [Export]

    private Vector3 _vel = new Vector3();
    private Vector3 _dir = new Vector3();

    private Camera _camera;
    private Spatial _rotationHelper;

    bool OnPlanet = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {   
        _Init();
        _camera = GetNode<Camera>("Rotation_Helper/Camera");
        _rotationHelper = GetNode<Spatial>("Rotation_Helper");

        Input.SetMouseMode(Input.MouseMode.Captured);
    }

    public override void _PhysicsProcess(float delta)
    {   
        ProcessInput(delta);
        UpdateVelocity(delta);
        ProcessMovement(delta);
        //MoveAndSlide(currentVelocity * delta);
    }

    private void ProcessInput(float delta)
    {
        //  -------------------------------------------------------------------
        //  Walking
        _dir = new Vector3();
        Transform camXform = _camera.GlobalTransform;

        Vector2 inputMovementVector = new Vector2();

        if (Input.IsActionPressed("movement_forward"))
            inputMovementVector.y += 1;
        if (Input.IsActionPressed("movement_backward"))
            inputMovementVector.y -= 1;
        if (Input.IsActionPressed("movement_left"))
            inputMovementVector.x -= 1;
        if (Input.IsActionPressed("movement_right"))
            inputMovementVector.x += 1;

        inputMovementVector = inputMovementVector.Normalized();

        // Basis vectors are already normalized.
        _dir += -camXform.basis.z * inputMovementVector.y;
        _dir += camXform.basis.x * inputMovementVector.x;
        //  -------------------------------------------------------------------

        //  -------------------------------------------------------------------
        //  Jumping

        if (Input.IsActionJustPressed("movement_jump"))
                _vel.y = JumpSpeed;
        //  -------------------------------------------------------------------

        //  -------------------------------------------------------------------
        //  Capturing/Freeing the cursor
        if (Input.IsActionJustPressed("ui_cancel"))
        {
            if (Input.GetMouseMode() == Input.MouseMode.Visible)
                Input.SetMouseMode(Input.MouseMode.Captured);
            else
                Input.SetMouseMode(Input.MouseMode.Visible);
        }
        //  -------------------------------------------------------------------
    }

    private void ProcessMovement(float delta)
    {
        _dir.y = 0;
        _dir = _dir.Normalized();

        _vel.y += delta * Gravity;

        Vector3 hvel = _vel;
        hvel.y = 0;

        Vector3 target = _dir;

        target *= MaxSpeed;

        float accel;
        if (_dir.Dot(hvel) > 0)
            accel = Accel;
        else
            accel = Deaccel;

        hvel = hvel.LinearInterpolate(target, accel * delta);
        _vel.x = hvel.x;
        _vel.z = hvel.z;
        
        if(OnPlanet)
            currentVelocity.x = 0;
            currentVelocity.z = 0;
        
        MoveAndSlide((currentVelocity * delta));
    }


    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion && Input.GetMouseMode() == Input.MouseMode.Captured)
        {
            InputEventMouseMotion mouseEvent = @event as InputEventMouseMotion;
            _rotationHelper.RotateX(Mathf.Deg2Rad(-mouseEvent.Relative.y * MouseSensitivity));
            RotateY(Mathf.Deg2Rad(-mouseEvent.Relative.x * MouseSensitivity));

            Vector3 cameraRot = _rotationHelper.RotationDegrees;
            cameraRot.x = Mathf.Clamp(cameraRot.x, -70, 70);
            _rotationHelper.RotationDegrees = cameraRot;
        }
    }

    public void _on_Area_area_entered(PhysicsBody body)
    {
        OnPlanet = true;
    }

    public void _on_Area_area_exited(PhysicsBody body)
    {
        OnPlanet = false;
    }
}

