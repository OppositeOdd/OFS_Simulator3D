extends Node
@onready var line_drawer = $"../LineDrawer"
@onready var distance_label = $"../UI/DistanceLabel"
@onready var tongue = $"../Stroker/Tongue"
@onready var stroker = $"../Stroker"


func _input(event):
	if event is InputEventKey and event.pressed and event.keycode == KEY_L:
		line_drawer.visible = not line_drawer.visible
	if event is InputEventKey and event.pressed and event.keycode == KEY_D:
		distance_label.visible = not distance_label.visible
	if event is InputEventKey and event.pressed and event.keycode == KEY_T:
		tongue.visible = not tongue.visible
