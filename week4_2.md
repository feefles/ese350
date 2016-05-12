---
layout: default
title: "Week 4 (Demo Preparation!)"
permalink: "/week4_2.html"
---

Week 4 - Demo Preparation
====================
Basically, we are in full gear to get ready for the demo. 

Some rather hacky things were done to get tempo and volume
working nicely together.. it's not as intuitive as I would 
like (I did one usability test, and the user did get confused / frustrated)

Basically, to get it all working, the user can only do tempo 
control if none of the instruments are selected. This just means
you have to point higher on the screen. I have to think a lot
harder about the logic that would be required to make this more intuitive. 

But anyway, this whole time I've been using the silly version of Pachebel's 
Canon in D I recorded in GarageBand on my iPad. It gives really high 
quality instrument sounds, unlike MIDI files (I mentioned this in Week 1).

My friend thought the volume control wasn't quite obvious enough in this track
because all the instruments were too "harmonious". He suggested trying to 
run this on more modern tracks. This was the first time we tried 
a different track from this one... so it was a good exercise to run through
the whole process of changing tracks. 

We downloaded a great anthem of my generation: Taylor Swift's Blank Space. 
We simulated cutting it up into different tracks by doing pitch cutoffs 
(ie bass, voice, music). It worked pretty well, so we got 4 tracks out of it. 
I went through the process of resampling in Matlab and importing into Unity. 

All in all, this only took about 5 minutes! When we went to play the 
game, it sounded great! It really does illustrate the volume changes much 
better, and the resampling really didn't distort the voice at all! 
It was a great suggestion, and we might even use this track in our demo 
since it sounds so good! 

Usability...
-------------
One issue I found often in testing was the lack of visual feedback 
to know if a command was received. I implemented a pretty simple fix: 
the size of the instrument's "sphere" is changed to represent the current volume. 
It's pretty intuitive now: a small sphere means it's playing softly and 
larger means louder. It makes testing so much easier, and is a really 
simple implementation that makes the "game" that much more fun. 

This is just an interesting thing - the simple things can really make 
the experience so much better. I'll be on the lookout for more simple
fixes to make this more usable. 

Fifi

<a class = "post-link" href = "{{ "week5.html" | prepend: site.baseurl}}">Final Post</a>
