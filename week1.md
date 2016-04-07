---
layout: default
title: "Week 1"
permalink: "/week1.html"
---

Week 1 - Systems are a go!
==============================

After pitching to the professor, our project changed ever so slightly. 

First, instead of using our own homegrown IR tracking system like we had planned, 
we will be using the Virtual Wall in Xlab. Their system consists of a series of 
IR emitters and greyscale IR cameras. Using Motive's Optitrack system, we can 
get very reliable tracking coordinates in 3D. 

We spent the weekend getting systems up and running. It did take some work
in checking all the connections to get the cameras running, but now 
all systems are go! We obtained the calibration wand from a 
different Penn lab, and successfully calibrated our system. 

We definitely followed the InVIRT project's guide fairly closely here, so much
thanks are in order. 

RoadBlocks and how we're overcoming...
---------------------------------------
First, we had a lot of trouble locating appropriate music files. 
We originally planned on using MIDI files and controlling tracks individually. 
However, this is not a great approach for two reasons. 

1. Controlling individual MIDI channels in MATLAB is not a very easy task
2. Most MIDI classical music files are not very high quality. They sound 
very artificial, and don't flow particularly well. 

We have temporarily solved this by making our own multi track classical 
music files. I did a toy version of Pachebel's Canon in D using GarageBand. 
The quality is much higher, and the instruments sound real. I'm not sure if 
this is a sustainable option, but I think it's okay for a proof of concept.
In a "real world setting", it shouldn't be that hard to record individual 
section parts and control them appropriately. 

Second, the actual sound synthesis is not particularly straightfoward in MATLAB. 
After obtaining the multiple tracks, I worked in MATLAB to achieve our two main goals.

1. Controlling volume. I thought this would be an easier task, but actually it isn't 
as easy as anticipated. It's somewhat difficult to dynamically change the volume of 
a sound player dynamically. The best way is to actually increase the amplitude of the
sound signal, but we haven't yet found a smooth way to do this. 

2. Controlling tempo. The "easy" way to control tempo in MATLAB is to change the sampling 
frequency. Unfortunately, this has the side effect of changing the pitch... There are ways 
to mitigate this, but it isn't a route we are going to pursue as of now. 

Our other large roadblock is the fact we have not yet been able to set up streaming from 
Motive to MATLAB. This seems like a software issue (the computer is running a rather old version
of Windows...). We do know that Motive to Unity has worked for teams in the past. 

Because of these large issues, we are now exploring using Unity instead of MATLab. 
We know already that they have relatively large libraries for doing sound synthesis, 
and we may be abot to achieve some of these effects that we want. 

Next Steps... 
---------------
We have divided the work between Fifi and Lucas so they can work in parallel. 
1. Lucas will be working on gesture recognition and processing the Motive data. 
The first feature will probably be determining the "direction" the baton is pointing, and
raising and lowering volume. We can do this without having motive streaming working 
(just collect data and do processing on that). 

2. Fifi will be working on the sound processing portion in Unity. She is looking into 
how we can Unity's libraries to create the desired effects. 

Our goal is to be able to do some basic sound effects in Unity by next week. 