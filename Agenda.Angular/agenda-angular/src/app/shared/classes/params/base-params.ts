import { HttpParams } from "@angular/common/http";

export class BaseParams extends HttpParams{
  [key: string]: any;
  skip = 0;
  take = 5;
}
