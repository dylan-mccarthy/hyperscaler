using Godot;
using System;

public partial class MainMenu : Node
{
	public Button StartButton;
	public Button OptionsButton;
	public Button ExitButton;

	public MainMenu(){
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		StartButton = GetNode<Button>("StartGame");
		OptionsButton = GetNode<Button>("Options");
		ExitButton = GetNode<Button>("Exit");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnStartButtonPressed(){
		GetTree().ChangeSceneToFile("res://Scenes/GameScreen.tscn");
	}

	public void OnExitButtonPressed(){
		GetTree().Quit();
	}
}
