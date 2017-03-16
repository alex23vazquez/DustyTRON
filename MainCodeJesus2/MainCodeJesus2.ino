/*********************************
DustyTron 3.0 - Software Team
**********************************/
#include<Servo.h> // servos are not used. Yet..

Servo servo1;
Servo servo2;

#define s1pin 12
#define s2pin 13

int maxTilt = 175;
int minTilt = 5;
int maxPan = 175;
int minPan = 5;

int sstep = 15;
int sdel  = 10;

int s1pos = 90;
int s2pos = 90;

char serIn = ' ';
int changed = 0;
//**********************************************
//Global Speed
int spd = 0; //4WD variable
int augspd = 0;


                          //Wheel Variables (The digital pins are directed to Arduino) (Other wire leads to Pololu)

//Polulu Motor Drive #1
/*******************(Front Wheels)************************/
//*******************(Front-Left Wheels)************************/
//WM means wheel motor
int WM1A_LeftFront = 22;// Digital Pin 22 of arduino to M1NA of Pololu
int WM1B_LeftFront = 23;// Digital pin 23 of arduino to M1NB of Pololu
int pwm_LeftFront = 2;// PWM Pin 2 of arduino to M1PWM

//Polulu Motor Drive #2 
/*******************(Front-Right Wheels)************************/
int WM1A_RightFront = 24;// Digital Pin 24 of arduino to M1NA of Pololu
int WM1B_RightFront = 25;// Digital pin 25 of arduino to M1NB of Pololu
int pwm_RightFront = 3;// PWM Pin 3 of arduino to M1PWM

/*******************(Rear Wheels)************************/
//Polulu Motor Drive #3
/*******************(Back-Left Wheels)************************/
int WM1A_LeftBack = 26;//Digital Pin 26 of arduino to M1NA of Pololu
int WM1B_LeftBack = 27;//Digital pin 27 of arduino to M1NB of Pololu
int pwm_LeftBack = 4;// PWM Pin 4 of arduino to M1PWM

//Polulu Motor Drive #4
/*******************(Back-Right Wheels)************************/
int WM1A_RightBack = 28;//Digital Pin 28 of arduino to M1NA of Pololu
int WM1B_RightBack = 29;//Digital pin 29 of arduino to M1NB of Pololu
int pwm_RightBack = 5;// PWM Pin 5 of arduino to M1PWM

                          // Steering Actuators
//Pololu Motor Drive #5
// All these 4 actuators are connected to the same Pololu
//******************(Front wheel Actuator)*******************/
//SA means steering actuator
int SA1A_Front = 32; //digital pin 32 of arduino to M1NA of Pololu
int SA1B_Front = 33; // digital pin 33 of arduino to M1NB of Pololu
int pwm_Frontsteering = 6; // PWM Pin 6 of arduino to M1PWM
//******************(Rear wheel Actuator)********************/
int SA1A_Back = 34; //digital pin 34 of arduino to M2NA of Pololu
int SA1B_Back = 35; // digital pin 35 of arduino to M2NB of Pololu
int pwm_Backsteering = 7; // PWM Pin 7 of arduino to M2PWM
//************************************************************

                                // Actuators for Auger Motor Drives
//Polulu Motor Drive 6
//// All these 4 actuators are connected to the same Pololu
/*******************(Robot Lift, Up & Down)************************/
int LM1A_Back = 38; //Digital Pin 38 of arduino to M1NA of Pololu
int LM1B_Back = 39; //Digital pin 39 of arduino to M1NB of Pololu
int pwm_Back = 8;// PWM Pin 8 of arduino to M1PWM

int LM1A_Front = 40; //Digital Pin 40 of arduino to M1NA of Pololu
int LM1B_Front = 41; //Digital pin 41 of arduino to M1NB of Pololu
int pwm_Front = 9;// PWM Pin 9 of arduino to M1PWM
                                // Actuators for Lifting and Lowering Robot
//Polulu Motor Drive 7
//// Two sliding actuators are moving it up and down
/*******************(Vertical Auger)************************/
int AM1A_Vertical = 44; // Digital Pin 44 of arduino to M2NA of Pololu
int AM1B_Vertical = 45; // Digital Pin 45 of arduino to M2NB of Pololu
int pwm_Vertical = 10; // PWM Pin 9 of arduino to M2PWM
                                //Conveyor Variables
//Polulu Motor Drive 7 as well
/*******************(Conveyor)************************/
int CM1A = 48;//Digital Pin 48 of arduino to M1NA of Pololu
int CM1B = 49;//Digital pin 49 of arduino to M1NB of Pololu
int pwm_conveyor = 11;// PWM Pin 10 of arduino to M1PWM


