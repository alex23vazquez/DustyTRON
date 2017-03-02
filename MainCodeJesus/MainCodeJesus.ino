/*********************************
DustyTron 3.0 - Software Team
**********************************/
#include<Servo.h> // servos are not used. Yet..

//Global Speed
int spd = 0; //4WD variable
int augspd = 0;

                          //Wheel Variables (The digital pins are directed to Arduino) (Other wire leads to Pololu)

//Polulu Motor Drive #1
/*******************(Front Wheels)************************/
//*******************(Front-Left Wheels)************************/
//WM means wheel motor
int WM1A_LeftFront = 25;// Digital Pin 25 of arduino to M1NA of Pololu
int WM1B_LeftFront = 26;// Digital pin 26 of arduino to M1NB of Pololu
int pwm_LeftFront = 2;// PWM Pin 2 of arduino to M1PWM
//Polulu Motor Drive #2
/*******************(Front-Right Wheels)************************/
int WM1A_RightFront = 27;// Digital Pin 27 of arduino to M1NA of Pololu
int WM1B_RightFront = 28;// Digital pin 28 of arduino to M1NB of Pololu
int pwm_RightFront = 3;// PWM Pin 3 of arduino to M1PWM

/*******************(Rear Wheels)************************/
//Polulu Motor Drive #3
/*******************(Back-Left Wheels)************************/
int WM1A_LeftBack = 29;//Digital Pin 29 of arduino to M1NA of Pololu
int WM1B_LeftBack = 30;//Digital pin 30 of arduino to M1NB of Pololu
int pwm_LeftBack = 4;// PWM Pin 4 of arduino to M1PWM
//Polulu Motor Drive #4
/*******************(Back-Right Wheels)************************/
int WM1A_RightBack = 31;//Digital Pin 31 of arduino to M1NA of Pololu
int WM1B_RightBack = 32;//Digital pin 32 of arduino to M1NB of Pololu
int pwm_RightBack = 5;// PWM Pin 5 of arduino to M1PWM

                          // Steering Actuators
//Pololu Motor Drive 5
// All these 4 actuators are connected to the same Pololu
//******************(Front wheel Actuator)*******************/
//SA means steering actuator
int SA1A_Front = 33; //digital pin 33 of arduino to M1NA of Pololu
int SA1B_Front = 34; // digital pin 34 of arduino to M1NB of Pololu
int pwm_Frontsteering = 6; // PWM Pin 6 of arduino to M1PWM
//******************(Rear wheel Actuator)********************/
int SA1A_Back = 35; //digital pin 35 of arduino to M2NA of Pololu
int SA1B_Back = 36; // digital pin 36 of arduino to M2NB of Pololu
int pwm_Backsteering = 7; // PWM Pin 7 of arduino to M2PWM
//************************************************************

                                // Actuators Motor Drives
//Polulu Motor Drive 6
//// All these 4 actuators are connected to the same Pololu
/*******************(Horizontal Auger, Up & Down)************************/
int AM1A_Horizontal = 37; //Digital Pin 37 of arduino to M1NA of Pololu
int AM1B_Horizontal = 38; //Digital pin 38 of arduino to M1NB of Pololu
int pwm_Horizontal = 8;// PWM Pin 8 of arduino to M1PWM
/*******************(Vertical Auger, Left & Right)************************/
int AM1A_Vertical = 39; // Digital Pin 39 of arduino to M2NA of Pololu
int AM1B_Vertical = 40; // Digital Pin 40 of arduino to M2NB of Pololu
int pwm_Vertical = 9; // PWM Pin 9 of arduino to M2PWM


                                //Conveyor Variables
//Polulu Motor Drive 7
/*******************(Conveyor)************************/
int CM1A = 41;//Digital Pin 41 of arduino to M1NA of Pololu
int CM1B = 42;//Digital pin 42 of arduino to M1NB of Pololu
int pwm_conveyor = 10;// PWM Pin 10 of arduino to M1PWM


//Augger Variables
Servo Augger;

