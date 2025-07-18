extends Node3D

var x0Speed = randf_range(-1, 1)
var x1Speed = randf_range(-1, 1)
var x2Speed = randf_range(-1, 1)
var x3Speed = randf_range(-1, 1)

var timer = randf_range(1, 5)

@onready var x0 = $"Cardis/3DMap/r0"
@onready var x1 = $"Cardis/3DMap/r1"
@onready var x2 = $"Cardis/3DMap/r2"
@onready var x3 = $"Cardis/3DMap/r3"

@onready var EnergyRotate = $Cardis/energyDC/rotate

@onready var startEngine = $Cardis/startEngineObject

@onready var InsideSoumd = $InsideSound

@onready var _3DMapRotate = $Cardis/screen1

func _ready() -> void:
	$Cardis/centerObject/CenterAnim.play("loop")
	GlobalVAR.PlayerCanJump = false

func _process(delta: float) -> void:
	if timer > 0:
		timer -= delta
	else:
		timer = randf_range(1, 5)

		x0Speed = randf_range(-1, 1)
		x1Speed = randf_range(-1, 1)
		x2Speed = randf_range(-1, 1)
		x3Speed = randf_range(-1, 1)

	x0.rotate_y(x0Speed * delta)
	x1.rotate_y(x1Speed * delta)
	x2.rotate_y(x2Speed * delta)
	x3.rotate_y(x3Speed * delta)

	_3DMapRotate.rotate_y(0.1 * delta)
	EnergyRotate.rotate_y(-1 * delta)

	if not InsideSoumd.playing:
		InsideSoumd.play()
