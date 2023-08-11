export interface Post {
  id: string
  title: string
  summary: string
  content: string
  createdAt: string
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
  token: string;
  isAdmin: boolean;
}

export interface Comment {
  id: string;
  author: { id: string, userName: string };
  content: string;
  createdAt: string;
}

export interface CommentData {
  postId: string;
  content: string;
  token: string;
}