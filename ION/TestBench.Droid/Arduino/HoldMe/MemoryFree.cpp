
#include "MemoryFree.h"

extern unsigned int __heap_start;
extern void* __brkval;

// the free list structure as maintained by the avr-libc memory allocator
struct __freelist {
  size_t sz;
  struct __freelist* nx;
};

// The head of the free list structure
extern struct __freelist* __flp;

int readBlocks() {
  struct __freelist* current;
  int total = 0;

  for (current = __flp; current; current = current->nx) {
    total += 2; // Add the two bytes that are memory block's header
    total += (int)current->sz;
  }

  return total;
}

int memoryAvailable() {
  int ret = 0;

  if ((int)__brkval == 0) {
    ret = ((int)&ret) - ((int)&__heap_start);
  } else {
    ret = ((int)&ret) - ((int)__brkval);
    ret += readBlocks();
  }

  return ret;
}
