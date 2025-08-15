export interface AddStudentRequest {
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  email: string;
  mobile: string;               // string olmalı
  genderId: string;
  physicalAdress: string;       // backend ile aynı isim
  postalAdress: string;
}
