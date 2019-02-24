#include "timer.h"
#include "stopper.h"
#include <condition_variable>
#include <mutex>

void Timer::start() {
	_active = true;
	_processorThread = new std::thread(&Timer::command, this);
}

void Timer::stop() {
	_active = false;
	_processorThread->join();
}

void Timer::command()
{
	std::condition_variable _cond;
    std::mutex mu;
	while (_active){
	    std::unique_lock<std::mutex> lock(mu);
		_cond.wait_for(lock, milliseconds(1000));
		_target->send(tick);
	}
}
