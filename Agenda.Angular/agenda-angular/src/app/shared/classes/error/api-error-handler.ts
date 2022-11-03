import { MatSnackBar } from "@angular/material/snack-bar";
import { ApiBaseError } from "./api-base-error";

export function ApiErrorHandler(snackBar: MatSnackBar, error: ApiBaseError) {
  console.error(error);
  snackBar.open(error.errors[0].errorMessage, undefined, { duration: 3000 });
}
