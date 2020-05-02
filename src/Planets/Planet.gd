extends KinematicBody


var G = 2000.0
var body = {position = Vector3(200,100,0), velocity = Vector3(0,1,0)}
var center = Vector3(0,0,0)

func _ready():
	pass # Replace with function body.


func acceleration(pos1, pos2):
	var direction = pos1 - pos2
	var length = direction.length()
	var normal = direction.normalized()

	return normal * (G / pow(length, 2))
   
   
func step_euler(euler_center, euler_body):
	var step = 8
	for i in range(step):
		var dt = 1.0 / step
		var acc = acceleration(euler_center, euler_body.position)
		body.velocity = body.velocity + acc * dt
		body.position = body.position + body.velocity * dt

func  _physics_process(delta):
	step_euler(center, body)
