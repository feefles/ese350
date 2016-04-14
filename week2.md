---
layout: default
title: "Week 2"
permalink: "/week2.html"
---

Week 2 (part 1) The sound synthesis
==============================
This week, I worked hard on the sound synthesis. 

First, I did spent a good deal of time getting comfortable in the Unity environment. 
There is a lot to go through here and learning the scripting. 

I know for sure I can create various sound groups and play the audio and control the 
volume in a pretty seamless manner. That was the easy part. 

The hard part is definitely figuring out how to get "smooth" sounding tempo
changes. Unity doesn't support this, and any kind of tempo change 
would also affect the pitch (I think they just change the sampling rate). 

I did a lot of research into how to do this smoothly. 

I now have a working demo in Matlab that does relatively 
smooth tempo transitions. I'm using a process using a Phase Vocoder. 
Basically, this is a process that stretches out time and pitch scaling 
in the frequency domain rather than the time domain. It's actually 
a relatively simple process: you convert into the freq domain using 
some kind of Fourier transform. In the frequency domain, you then use
a sliding window that rescales the ratio between frequency and pitch. 
Once that's done, you inverse transform back into time domain and 
your signal does what you wanted! Same pitch but different speed. 

Matlab has some great code, but it doesn't work since it was introduced in 
R2016a. http://www.mathworks.com/help/audio/examples/pitch-shifting-and-time-dilation-using-a-phase-vocoder-in-matlab.html?refresh=true

I took my code from here: 
http://www.ee.columbia.edu/~dpwe/resources/matlab/pvoc/

My demo in MATLAB will almost definitely be different in the real thing, though. 
Right now, I'm doing actual dynamic computation (setting a pause and then 
figuring out my position and transforming on the remaining portion of the vector). 
I think the way to do it in Unity would be play them all at the same time
and smoothly transition using the volume (which I can't try out in Matlab). 

I intend to have that working by the end of the week. 

Fifi