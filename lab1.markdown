---
layout: default
title: "Lab 1"
permalink: "/lab1.html"
---

Lab 1 - The Solex Agitator
==========================

1. Identify the function of all four stages and explain how each works
    - Astable Multivibrator:
	Multivibrators are ciruits which provide for a two different "states" of output. Astability implies that neither of these two states are stable and that ultimately, when in one state, feedback will cause the circuit to jump to the other state. This is accompished with an RC circuit component to drive the oscilation, and two zener diodes to provide voltage limits on either state. There is a variable resistor included as one of the feedback resistors to the positive terminal of the amplifier, this serves to modulate the frequency of the oscilation between states through changing the RC constant of the oscilatory component.
    - Amplifier
	The amplifier stage is an inverting amplifier whose feedback resistor is a 100kOhm variable resistor which can be used to change the output signal's gain. The ability to modify the gain allows the operator to fully vary the amount that the input square wave is amplified.  
    - Integrators 
	The final two components are Op-Amp circuits integrators. As the name betrays, these circuits perform the mathematical operation of integration. That is, in the first stage, the square wave is integrated, producing a triangle wave. In the second stage, the resulting triangle wave is integrated to generate the sine wave.


2. How would you convert the final sine wave to a square wave? 
    - We know that we integrated a square wave twice in order to get the final sine wave. Therefore, we could differentiate twice again to get back to a square wave. However, due to steady state error in integration, there is a good chance there will be some degredation of the signal due to loss and propgated error across all components. 

3. How would you increase the maximum frequency of the circuit? 
    - The first stage of the circuit produces the square wave that is then operated on by the rest of the components. It uses an RC circuit to drive the change, and thus, if we decrease the RC time constant, the charging of the capacitor would take less time and the maximum frequency of the circuit would be higher. Thus, all things constant, a smaller capacitor would increase the maximum possible frequency.

4. What is the function of the zener diodes? 
    - While the zener diodes were not used in lab, their purpose is to allow flow in one direction up to a certain potential difference and then to permit any flow beyond this voltage limit. In this circuit they act as a limit to the square wave's amplitude, allowing only certain maximum voltages relative to ground at the output. In the case of the diagram on the lab handout, this would be an amplitude of 5.1V either direction from ground. 
