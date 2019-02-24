#ifndef TIMER_H
#define TIMER_H

#include <chrono>
#include <thread>
#include "event.h"

class Stopper;

class Timer
{
	typedef std::chrono::milliseconds milliseconds;
public:
	Timer(Stopper *t) : _target(t), _active(false) {  }
	void start();
	void stop();

private:
	void command();

	Stopper *_target;
	bool _active;
	std::thread  *_processorThread;
};

#endif // TIMER_H
