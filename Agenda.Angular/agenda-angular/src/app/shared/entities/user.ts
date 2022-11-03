import { Base } from './base';
import { Enumeration } from './enumeration';

export interface User extends Base{
  name: string;
  userName: string;
  password: string;
  email: string;
  userRole: Enumeration;
  userRoleId?: number;
}
