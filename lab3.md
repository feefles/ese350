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

    
