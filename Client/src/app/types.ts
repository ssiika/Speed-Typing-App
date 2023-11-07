export interface Record {
  id: number;
  length: string;
  time: number;
  user?: UserSafe;
}

export interface UserSafe {
  Id: number;
  Username: string;
}

export interface ServiceResponse<T> {
  data: T;
  success: boolean;
  message: string
}
