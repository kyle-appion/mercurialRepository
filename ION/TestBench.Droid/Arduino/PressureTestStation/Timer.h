
#pragma once

#include "Appion.h"

class Timer {
public:
  /*
  Creates a new timer that will maintain delta time.
  */
  Timer() {
  }
  /*
  Queries the amount of time that has ellapsed since the last time that delta
  time was called.
  */
  u32 GetDeltaTime();

  // Resets the timer. This is done to ensure a fresh delta time and should be called when the timer needs to be reused.
  void Reset() {
    lastTime = 0;
  }
  /*
  Queries the total ellapsed microseconds since the timer was created.
  */
  u64 GetTime();

private:
  // The last time that delta time was called.
  u32 lastTime;
  // The total ellapsed time that timer has known of.
  u64 time;
};
