/*
 THE RANGES SHOULD BE FROM 0 - 250 OR 0 - 180
 FULL REVERSE (0) 
 REVERSE (1-89)
 STOP(90)
 FORWARD (91-179) OR (91-249)
 FULL FORWARD (180) OR (250)
 */
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

//BELOW IS THE CODE TO OPERATE THE AUGGER.  YOU NEED TO MODIFY IT, IT WONT WORK IF COPY N PASTED!
///************************************************/
//#include <Servo.h>
///************************************************/
//Servo myservo;// Jaguar motor driver
//void setup()
//{
//  Serial.begin(9600);
//  myservo.attach(7);//attachs the servo on pin 7 to servo object
//}
///*************************************************/
//void loop()
//{ 
//  char command = Serial.read();
//
//////// Forward Motions /////
//  if(command == 'a'){
//    myservo.write(150);
//    delay(2000);
//    }
//
//   if (command == 's')
//   {
//    myservo.write(100);
//    delay(2000);
//    }
//
//   if (command == 'd')
//   {
//    myservo.write(250);
//    delay(2000);
//    }
//
////Perfect steady and slow fowrward initial speed
//  if (command == 'g')
//   {
//    myservo.write(120);
//    delay(2000);
//    }
//
//    if (command == 'y')
//   {
//    myservo.write(99);
//    delay(2000);
//    }
//
//if (command == 'h')
//   {
//    myservo.write(105);
//    delay(2000);
//    }
//
// if (command == 'z')
//   {
//    myservo.write(110);
//    delay(2000);
//    }
//  
////// Reverse Motions ////
//   
//   if (command == 'x')
//   {
//    myservo.write(0);
//    delay(2000);
//    }
//
//  if (command == 'p')
//   {
//    myservo.write(75);
//    delay(2000);
//    }
//
//  if (command == 'w')
//   {
//    myservo.write(85);
//    delay(2000);
//    }
//
//   if (command == 'v')
//   {
//    myservo.write(25);
//    delay(2000);
//    }
//
//    if (command == 'l')
//   {
//    myservo.write(35);
//    delay(2000);
//    }
//
//  if (command == 'n')
//   {
//    myservo.write(45);
//    delay(2000);
//    }
//
//  if (command == 't')
//   {
//    myservo.write(50);
//    delay(2000);
//    }
//    
//// stops //
//    if (command == 'i')
//   {
//    myservo.write(90);
//    delay(2000);
//    }
//}


