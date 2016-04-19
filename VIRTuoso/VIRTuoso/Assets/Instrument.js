#pragma strict

var normalSpeed: AudioSource; 
var slowSpeed: AudioSource; 
var fastSpeed: AudioSource;

function Start () {
	var aSources = GetComponents(AudioSource);
	normalSpeed = aSources[0];
	slowSpeed = aSources[1];
	fastSpeed = aSources[2];
}

function Update () {
	normalSpeed.Play();

}