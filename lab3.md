---
layout: default
title: "Lab 3"
permalink: "/lab3.html"
---

Lab 2 - DTMF
==========================

Overview
--------

Part 0: We made the circuit, and the speaker buzzed. Not much was done here. 

Part 1: We generated the waveform on OCR1A. We set the desired delay and made sure that we enabled toggle on compare match. 

This was a helper method to compute the delay in terms of our desired Hz for the waveform. 

    int freq_to_delay(int freq){

        return 1000000 / freq ;

    }

Helper function to handle the COMPA setup

    void turn_COMPA_on(){
        // enable tcnt and toggle on compare match
        TCCR1A |= 0x50;
        TIMSK1 |= 0x06;
    }

The interrupt handler for OCR1A. There is some additional logic here for the later part of the lab (additional logic to see that a button has been held down for a sufficient amount of time)

    // use this timer to control the signal 
    ISR(TIMER1_COMPA_vect) 
    {
        OCR1A += freq_to_delay(freq);
        if (x_seen > 10) {
            TIMSK1 = 0;
            TCCR1A = 0;
        }
    }

Part 2: This involved getting some input. Changing frequency was very easy (just setting a global variable freq), and the length was a matter of counting down. That code was removed in the final demo version, but it was in the interrupt code above as a simple count down loop. 

    sei();
    int count; 
    char c;
    int word = 0;
    while (1)
    {
        count = 0;

        while (1)
        {
            c = uart_getchar(&uart_in);
            // uart_putchar('h',&uart_out);
            if (c == ' ') {
                freq_buf[count] = '\0';
                freq = atoi(freq_buf);
                word = 1;
                count = 0;
            }

            if (c == '\n' || c == '\r' || c == EOF) {
                time_buf[count] = '\0';
                time_delay = atoi(time_buf);
                cycle_count = (((float)time_delay/1000.0) / (1 / (float)freq));
                uart_putchar('\n', &uart_out);
                printf("freq: %i, time: %i", freq, cycle_count);

                TCCR1A = 0x40;
                TIMSK1 |= 0x02;
                break;
            }

            uart_putchar(c,&uart_out);
            if (word == 0) {
                freq_buf[count++] = c;
            } else {
                time_buf[count++] = c;
            }
        }
        word = 0;
    }

Part 3: We divided the work here. Fifi worked mainly on wiring the keypad, while Lucas wrote the code to detect keypresses. The keypad uses 4 pull down resistors on the input pins. The code is somewhat similar to the previous part: we use output compare for a fixed 4ms time period (we prescaled by 256)

Setup for the timers

    TCCR1B |= 0x02; // prescalar of 8
    TIMSK1 |= 0x02;

    OCR1A = delay;
    OCR1B = delay;
    // Timer 2 setup

    // DDRD = 0xFF;
    // Prescale by 256
    TCCR2B |= ((1 << 2) | (1 << 1));
    // TCCR2B |= 0x02;

    // Set CTC
    TCCR2B |= 0x40;

    // enable interrupt
    TIMSK2 |= 0x02;

    OCR2A = 0xFA; // set this to interrupt every 4 ms

Interrupt for this, gets the correct character

    int cur_row = 0;
    // use this timer to detect the keypress
    ISR(TIMER2_COMPA_vect)
    {

        char a = 'X';
        
        if(cur_row == 0){
            a = get_pressed_key_1();
        }else if (cur_row == 1){
            a = get_pressed_key_2();
        }else if (cur_row == 2){
            a = get_pressed_key_3();
        }else{
            a= get_pressed_key_4();
        }

        if(a != 'X'){
            uart_putchar(a,&uart_out);
            if(curr_button = 'X'){
                turn_COMPA_on();
            }
            curr_button = a;
            x_seen = 0;
        }else{
            x_seen++;
        }

        cur_row = (cur_row+1) % 4;

    }

Not putting all the different functions, but here is what one of them looks like (the rest are identical)

    
    char get_pressed_key_1(){
        // check row 1
        // Set row 1 high
        PORTC = 0x00;
        PORTC = (1 << 2); 

        if ((PIND & port2) > 0){
            freq = 697;
            freq2 = 1209;
            return '1';
        }

        if ((PIND & port3) > 0) {
            freq = 697;
            freq2 = 1336;
            return '2';
        }
        if ((PIND & port4) > 0) {
            freq = 697;
            freq2 = 1477;
            return '3';
        }

        if ((PIND & port5) > 0) {
            freq = 697;
            freq2 = 1633;
            return 'A';
        }

        else return 'X';

    }

Part 5:

