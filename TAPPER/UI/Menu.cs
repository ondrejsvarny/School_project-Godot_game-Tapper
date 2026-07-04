using Godot;

public partial class Menu : Control
{

	public void _on_play_pressed()
	{
		GetTree().ChangeSceneToFile("res://Levels/level.tscn");
	}

	public void _on_exit_pressed()
	{
		GetTree().Quit();
	}
}
