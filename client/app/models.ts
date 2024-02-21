export interface Post {
  id: string
  title: string
  summary: string
  content: string
  slug: string
  createdAt: string
  updatedAt: string
}

export interface PostData {
  authorId: string
  title: string
  summary: string
  content: string
}

export interface UserData {
  id: string
  userName: string
  email: string
  token: string
}

export interface UserAdminData {
  id: string
  username: string
  email: string
  posts: [{ title: string; slug: string }]
  comments: [{ content: string; postSlug: string }]
  isAdmin: boolean
}

export interface SignUpData {
  Id: string
  email: string
  username: string
  password: string
  confirmPassword: string
}

export interface CreateUser {
  username?: string
  email?: string
  password?: string
  admin: boolean
}

export interface SignInData {
  Id: string
  email: string
  password: string
  rememberMe: boolean
  token: string
  isAdmin: boolean
}

export interface Comment {
  id: string
  authorId: string
  authorUsername: string
  content: string
  createdAt: string
  replies: Reply[]
}

export interface CommentData {
  postSlug: string
  content: string
  authorId: string
}

export interface Reply {
  id: string
  authorId: string
  authorEmail: string
  authorUsername: string
  replies: Reply
  createdAt: string
  updatedAt: string
  content: string
}

export interface InputReply {
  id: string
  authorId: string
  content: string
}

export interface Project {
  id: string
  title: string
  overview: string
  url: string
  imageUrl: string
  createdAt: string
  updatedAt: string
}

export interface ProjectData {
  title: string
  overview: string
  url: string
  image: File | null
}

export type FeedItem = {
  id: string
  title: string
  summary?: string
  content?: string
  slug?: string
  createdAt: Date
  updatedAt: Date
  url?: string
  imageUrl?: string
  overview?: string
  type: string // "Post" or "Project"
}
