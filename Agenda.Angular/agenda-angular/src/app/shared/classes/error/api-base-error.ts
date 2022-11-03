export interface ApiBaseError {
  errors: ErrorObject[];
  message: string;
}

interface ErrorObject {
  propertyName: string;
  errorMessage: string;
  attemptedValue: object;
  errorCode: number;
}