//Inititialization
void setup() {
Serial.begin(9600); //What bound we are using (from serial monitor) it starts the connection

// pinMode() configures the specified pin to behave either as an input or output.
// syntax for it is pinMode(pin,mode)
  
// these pinModes are only used for all the actuators
pinMode(pwm_Horizontal, OUTPUT); //PWM_act - Pin 8
pinMode(AM1A_Horizontal, OUTPUT); //M1INA
pinMode(AM1B_Horizontal, OUTPUT); //M1INB

pinMode(pwm_Vertical, OUTPUT); //PWM_act - Pin 9
pinMode(AM1A_Vertical, OUTPUT); //M1INA
pinMode(AM1B_Vertical, OUTPUT); //M1INB

pinMode(pwm_Frontsteering, OUTPUT);
pinMode(SA1A_Front, OUTPUT); 
pinMode(SA1B_Front, OUTPUT); 

pinMode(pwm_Backsteering, OUTPUT);
pinMode(SA1A_Back, OUTPUT); 
pinMode(SA1B_Back, OUTPUT); 

pinMode(pwm_conveyor, OUTPUT); //PWM_con - Pin 10
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

//******************************(Conveyor Movement)*****************************//

//analogWrite() writes an analog value (PWM wave) to a pin.
//After a call to analogWrite(), the pin will generate a steady square wave of the specified 
//duty cycle until the next call to analogWrite() (or a call to digitalRead() or digitalWrite() on the same pin).
//You do not need to call pinMode() to set the pin as an output before calling analogWrite()
//syntax is analogWrite(pin,value) where pin is the one you want. and value is a pwm between 0 (off) and 255 (on).
  
if(command == 'x')
{
  setDirection_CON(0, CM1A, CM1B); // Stop - Conveyor Belt
  analogWrite(pwm_conveyor, 0);
  }
if(command == 'c')
{
  setDirection_CON(1, CM1A, CM1B); // Forward
  analogWrite(pwm_conveyor, 65);
  }
if(command == 'z')
{
  setDirection_CON(2, CM1A, CM1B); // Backward
  analogWrite(pwm_conveyor, 65);
  }

//******************************(Actuator Movement auger)*****************************//     
//********************Horizontal Movements********************

// digitalWrite() writes a high or low value to a digital pin
// syntax is digitalWrite(pin,value) where the value is either HIGH or LOW
//HIGH will enable max voltage and LOW will disable to ground OV
// if pin has been configured as an OUTPUT with pinMode(), its voltage will be set to the corresponding value.

// setDirection_ACT(int d, int M1INA, int M1INB) is another void function 
// the int d will either be 0(OFF), 1(Extend), or 2(Retract) 
// int M1INA and M1INB are gonna be HIGH or LOW to give all voltage, or ground wirelead
  
if(command == 'm')
{
  setDirection_ACT(0, AM1A_Horizontal, AM1B_Horizontal); //Stop - Actuator // (OFF, LOW, LOW)
  digitalWrite(pwm_Horizontal, HIGH);
  }
if(command == 'l') // moves the auger to the right (extends)
{
  setDirection_ACT(1, AM1A_Horizontal, AM1B_Horizontal); //Extend // (ON, HIGH, LOW)
  digitalWrite(pwm_Horizontal, HIGH);
  }
if(command == 'j') // moves the auger to the left (retracts)
{
  setDirection_ACT(2, AM1A_Horizontal, AM1B_Horizontal); //Retract // (ON, LOW, HIGH)
  digitalWrite(pwm_Horizontal, HIGH);
  }
  
//********************Vertical Movements******************** // we need to change the command inputs
  if(command == 'm')
{
  setDirection_ACT(0, AM1A_Vertical, AM1B_Vertical); //Stop - Actuator // (OFF, LOW, LOW)
  digitalWrite(pwm_Vertical, HIGH);
  }
if(command == 'k')
{
  setDirection_ACT(1, AM1A_Vertical, AM1B_Vertical); //auger  moves downward (Extends) // (ON, HIGH, LOW)
  digitalWrite(pwm_Vertical, HIGH);
  }
if(command == 'i') // auger moves upward (Retracts)
{
  setDirection_ACT(2, AM1A_Vertical, AM1B_Vertical); //Retract // (ON, LOW, HIGH)
  digitalWrite(pwm_Vertical, HIGH);
  }


  
//*******************************(4WD Movement)*********************************// 


if (command == '`') //KILL SWITCH the button below esc, left of 1
{
    augger_movement(Augger,'a'); //Stop Auger

    setDirection_CON(0, CM1A, CM1B); // Stop - Conveyor Belt
    analogWrite(pwm_conveyor, 0);

    setDirection_ACT(0, SA1A_Front, SA1B_Front); //Stop - Actuator for steering front
    digitalWrite(pwm_Frontsteering, HIGH);

    setDirection_ACT(0, SA1A_Back, SA1B_Back); //Stop - Actuator for steering back
    digitalWrite(pwm_Backsteering, HIGH);
    
    setDirection_ACT(0, AM1A_Horizontal, AM1B_Horizontal); //Stop - Actuator for auger horizontal
    digitalWrite(pwm_Horizontal, HIGH);

    setDirection_ACT(0, AM1A_Vertical, AM1B_Vertical); //Stop - Actuator for auger vertical
    digitalWrite(pwm_Vertical, HIGH);

    //victor(0, WM1A_LeftFront, WM1B_LeftFront); // Left Front Wheel STOP
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
  
if(command == '3'){  //Increment the speed
  if (spd == 255) // 255 is the max, if the speed is at 255 then it that value is printed.
  {
    Serial.print(spd);
  }else{ // if the value is not 255 when 2 is pressed then it will higher it by 15 each time
   spd+= 51;
  } 
}
if(command == '2'){  //Increment the speed
    spd == 153
    Serial.print(spd);
}

if(command == '3'){  //Decrement the speed
  if (spd == 0) // 0 is the minimum, if the speed is 0 then that value is printed.
  {
   Serial.print(spd);
  }else{ // if the value is not 0 then the speed will be decremented by 15 each time 1 is pressed.
   spd-= 51;
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
  victor(1, WM1A_LeftFront, WM1B_LeftFront); // forward or counter clockwise
  wheelSpeed(2, spd, pwm_LeftFront); //reactivate

  victor(1, WM1A_RightFront, WM1B_RightFront); // forward or counte clockwise
  wheelSpeed(2, spd, pwm_RightFront); //reactivate

  victor(1, WM1A_LeftBack, WM1B_LeftBack); // reactivate forward or counter clockwise
  wheelSpeed(2, spd, pwm_LeftBack);//reactivate

  victor(1, WM1A_RightBack, WM1B_RightBack); // reactivate forward or clockwise
  wheelSpeed(2, spd, pwm_RightBack);//reactivate


}

if(command == 's') // Front and Back Wheels going reverse
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

//**************************************************************************************
//*******************this part is for the actuators' movement **************************
  
if(command == 'v')// stop steering actuators have to check if this is correct
   {
setDirection_ACT(0, SA1A_Front , SA1B_Front ); // 
    digitalWrite(pwm_Frontsteering, HIGH);

    setDirection_ACT(0, SA1A_Back , SA1B_Back ); //
    digitalWrite(pwm_Backsteering , HIGH);
}

if(command == 'x')// Retract actuators to have diagonal wheels
   {
    setDirection_ACT(1, SA1A_Front , SA1B_Front ); // Extract Front steering actuators
    digitalWrite(pwm_Frontsteering, HIGH);

    setDirection_ACT(1, SA1A_Back , SA1B_Back ); // Extract Back steering actuators
    digitalWrite(pwm_Backsteering , HIGH);
  
    Delay (4000);
  
    digitalWrite(pwm_Frontsteering, LOW);// turn off actuators
    digitalWrite(pwm_Backsteering ,LOW); // turn off actuators
    Delay (1000);
  
    setDirection_ACT(2, SA1A_Front , SA1B_Front ); // Retract steering actuators
    digitalWrite(pwm_Frontsteering, HIGH);

    setDirection_ACT(2, SA1A_Back , SA1B_Back ); // Retract steering actuators
    digitalWrite(pwm_Backsteering , HIGH);
  
     Delay (2000);
  
    digitalWrite(pwm_Frontsteering, LOW);// turn off actuators
    digitalWrite(pwm_Backsteering ,LOW);// turn off actuators
}
  
if(command == 'z')// Retract actuators horizontal wheels
   {
setDirection_ACT(2, SA1A_Front , SA1B_Front ); // Retract steering actuators
    digitalWrite(pwm_Frontsteering, HIGH);

    setDirection_ACT(2, SA1A_Back , SA1B_Back ); // Retract steering actuators
    digitalWrite(pwm_Backsteering , HIGH);
}
if(command == 'c')// Extract actuators to have straight wheels
   {
setDirection_ACT(1, SA1A_Front , SA1B_Front ); // Extract Front steering actuators
    digitalWrite(pwm_Frontsteering, HIGH);

    setDirection_ACT(1, SA1A_Back , SA1B_Back ); // Extract Back steering actuators
    digitalWrite(pwm_Backsteering , HIGH);
}
//***********^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^****************************
//*************^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^****************************
if(command == 'd')// rotation clockwise or to turn right
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
           
if(command == 'a') // rotation counter clockwise or to the left
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
   
/**************************************************(End of Code)****************************************************************/
}






  



