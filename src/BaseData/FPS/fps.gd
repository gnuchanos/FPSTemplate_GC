extends CharacterBody3D

const JUMP_VELOCITY = 5
@onready var head = $head
@onready var body = $body

const  bodyDuck: int = 1
const  bodyCrawley: float = 0.3
const bodyMaxHeight: int = 2
var bodyCurrentHeight: float = 2

var Dukking: bool  = false
var Crawling: bool = false
const  CrawlingOrDuckingSpeed: int = 5

@onready var SettingsWindow = $CanvasLayer/UI/settings

@onready var HeadRay = $HeadRay

func _ready():
	Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)

func _input(event):
	if GlobalVAR.PlayerCanMove and not GlobalVAR.PlayerLookingConsole and not GlobalVAR.PlayerLookingSettings:
		if event is InputEventMouseMotion:
			rotate_y(deg_to_rad(-event.relative.x * GlobalVAR.MouseSpeed))
			head.rotate_x(deg_to_rad(-event.relative.y * GlobalVAR.MouseSpeed))
			head.rotation.x = clamp(head.rotation.x, deg_to_rad(-89), deg_to_rad(89))

func _process(_delta: float) -> void:
	if Input.is_action_just_pressed("escape"):
		if GlobalVAR.PlayerLookingSettings:
			GlobalVAR.PlayerLookingSettings = false
			Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)
			SettingsWindow.hide()
		else:
			GlobalVAR.PlayerLookingSettings = true
			Input.set_mouse_mode(Input.MOUSE_MODE_VISIBLE)
			SettingsWindow.show()

func _physics_process(delta: float) -> void:
	# Add the gravity.
	if not is_on_floor():
		velocity += get_gravity() * delta

	if GlobalVAR.PlayerCanMove and not GlobalVAR.PlayerLookingConsole and not GlobalVAR.PlayerLookingSettings:
		if Input.is_action_just_pressed("space") and is_on_floor():
			velocity.y = JUMP_VELOCITY

		if Input.is_action_pressed("duck"):
			body.shape.height -= CrawlingOrDuckingSpeed * delta
			head.position.y = 0.7
			Crawling = false
			Dukking  = true
			body.shape.height = clamp(body.shape.height, bodyDuck, bodyMaxHeight)
		elif Input.is_action_pressed("crowley"):
			body.shape.height -= CrawlingOrDuckingSpeed * delta
			head.position.y = 0.3
			Crawling = true
			Dukking  = false
			body.shape.height = clamp(body.shape.height, bodyCrawley, bodyMaxHeight)
		else:
			if not HeadRay.is_colliding():
				body.shape.height += CrawlingOrDuckingSpeed * delta
				head.position.y = 1
				Crawling = false
				Dukking  = false
				body.shape.height = clamp(body.shape.height, bodyDuck, bodyMaxHeight)

		body.shape.radius = clamp(body.shape.height, 0.4, 0.4)

		var input_dir := Input.get_vector('a', 'd', 'w', 's')
		var direction := (transform.basis * Vector3(input_dir.x, 0, input_dir.y)).normalized()
		if direction:
			velocity.x = direction.x * GlobalVAR.PlayerCurrentSpeed
			velocity.z = direction.z * GlobalVAR.PlayerCurrentSpeed
		else:
			velocity.x = move_toward(velocity.x, 0, GlobalVAR.PlayerCurrentSpeed)
			velocity.z = move_toward(velocity.z, 0, GlobalVAR.PlayerCurrentSpeed)

	move_and_slide()
