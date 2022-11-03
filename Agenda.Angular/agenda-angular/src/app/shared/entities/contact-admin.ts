import { User } from './user';
import { Contact } from './contact';

export interface ContactAdmin extends Contact{
  user: User;
  userId?: number;
}
