extends Node
onready var line_drawer = $"../LineDrawer"
onready var distance_label = $"../UI/DistanceLabel"
onready var tongue = $"../Stroker/Tongue"
onready var stroker = $"../Stroker"
onready var valve_gauge = $"../UI/ValveGauge"
onready var help_panel = $"../UI/HelpPanel"
onready var camera = $"../Camera"

var angle_z = 0.0  # left/right rotation
var angle_x = 0.0  # up/down rotation
var angle_y = 0.0  # shift+left/right rotation
var camera_distance = 3.25


func _input(event):
	if event is InputEventKey and event.pressed and event.scancode == KEY_L:
		line_drawer.visible = not line_drawer.visible
	if event is InputEventKey and event.pressed and event.scancode == KEY_D:
		distance_label.visible = not distance_label.visible
	if event is InputEventKey and event.pressed and event.scancode == KEY_T:
		tongue.visible = not tongue.visible
	if event is InputEventKey and event.pressed and event.scancode == KEY_V:
		valve_gauge.visible = not valve_gauge.visible
	if event is InputEventKey and event.pressed and event.scancode == KEY_H:
		help_panel.visible = not help_panel.visible
	if event is InputEventKey and event.pressed and event.scancode == KEY_LEFT:
		if event.shift:
			angle_y += 15.0
		else:
			angle_z -= 15.0
		_update_camera()
	if event is InputEventKey and event.pressed and event.scancode == KEY_RIGHT:
		if event.shift:
			angle_y -= 15.0
		else:
			angle_z += 15.0
		_update_camera()
	if event is InputEventKey and event.pressed and event.scancode == KEY_UP:
		angle_x -= 15.0
		_update_camera()
	if event is InputEventKey and event.pressed and event.scancode == KEY_DOWN:
		angle_x += 15.0
		_update_camera()
	if event is InputEventKey and event.pressed and event.scancode == KEY_R:
		angle_z = 0.0
		angle_x = 0.0
		angle_y = 0.0
		_update_camera()


func _update_camera():
	angle_z = fmod(angle_z, 360.0)
	angle_x = fmod(angle_x, 360.0)
	angle_y = fmod(angle_y, 360.0)
	camera.transform = Transform.IDENTITY
	camera.translate(Vector3(0, 0, camera_distance))
	camera.transform.origin = Vector3.ZERO
	var rot = Transform.IDENTITY
	rot = rot.rotated(Vector3.RIGHT, deg2rad(angle_x))
	rot = rot.rotated(Vector3.UP, deg2rad(angle_y))
	rot = rot.rotated(Vector3.FORWARD, deg2rad(angle_z))
	var offset = rot.xform(Vector3(0, 0, camera_distance))
	camera.transform.origin = offset
	camera.look_at(Vector3.ZERO, rot.basis.xform(Vector3.UP))
