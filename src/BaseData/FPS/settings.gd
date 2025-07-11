extends Window




func _on_close_requested() -> void:
	self.hide()

var FullScreen = false
func _on_window_mode_pressed() -> void:
	if FullScreen:
		DisplayServer.window_set_mode(DisplayServer.WINDOW_MODE_WINDOWED)
		FullScreen = false
	else:
		DisplayServer.window_set_mode(DisplayServer.WINDOW_MODE_FULLSCREEN)
		FullScreen = true

func _on_mousespeed_slider_value_changed(value: float) -> void:
	GlobalVAR.MouseSpeed = value
	print(GlobalVAR.MouseSpeed)

func _on_music_volume_slider_value_changed(value: float) -> void:
	AudioServer.set_bus_volume_db(AudioServer.get_bus_index("Music"), value)

func _on_effect_volume_slider_value_changed(value: float) -> void:
	AudioServer.set_bus_volume_db(AudioServer.get_bus_index("Effect"), value)

func _on_atmosphere_volume_slider_value_changed(value: float) -> void:
	AudioServer.set_bus_volume_db(AudioServer.get_bus_index("Atmosphere"), value)

func _on_voice_volume_slider_value_changed(value: float) -> void:
	AudioServer.set_bus_volume_db(AudioServer.get_bus_index("Voice"), value)
