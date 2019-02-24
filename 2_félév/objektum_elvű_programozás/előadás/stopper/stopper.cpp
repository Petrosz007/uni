#include "stopper.h"

Stopper::Stopper() :  _timer(this), _processorThread(&Stopper::stateMachine, this)
{
    _eventQueue.startQueue();
}

//a destruktorban termináljuk az esemény feldolgozó szálat
Stopper::~Stopper()
{
    _processorThread.join();
	_eventQueue.stopQueue();
}

void Stopper::stateMachine()
{
    _seconds = 0;
	_lcd.display(_seconds);
	_timer.start();
	_currentState = stopped;
    _active = true;
    while(_active) { //amíg nincs terminálás
        if(_active) {
            transition(getSignal()); //kiveszünk egy eseményt a sorból
        }
    }
    _timer.stop();
}

void Stopper::transition(Signal sign)
{
    switch (_currentState) { // milyen állapotban vagyunk
        case stopped:
            switch (sign) { // mi a szignál
                case click: _currentState = operate; break;
                case tick : break;
                case quit : _active = false; break;
            }
            break;
        case operate:
            switch (sign) { // mi a szignál
                case click: _currentState = stopped; break;
                case tick : _lcd.display(++_seconds); break;
                case quit : _active = false; break;
            }
            break;
    }
}
