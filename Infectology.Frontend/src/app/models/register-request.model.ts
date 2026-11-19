export interface RegisterRequest {
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  titleBefore: string | null;
  titleAfter: string | null;
  email: string;
  phoneNumber: string | null;
  mobileNumber: string | null;
  personalCity: string;
  personalAddress: string;
  personalPostalCode: string;
  fax: string | null;
  workplaceAddress: string;
  workplacePostalCode: string;
  workplaceCity: string;
  workplacePhone: string | null;
  workplaceEmail: string | null;
  workplaceFax: string | null;
  workplaceFunction: string;
  focus: string;
  graduationYear: number;
  chamber: string;
  chamberId: string;
  password: string;
}