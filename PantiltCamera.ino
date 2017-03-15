

void jsonAxis() {
  Serial.print(180 - s1pos);
  Serial.print(',');
  Serial.println(s2pos);
}

void resetAll() {
  centerTilt();
  centerPan();
}

int centerTilt() {
  servo1.attach(s1pin);
  delay(sdel);
  s1pos = 90;
  servo1.write(s1pos);
  delay(sdel * 10);
  servo1.detach();
  return s1pos;
}

int tiltDown() {
  servo1.attach(s1pin);
  delay(sdel);
  int newStep = 1;
  while((newStep < sstep) && (s1pos < maxTilt)) {
    s1pos++;
    newStep++;
    servo1.write(s1pos);
    delay(sdel);
  }
  servo1.detach();
  return s1pos;
}

int tiltUp() {
  servo1.attach(s1pin);
  delay(sdel);
  int newStep = 1;
  while((newStep < sstep) && (s1pos > minTilt)) {
    s1pos--;
    newStep++;
    servo1.write(s1pos);
  delay(sdel);
  }
  servo1.detach();
  return s1pos;
}

int centerPan() {
  servo1.attach(s2pin);
  delay(sdel);
  s2pos = 90;
  servo2.write(s2pos);
  delay(sdel * 10);
  servo2.detach();
  return s2pos;
}

int panLeft() {
  servo2.attach(s2pin);
  delay(sdel);
  int newStep = 1;
  while((newStep < sstep) && (s2pos < maxPan)) {
    s2pos++;
    newStep++;
    servo2.write(s2pos);
    delay(sdel);
  }
  servo2.detach();
  return s2pos;
}

int panRight() {
  servo2.attach(s2pin);
  delay(sdel);
  int newStep = 1;
  while((newStep < sstep) && (s2pos > minPan)) {
    s2pos--;
    newStep++;
    servo2.write(s2pos);
  delay(sdel);
  }
  servo2.detach();
  return s2pos;
}



