//Pololu Motor Driver

//FUNCTION FOR ACTUATORS
void setDirection_ACT(int d, int M1INA, int M1INB)
{
  switch(d){
    case 0://OFF
      digitalWrite (M1INA, LOW);
      digitalWrite (M1INB, LOW);
      break;
    case 1://Retract
      digitalWrite (M1INA, HIGH);
      digitalWrite (M1INB, LOW);
      break;
    case 2://Extend
      digitalWrite (M1INA, LOW);
      digitalWrite (M1INB, HIGH);
      break;
     }
}

//FUNCTION FOR CONVEYOR
void setDirection_CON(int x, int M2INA, int M2INB)
{
  switch(x){
    case 0://STOP
      digitalWrite (M2INA, LOW);
      digitalWrite (M2INB, LOW);
      break;
    case 1://FORWARD
      digitalWrite (M2INA, HIGH);
      digitalWrite (M2INB, LOW);
      break;
    case 2://BACKWARD
      digitalWrite (M2INA, LOW);
      digitalWrite (M2INB, HIGH);
      break;
    }
}


