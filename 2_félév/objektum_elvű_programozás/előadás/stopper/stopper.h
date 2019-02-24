#ifndef STOPPER_H
#define STOPPER_H

#include <thread>
#include <atomic>
#include "lcdnumber.h"
#include "event.h"
#include "timer.h"
#include "threadsafequeue.h"

class Stopper
{
public:
    Stopper();
    ~Stopper();

    void send(Signal event) { _eventQueue.enqueue(event); }
private:
	enum State {operate, stopped};

	void stateMachine();
    void transition(Signal event);
    Signal getSignal() { Signal s; _eventQueue.dequeue(s); return s; }

    Timer _timer;
    LcdNumber _lcd;
    ThreadSafeQueue<Signal> _eventQueue;

    State _currentState;
	int _seconds;
    bool _active;
    std::thread _processorThread;
};

#endif // STOPPER_H
