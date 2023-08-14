export interface Post {
  id: string
  title: string
  summary: string
  content: string
  slug: string
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
  Id: string;
  email: string;
  username: string;
  password: string;
  confirmPassword: string;
}

export interface SignInData {
  Id: string;
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
  postSlug: string;
  content: string;
  token: string;
}

export interface Project {
  id: string;
  title: string;
  overview: string;
  url: string;
  imageUrl: string;
}

export interface ProjectData {
  title: string;
  overview: string;
  url: string;
  image: File | null;
}