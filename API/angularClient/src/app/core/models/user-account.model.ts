export interface userAccount {
  id?: number;
  username: string;
  currentPassword: string;
  newPassword?: string;
  firstName?: string;
  lastName?: string;
  location?: string;
  dateOfBirth?: Date;
}