//Augger Variables
Servo Augger;

//Inititialization
void setup() {
Serial.begin(9600); //What bound we are using (from serial monitor) it starts the connection

// pinMode() configures the specified pin to behave either as an input or output.
// syntax for it is pinMode(pin,mode)
  
// these pinModes are only used for all the actuators
pinMode(pwm_Back, OUTPUT); //PWM_act
pinMode(LM1A_Back, OUTPUT); //M1INA
pinMode(LM1B_Back, OUTPUT); //M1INB

pinMode(pwm_Front, OUTPUT); //PWM_act
pinMode(LM1A_Front, OUTPUT); //M1INA
pinMode(LM1B_Front, OUTPUT); //M1INB

pinMode(pwm_Vertical, OUTPUT); //PWM_act
pinMode(AM1A_Vertical, OUTPUT); //M1INA
pinMode(AM1B_Vertical, OUTPUT); //M1INB

pinMode(pwm_Frontsteering, OUTPUT);
pinMode(SA1A_Front, OUTPUT); 
pinMode(SA1B_Front, OUTPUT); 

pinMode(pwm_Backsteering, OUTPUT);
pinMode(SA1A_Back, OUTPUT); 
pinMode(SA1B_Back, OUTPUT); 

pinMode(pwm_conveyor, OUTPUT); //PWM_con
pinMode(CM1A, OUTPUT); // M2INA
pinMode(CM1B, OUTPUT); // M2INB

Augger.attach(8); //Augger utilizes pin 8
}

