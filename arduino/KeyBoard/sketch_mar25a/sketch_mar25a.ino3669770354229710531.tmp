
int buttonState[12];
int lastButtonState[12];

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  pinMode(2, INPUT_PULLUP);
  pinMode(3, INPUT_PULLUP);
  pinMode(4, INPUT_PULLUP);
  pinMode(5, INPUT_PULLUP);
  pinMode(6, INPUT_PULLUP);
  pinMode(7, INPUT_PULLUP);
  pinMode(8, INPUT_PULLUP);
  pinMode(9, INPUT_PULLUP);
  pinMode(10, INPUT_PULLUP);
  pinMode(11, INPUT_PULLUP);
  pinMode(12, INPUT_PULLUP);
  pinMode(13, INPUT_PULLUP);
}

void loop() {
  int i;
  for(i = 0; i < 12; i++){
    buttonState[i] = digitalRead(i + 2);
    if(buttonState[i] != lastButtonState[i]){
      if(buttonState[i] == LOW){
        Serial.write(i + 65);
      }
      else{
        Serial.write(i + 97);
      }
      delay(15);
    }
    lastButtonState[i] = buttonState[i];
  }
}
