---
layout: default
title: "Week 4"
permalink: "/week4.html"
---

Week 4 - Towards a Full Demo
====================

This was definitely the fun week. Since a lot of the plumbing has been done with respect
to the sound synthesis, I could really dig into some fun gesture recognition stuff. 

Roadblock 1
-----------
The original thought was that a user would be able to "point" at a section of the 
orchestra and it would play. This involves getting orientation of the baton. 
The libraries we are using (the rigidbody.cs files that inVIRT also used) provide
that functionality. However, it's extremely noisy compared to the position tracking.

We tried recalibrating it a few times, but it doesn't seem to be a calibration error. 
The next move was to try smoothing it. First, I wanted to smooth it by taking averages 
over the past few readings. However, this isn't as easy as it seems since orientation
is given in terms of quaternions. Averaging individual quaternion values might not provide 
the behavior I actually want, and is also very tedious to do. The next smoothing I tried was
a simple filter with a weight w where the value is w * current reading + (1-w) * previous reading. 
This has certainly worked in other noisy applications like velocity filtering from 
position values (derivatives are tricky.....)
This doesn't really make sense for a quaternion representation, though... so this wwas quickly scrapped.  

After searching on the internet, it seemed most people smooth things out by doing linear 
interpolation between the quaternions. (this should achieve a similar effect to what
I was trying to do with that filter) 
This also didn't help much. Readings were too noisy and it took a long time to settle down. 

When I turned off rotation tracking, I found that actually since the markers are on the end of the stick, rotating it 
causes enough position offset to be effective, so for now I'm just going to turn off 
rotation tracking. This could be an opportunity for a stretch goal though.

General Approaches
------------------
Smoothing, smoothing, smoothing. That was all that happened this week. Lots of code to try to 
smooth out all the various readings and interpretations of the position data of the wand. 

Not to bore you too much, but lots of different techniques are used including lots of averaging 
of data and picking majority readings. Pretty tedious and not particularly sexy for this blog. 
I spent a lot of time writing a nice gesture recognizer that takes in an array of the last 
(however many you want) position readings and tries to determine if there's a valid gesture. 
This makes the code pretty flexible so in theory soemone could plug this into some other application
that needs gestures. It does require a good amount of fine tuning though. 

Hooking that gesture recognizer into the demo was pretty easy. Now you can point at one of the 
4 "instruments" and raise / lower your baton to change the volme. 

Another technical challenge
---------------------------
I sort of knew volume control would be the easy part. Determining the "beat" was definitely harder. 
I considered 2 approaches. One would be that the user could point to specific objects in the game
environment in the time that they want, and the tempo could be determined from when you point to 
those objects. However, the user experience of that is definitely not optimal, so I opted for the 
more challenging approach: actually detecting direction changes of the baton as "beats", which is far 
more realistic. 

This was really fun. Took a lot of debugging and it's rather tedious code to write. 
It's particularly difficult because of the way people conduct. Sometimes you might
move the baton and stop to signify a beat before moving on, but this particular approach
means you have to actually change direction on the beat. (It's a subtle difference, but 
surprisingly hard to deal with)

I did some tests on how well I could determine BPM based on my gestures. I conducted to a metronome, 
and I was actually consistently within 10 BPM of the target (usually under, which is likely due
to the smoothing required). I would say that's not terrible, but could definitely be improved for 
the final demo. 

Finally...
----------
I tied the gesture recognizer of the wand into the tempo changer of the 
track. It actually worked shockingly well. I'm really happy with the 
quality of the track (the track switches are somehow less obvious now than before). 
Not too much fine tuning is needed here. Now all I need to do is tie it all back together
and make it look nice :) 

Fifi