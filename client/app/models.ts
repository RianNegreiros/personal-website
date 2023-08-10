export interface Post {
  id: string
  title: string
  summary: string
  content: string
}

export interface PostData {
  authorId: string
  title: string
  summary: string
  content: string
}

export interface UserData {
  id: string;
  username: string;
  email: string;
  token: string;
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
  rememberMe: boolean;
  isAdmin: boolean;
}