//Main Code.
void loop() 
{
//******(9600 bound)******//
char command = Serial.read();

//******************************(Augger)*****************************//  
// the augger speeds are modified
// the different speeds are removed and now there is only full reverse, stop, full excavate

if(command =='8')
{
augger_movement(Augger,'a'); // Stops the Augger
}

if(command =='9')
{
augger_movement(Augger,'b'); // Excavate - Speed 1
}
//if(command =='7'){
//augger_movement(Augger,'c'); // Excavate - Speed 2
//}

if(command =='0')
{
augger_movement(Augger,'d'); // MAX Excavate Speed 3
}

if(command =='7'){
augger_movement(Augger,'e'); // Reverse Aug - Speed 1
}
//if(command =='>'){
//augger_movement(Augger,'f'); // Reverse Aug - Speed 2
//}

if(command =='6')
{
augger_movement(Augger,'g'); // MAX Reverse Aug Speed 3
}


//**************************camera pan tilt***********************************
if(command =='y')
{
 tiltUp();
}

if(command =='t')
{
 tiltDown();
}
if(command =='h')
{
 panRight();
}
if(command =='g')
{
 panLeft();
}
if(command =='b')
{
 resetAll();
}
 
//**************************end of camera mount code***********************************
//******************************(Conveyor Movement)*****************************//

//analogWrite() writes an analog value (PWM wave) to a pin.
//After a call to analogWrite(), the pin will generate a steady square wave of the specified 
//duty cycle until the next call to analogWrite() (or a call to digitalRead() or digitalWrite() on the same pin).
//You do not need to call pinMode() to set the pin as an output before calling analogWrite()
//syntax is analogWrite(pin,value) where pin is the one you want. and value is a pwm between 0 (off) and 255 (on).
  
if(command == 'l')
{
  setDirection_CON(0, CM1A, CM1B); // Stop - Conveyor Belt
  analogWrite(pwm_conveyor, 0);
  }
if(command == '.')
{
  setDirection_CON(1, CM1A, CM1B); // Forward
  analogWrite(pwm_conveyor, 65);
  }
if(command == ',')
{
  setDirection_CON(2, CM1A, CM1B); // Backward
  analogWrite(pwm_conveyor, 65);
  }
   
//********************Robot lifting and lowering********************

// digitalWrite() writes a high or low value to a digital pin
// syntax is digitalWrite(pin,value) where the value is either HIGH or LOW
//HIGH will enable max voltage and LOW will disable to ground OV
// if pin has been configured as an OUTPUT with pinMode(), its voltage will be set to the corresponding value.

// setDirection_ACT(int d, int M1INA, int M1INB) is another void function 
// the int d will either be 0(OFF), 1(Extend), or 2(Retract) 
// int M1INA and M1INB are gonna be HIGH or LOW to give all voltage, or ground wirelead
  
if(command == 'n') //stops the robot lifting or lowering
{
  setDirection_ACT(0, LM1A_Back, LM1B_Back); //Stop - Actuator // (OFF, LOW, LOW)
  setDirection_ACT(0, LM1A_Front, LM1B_Front); //Stop - Actuator // (OFF, LOW, LOW)
  digitalWrite(pwm_Back, HIGH);
  digitalWrite(pwm_Front, HIGH);
  }
if(command == 'u') // moves the robot up (extends)
{
  setDirection_ACT(2, LM1A_Back, LM1B_Back); //Extend // (ON, HIGH, LOW)
  setDirection_ACT(2, LM1A_Front, LM1B_Front);
  digitalWrite(pwm_Back, HIGH);
  digitalWrite(pwm_Front, HIGH);
  }
if(command == 'j') // moves the robot down (retracts) 
{
  setDirection_ACT(1, LM1A_Back, LM1B_Back); //Retract // (ON, LOW, HIGH)
  setDirection_ACT(1, LM1A_Front, LM1B_Front);
  digitalWrite(pwm_Back, HIGH);
  digitalWrite(pwm_Front, HIGH);
  }
  
//******************************(Actuator Movement auger)*****************************//  
//********************Vertical Movements******************** // we need to change the command inputs
  if(command == 'm')
{
  setDirection_ACT(0, AM1A_Vertical, AM1B_Vertical); //Stop - Actuator // (OFF, LOW, LOW)
  digitalWrite(pwm_Vertical, HIGH);
  }
if(command == 'i') // auger moves upward (retract)
{
  setDirection_ACT(1, AM1A_Vertical, AM1B_Vertical); //auger  moves downward (Extends) // (ON, HIGH, LOW)
  digitalWrite(pwm_Vertical, HIGH);
  }
if(command == 'k') // auger moves downward (extracts)
{
  setDirection_ACT(2, AM1A_Vertical, AM1B_Vertical); //Retract // (ON, LOW, HIGH)
  digitalWrite(pwm_Vertical, HIGH);
  }


  
//*******************************(4WD Movement)*********************************// 


if (command == '`') //KILL SWITCH the button below esc, left of 1
{
    augger_movement(Augger,'a'); //Stop Auger

    setDirection_ACT(0, LM1A_Back, LM1B_Back); //Stop - Actuator // (OFF, LOW, LOW)
    digitalWrite(pwm_Back, HIGH);
    
    setDirection_ACT(0, LM1A_Front, LM1B_Front); //Stop - Actuator // (OFF, LOW, LOW)
    digitalWrite(pwm_Front, HIGH);

    setDirection_CON(0, CM1A, CM1B); // Stop - Conveyor Belt
    analogWrite(pwm_conveyor, 0);

    setDirection_ACT(0, SA1A_Front, SA1B_Front); //Stop - Actuator for steering front
    digitalWrite(pwm_Frontsteering, HIGH);

    setDirection_ACT(0, SA1A_Back, SA1B_Back); //Stop - Actuator for steering back
    digitalWrite(pwm_Backsteering, HIGH);

    setDirection_ACT(0, AM1A_Vertical, AM1B_Vertical); //Stop - Actuator for auger vertical
    digitalWrite(pwm_Vertical, HIGH);
    
    analogWrite(pwm_LeftFront, 0);
    analogWrite(pwm_RightFront, 0);
    analogWrite(pwm_LeftBack, 0);
    analogWrite(pwm_RightBack, 0);
}



//*********************************** 4WControl Speeds *****************************
//We need to decide if we need to change the spd and spd2
// i think spd was for one side paired wheels
// spd2 was the other side paired wheels
//spd2 hasnt been used anywhere

// print() prints data to the serial port as human-readable ACII text.
// example is Serial.print(78) gives "78"
// syntax is Serial.print(val) or Serial.print(val,format) where format is in either BIN, OCT, DEC, HEX,
  
if(command == '2'){  //Increment the speed
  if (spd == 255) // 255 is the max, if the speed is at 255 then it that value is printed.
  {
    Serial.print(spd);
  }else{ // if the value is not 255 when 2 is pressed then it will higher it by 15 each time
   spd+= 15;
  } 
}

if(command == '1'){  //Decrement the speed
  if (spd == 0) // 0 is the minimum, if the speed is 0 then that value is printed.
  {
   Serial.print(spd);
  }else{ // if the value is not 0 then the speed will be decremented by 15 each time 1 is pressed.
   spd-= 15;
   }
}


//^^^^^^^^^^^^^^^^^^^^^^^^^^^Robot Movements^^^^^^^^^^^^^^^^^^^^^
if(command == 'f') // need to find a different key that is not q maybe f
{

  analogWrite(pwm_LeftFront, 0);
  analogWrite(pwm_RightFront, 0);
  analogWrite(pwm_LeftBack, 0);
  analogWrite(pwm_RightBack, 0);
}

if(command == 'w')  //front and back wheels going forward
{
 victor(1, WM1A_LeftFront, WM1B_LeftFront); // reactivate forward
  wheelSpeed(2, spd, pwm_LeftFront);//reactivate

  victor(2, WM1A_RightFront, WM1B_RightFront); // reactivate reverse
  wheelSpeed(2, spd, pwm_RightFront);//reactivate

  victor(1, WM1A_LeftBack, WM1B_LeftBack); // reactivate forward
  wheelSpeed(2, spd, pwm_LeftBack);//reactivate

  victor(2, WM1A_RightBack, WM1B_RightBack); // reactivate reverse
  wheelSpeed(2, spd, pwm_RightBack);//reactivate
}

if(command == 's') // Front and Back Wheels going reverse
{
  victor(2, WM1A_LeftFront, WM1B_LeftFront); // reactivate reverse
  wheelSpeed(2, spd, pwm_LeftFront);//reactivate

  victor(1, WM1A_RightFront, WM1B_RightFront); // reactivate forward
  wheelSpeed(2, spd, pwm_RightFront);//reactivate

  victor(2, WM1A_LeftBack, WM1B_LeftBack); // reactivate reverse
  wheelSpeed(2, spd, pwm_LeftBack);//reactivate

  victor(1, WM1A_RightBack, WM1B_RightBack); // reactivate forward
  wheelSpeed(2, spd, pwm_RightBack);//reactivate
}


if(command == 'r')// stop steering actuators have to check if this is correct
   {
    
    setDirection_ACT(0, SA1A_Front , SA1B_Front ); // 
    digitalWrite(pwm_Frontsteering, HIGH);

    setDirection_ACT(0, SA1A_Back , SA1B_Back ); //
    digitalWrite(pwm_Backsteering , HIGH);
}


if(command == 'q')// Retract actuators to have diagonal wheels
   {
    setDirection_ACT(2, SA1A_Front , SA1B_Front ); // Retract steering actuators
    digitalWrite(pwm_Frontsteering, HIGH);

    setDirection_ACT(2, SA1A_Back , SA1B_Back ); // Retract steering actuators
    digitalWrite(pwm_Backsteering , HIGH);
}
if(command == 'e')// Extract actuators to have straight wheels
   {
setDirection_ACT(1, SA1A_Front , SA1B_Front ); // Extract Front steering actuators
    digitalWrite(pwm_Frontsteering, HIGH);

    setDirection_ACT(1, SA1A_Back , SA1B_Back ); // Extract Back steering actuators
    digitalWrite(pwm_Backsteering , HIGH);
}

if(command == 'd')// rotation clockwise or to turn right
{
victor(1, WM1A_LeftFront, WM1B_LeftFront); // forward or clockwise
  wheelSpeed(2, spd, pwm_LeftFront); //reactivate

  victor(1, WM1A_RightFront, WM1B_RightFront); // forward or clockwise
  wheelSpeed(2, spd, pwm_RightFront); //reactivate

  victor(1, WM1A_LeftBack, WM1B_LeftBack); // reactivate forward or clockwise
  wheelSpeed(2, spd, pwm_LeftBack);//reactivate

  victor(1, WM1A_RightBack, WM1B_RightBack); // reactivate forward or clockwise
  wheelSpeed(2, spd, pwm_RightBack);//reactivate
}
           
if(command == 'a') // rotation counter clockwise or to the left
{

victor(2, WM1A_LeftFront, WM1B_LeftFront); // reactivate reverse or counterclockwise
  wheelSpeed(2, spd, pwm_LeftFront);//reactivate

  victor(2, WM1A_RightFront, WM1B_RightFront); // reactivate reverse or counterclockwise
  wheelSpeed(2, spd, pwm_RightFront);//reactivate

  victor(2, WM1A_LeftBack, WM1B_LeftBack); // reactivate reverse or counterclockwise
  wheelSpeed(2, spd, pwm_LeftBack);//reactivate

  victor(2, WM1A_RightBack, WM1B_RightBack); // reactivate reverse or counterclockwise
  wheelSpeed(2, spd, pwm_RightBack);//reactivate


}
   
/**************************************************(End of Code)****************************************************************/
}






  



