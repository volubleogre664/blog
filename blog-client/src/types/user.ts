export type User = {
  [key: string]: any;

  userId: number;
  firstname: string;
  lastname: string;
  email: string;
  bio?: string;
  userProfile?: string;
  identityAccountId?: string;
};
