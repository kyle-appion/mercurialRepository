
#include "Timer.h"

u32 Timer::GetDeltaTime() {
  u32 max = -1; // Used to manage a wrapping of micros.

  u32 now = micros();
  u32 dt = 0;

  if (now < lastTime) {
    dt = max - lastTime + now;
  } else {
    dt = now - lastTime;
  }

  lastTime = now;
  time += dt;

  return dt;
}

u64 Timer::GetTime() {
  GetDeltaTime();
  return time;
}
