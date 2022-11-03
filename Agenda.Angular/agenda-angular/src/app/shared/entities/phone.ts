import { Base } from './base';
import { Enumeration } from './enumeration';

export interface Phone extends Base{
  description: string;
  formattedPhone: string;
  ddd: number;
  number: string;
  phoneType: Enumeration;
  phoneTypeId: number;
}
