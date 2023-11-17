export interface Record {
  id: number;
  date: string;
  time: number;
  user?: UserSafe;
}

export interface Text {
  id: number,
  textString: string
}

export interface UserSafe {
  Id: number;
  Username: string;
}

export interface ServiceResponse<T> {
  data?: T;
  success: boolean;
  message: string
}

export interface UserData {
  username: string;
  password: string
}

export interface UserCreds {
  username: string,
  token: string
}
