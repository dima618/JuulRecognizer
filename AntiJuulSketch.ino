#include <ESP32Servo.h>

#define SERVO1 23
#define SERVO2 18
#define motorpin 13

Servo y_servo;
Servo x_servo;

String buf;

void setup() {
  x_servo.attach(SERVO1);
  y_servo.attach(SERVO2);
  pinMode(motorpin, OUTPUT);
  pinMode(2, OUTPUT);
  Serial.begin(9600);
  Serial.setTimeout(10); //defaults to 1000
}

void loop() {
  // put your main code here, to run repeatedly:
  if(Serial.available() > 0){
    digitalWrite(2, HIGH);
    buf = Serial.readString();
    x_servo.write(parsex(buf));
    y_servo.write(parsey(buf));
    if(parsef(buf)){
      fire();
    }
  }
}

/*
void serialEvent(){
  
} */

int parsex(String buff){
  buff.remove(buff.indexOf(":"));
  buff.remove(buff.indexOf("X"), 1);
  return buff.toInt();
}

int parsey(String buff){
  buff.remove(0, buff.indexOf(":") + 2);
  if(buff.charAt(buff.length()-1) == 'F'){
    buff.remove(buff.length()-2);
  }
  return buff.toInt();
}

bool parsef(String buff){
  if(buff.charAt(buff.length()-1) == 'F'){
    return true;
  }
  return false;
}

void fire(){
  for(int i = 0; i<4; i++){
    digitalWrite(motorpin, HIGH);
    delay(1200);
    digitalWrite(motorpin, LOW);
    delay(1200);
  }
}