Simply, the timer was set to CTC mode through the following configuration of TCCR1A/B. Bit WGM12 in TCCR1B was set high, thus enabling CTC mode for timer 1. Bits COM1B0 and COM1A0 were set low to disable interrupt compare on OCR1A/B. To set timer 0, the same configuration and set of selections was used, although specific to the control structure of that timer.

	

Questions
----------

### Question 1
What are the functionalities of TCNT1, OCR1A/B, TIMSK1, TCCR1A/B? What are the purposes for each and how did they contribute to the final product? 

  - TCNT1: We didn't actually use it explicitly in this lab, but the microcontroller is controlling this register to continuously count. This is how we derive timing for other things. 

  - OCR1A/OCR1B: These output compare registers were used to generate waveforms with the desired frequencies. These were set to toggle on output compare (when TCNT1 reaches the value in OCR1A/OCR1B) Note that this was changed for part 5. 

  - TIMSK1: This register allows you to specify desired timer behaviors. In this lab, it was used to enable and disable interrupts for output compare (changing bits 1 and 2 accordingly)

  - TCCR1A/B: This allows you to control the timer in various ways. We used it to do prescaling for certain functions so we could interrupt at intervals within our ranges. We later use it in CTC mode to generate square waves. 

### Question 2
What are the minimum and maximum frequences of Timer1 and Timer0 assuming no prescaling? 

The difference between the two is that Timer1 is a 16 bit timer, whereas timer0 is an 8 bit timer. They have the same granularity, that, with no prescaling, they could conceivably measure every one pulse of the cpu. That is, 16mhz max. However, since Timer1 can count up to 2^16, and Timer0 can only count up to 2^8. Timer1's minimum frequency is ~244hz whereas timer0's is 62.5kHz.

### Question 3
Provide an equation to convert milliseconds into clock ticks
num_ticks = 16 * 10^3 * num_ms

### Question 4
What is the maximum and minimum frequency you can achieve with a 16MHz clock using output compare? 
Maximum frequency is 8,000,000 Hz.  
We know that the timer is 16 bits, so it can only count up to 65536.  
Minimum frequency is 123 Hz. 

On 2MHz clock: 
Maximum frequency is 1,000,000 Hz. 
Minimum frequency is 16 Hz. 

### Question 5. 
![DTMF](assets/dtmf_keyboard.jpg "Circuit Diagram")
We connected all the rows directly to the output pins as labeled. We use pull down resisters on the input pins (PD2-5) in order to keep them grounded when the circuit is not connected. Upon pressing any button, the circuit would be completed. As electrons take the path of least resistance, the input pins will register a high voltage. 

### Question 6. 
Explain the logic of your code in part 3. 
This is pretty much outlined in the project writeup. Every 4 ms in the timer2 interrupt (also using output compare), we set a new row to "high" (it rotates between them). We then read all of the input pins to see if any of them are high. If one of them is high, we have isolated the signal (since we now know which row is high and which pin is reading high). There are just a lot of if statements here to get the right pin which is pretty uninteresting to talk about. The registers used are in the diagram above. As an example, let's say our cur_row is 1 (0 indexing caused some inconsistent naming). We set PC5 to be high. We now press 6 which completes the circuit. PD4 will now become high. In the get_pressed_key_2() which looks just like get_pressed_key1() above, reads PORTDs 2-5. Here are some constants we defined for readability

    #define port5 0x20 // 100000
    #define port4 0x10 // 10000
    #define port3 0x8//1000
    #define port2 0x4
    #define port6 0x40
    #define port7 0x80

we AND the PIND with these port "masks" to select the bit that we want. If it's above 0, we read it as high and output the specified character. 

### Question 7. 
Explain the function of the resistor and keypad in terms of switches. 

This was discussed above. This is a pulldown resistor which ensures that when switches are not pressed, the input pins are reading low, and when the switch is pressed, the input pins will read high. 
We could also use a pull-up resistor and reverse some logic. Instead of settings rows to be high in sucession, we will now keep all rows high and set them to low one at a time. On the input pins, it will read high when no buttons are pressed. When the output pin is driven low and the corresponding button is pressed, the input pin will read a low voltage. 

### Question 8. 
Explain why you used prescalars. 
The prescalar was more necessary for some parts than others. For generating the waveform, a prescalar of 8 seemed like a good choice. We didn't have a very specific range of frequencies to work off of. It was more important for the 4ms interrupt in part 3. That had to be set to a prescalar of 256 in order to get the output compare within the required 65536 imposed by the 16 bit timer. You'll see in the code we set OCR2A = 0xFA, which is 250. 

