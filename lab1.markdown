---
layout: default
title: "Lab 1"
permalink: "/lab1.html"
---

Lab 1 - The Solex Agitator
==========================

1. Identify the function of all four stages and explain how each works
    - Astable Multivibrator: A multivibrator is a circuit with two distinct output voltage levels. Astability means that the state of the circuit at each output level is unstable and that feedback will eventually cause the circuit to transition to the other state. This feature is driven by an RC circuit whose natural charging and discharging properties cause the circuit to oscilate through two states indefinitely. 
    - Amplifier
	The amplifier stage is an inverting amplifier whose feedback resistor is a 100kOhm variable resistor. It serves to take the input square wave from the multivibrator and amplify it before it is mathematically operated on. The gain of the stage can be changed through a adjusting the potentiometer. 
    - Integrators 
	The final two stages are Op-Amp based integrators. These circuits perform the mathematical operation of integration. The square wave is integrated into the triangle wave, and finally the triangle wave is integrated into the sine wave. The integrator performs these operations through the charging of a feedback capacitor and resistor pair which performs the desired integration over time. 


2. How would you convert the final sine wave to a square wave? 
    - We know that we integrated a square wave twice in order to get the final sine wave. Therefore, we could differentiate twice again to get back to a square wave. However, due to steady state error in integration, there is a good chance there will be some degredation of the signal due to loss and propgated error across all components. 

3. How would you increase the maximum frequency of the circuit? 
    - The first stage of the circuit produces the square wave that is then operated on by the rest of the components. It uses an RC circuit to drive the change between different levels of the square wave, and thus, if we decrease the RC time constant, the charging of the capacitor would take less time and the maximum frequency of the circuit would be higher. Thus a smaller capacitor in the first stage would increase the maximum possible frequency.

4. What is the function of the zener diodes? 
    - While the zener diodes were not used in lab, their purpose is to allow flow in one direction up to a certain potential difference and then to permit any flow beyond this voltage limit. In this circuit they act as a limit to the square wave's amplitude, allowing only certain maximum voltages relative to ground at the output. In the case of the diagram on the lab handout, this would be a range of 5.1V in either direction from ground. 
