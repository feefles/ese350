---
layout: default
title: "Project Proposal"
permalink: "/proposal.html"
---

VIRTuoso
====================
Evelyn Fifi Yeung + Lucas Tai-Macarthur  


Motivation
----------

Becoming a classically trained conductor is a long and difficult process involving many years of academic study and practical instruction. It relies both on a wealth of historic knowledge about music as well as the ability to translate that knowledge into voiceless direction for many dozens of musicians. Without an orchestra to practice with, it is difficult, if not impossible to practically apply the skills learned. It takes a long time and a lot of human effort to convene an orchestra for this purpose, therefore, it would be advantageous if there were a system that would allow a conductor to practice or sharpen their skills without requiring other people. Our project fills this gap by creating a method by which a conductor may both practice, as well as receive auditory feedback completely autonomously.

Goal
----
- Design an easy-to-use baton to control a “digital orchestra”
- Create the various “instruments” using embedded system and microphones 
- Provide haptic cues to assist conductor 


Methodology
-----------
- Use VirtualWall in XLab to track stick 
- Stream motive data to Matlab to do processing of motion, sound synthesis
- Maybe stream sound to separate speakers by Mbed


Project Components
------------------
    
Hardware  
- VirtualWall to do baton tracking
- Computer running Matlab
- Maybe vibrators to provide haptic feedback

Software  
- Software on computer to interface with baton signals, locate and process location and how baton movement affects the digital orchestra.


Testing  
- Accuracy of location of baton in space and gesture recognition from other sensors  
- Correct response from computer software based on input gestures  
- Correct actuation of haptics based on computer response  

Evaluation  
User testing 

Deliverables
------------

- Baseline1: Camera can track user + baton and computer can control a single track at a certain amplitude or frequency based off the (X,Y) coordinates of the baton.
- Baseline2: Baseline1 + user can control specific tracks of MIDI file based on coordinates
- Baseline3: Baseline2 + user gets haptic feedback so as to be able to stay on beat during conducting 

- Stretch1: Adjustable Haptic Feedback, be able to increase or decrease feedback/guidance on the fly
- Stretch2: Active feedback when the user is conducting the digital orchestra incorrectly
- Stretch3: Save previous runs and get haptic feedback when you deviate from them.

Overall Timeline
----------------

- Week 1: Camera IR tracking and MIDI instrument differentiation  
    Features: Modulating amplitude of specific MIDI tracks
- Week 2: Integrating accelerometer data into baton movement recognition  
    Features: Modulating frequency of specific MIDI tracks, adding punctuation to specific tracks
- Week 3: Incorporating basic haptic feedback into system  
    Features: Fixed tempo pulses on baton
- Week 4: User testing and prepping for demo! 
