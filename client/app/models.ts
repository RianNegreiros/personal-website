export interface Post {
  id: string
  title: string
  body: string
}

export interface SignUpData {
  email: string;
  username: string;
  password: string;
  confirmPassword: string;
}

export interface SignInData {
  email: string;
  password: string;
}