import { Base } from './base';
import { Phone } from './phone';

export interface Contact extends Base{
  name: string;
  phones: Phone[];
}
