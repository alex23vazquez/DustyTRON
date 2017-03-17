
//Pololu Motor Drivers
void augerwrite(int c, int A, int B){
       if (c == 0) // OFF
{
            digitalWrite(A,LOW);
            digitalWrite(B, LOW);
       }
       if (c == 1) // Foward or clockwise
{
            digitalWrite(A,HIGH);
            digitalWrite(B, LOW);
       }
       if (c == 2) // Back or counterclockwise
{ 
            digitalWrite(A,LOW);
            digitalWrite(B, HIGH);
       }
  }

void augerSpeed(int CASE, int SPEED, int PWM){
  switch(CASE){
    case 2: //decrement speed , I AM NOT SURE WHY IT SAYS decrement speed, might be a mistake
      analogWrite(PWM, SPEED);
      Serial.print(SPEED);
      break; 
    }
  }
  




















/*
 THE RANGES SHOULD BE FROM 0 - 250 OR 0 - 180
 FULL REVERSE (0) 
 REVERSE (1-89)
 STOP(90)
 FORWARD (91-179) OR (91-249)
 FULL FORWARD (180) OR (250)
//FUNCTION FOR AUGGER MOVEMENT
void augger_movement(Servo y, char cmd){

//Stop
if (cmd == 'a')
{
  y.write(90);
  }
//Excavate
if (cmd == 'b')
{

  y.write(135); // it was 120 before
  
  }
//if (cmd == 'c')
//{

  //y.write(150);
  
  //}
if (cmd == 'd')
{
  y.write(180);  // MAX SPEED-3
  }
//Retract
if (cmd == 'e')
{
  y.write(45);  // Speed-1 // it was 60 before
  }
//if (cmd == 'f')
//{
  //y.write(30);  // Speed-2
  //}
if (cmd == 'g'){
  y.write(0);  // MAX SPEED-3
  }
}
*/



