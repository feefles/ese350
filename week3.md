---
layout: default
title: "Week 3"
permalink: "/week3.html"
---

Week 3 - A baby demo
====================

There were two big breakthroughs this week. 

First, we finally got the Optitrack streaming to Unity. This 
was mainly thanks to the tutorial of the inVIRT team from last 
semester. We ran into a lot of computer problems and random 
issues that made it difficult (like the computer's antivirus software
deleting the .exe file we needed to capture the packets upon execution
of the program- a very strange bug indeed!) We were able to 
load up their group's games and test that everything worked properly. 

Second, I made a Unity demo for the sound synthesis, as described last week. 
I have a MATLAB script where you only need to specify the input files that
you want for each track, and it will go ahead and resample them at some list of
specified frequencies and save the files. It's a pretty easy process. 

The Unity demo only uses arrow keys and one "instrument". You can go 
left and right to change the speed of the track, and up and down to 
control volume. The speed changing is a huge pain. The reason I have 
the MATLAB script is because we attach all those sound files to the 
GameObject (so it has ~7 AudioSources attached to it). When the program
is run, all the AudioSources on an object are muted, except for the speed
that you want. When you change speed, you mute all except the desired speed.
However, you don't just mute it, you have to make sure all of them are playing
at the right speed (since clearly some files are shorter than others). This is
where I take advantage of the pitch feature (which effectively changes the speed).

When you do a tempo change, you play all the other tracks at the same "speed" as the 
tempo you are currently playing. So you change the pitch to be  (playing speed) / (actual speed of track). 
For example, I want to play at 1.2x the normal speed. Therefore, the 1.0 will have to be played at 
1.2 / 1.0 which is 1.2x as fast, and .8 speed will be at 1.2/.8 which is 1.5x as fast. 
This conversion ensures that when you switch tracks, they are all at the same place, which makes
for pretty smooth transitions! The only issue is that the resampling seems to affect
the volume slightly. You can sometimes hear the track change. I'll have to look into this. 

I'm happy with the progress this week - the sound synthesis stuff came together nicely, 
so next week will be integration of this small demo and the gestures. 

Fifi

<a class = "post-link" href = "{{ "week4.html" | prepend: site.baseurl}}">Week 4</a>
