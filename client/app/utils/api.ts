import Axios, { AxiosResponse } from 'axios'
import {
  CommentData,
  CreateUser,
  PostData,
  ProjectData,
  SignInData,
  SignUpData,
} from '@/app/models'

const API_URL = process.env.NEXT_PUBLIC_API_URL

const axios = Axios.create({
  withCredentials: true,
})

axios.defaults.withCredentials = true

async function signUpUser(formData: SignUpData): Promise<AxiosResponse> {
  try {
    const response = await axios.post(`${API_URL}/user/register`, formData)
    return response.data
  } catch (error) {
    throw new Error('Sign up failed. Please try again later.')
  }
}

async function createUser(formData: CreateUser): Promise<void> {
  try {
    await axios.post(`${API_URL}/admin/users`, formData)
  } catch (error) {
    throw new Error('User creation failed. Please try again later.')
  }
}

async function signInUser(formData: SignInData) {
  try {
    const response = await axios.post(`${API_URL}/user/login`, formData, {
      headers: {
        'Content-Type': 'application/json',
      },
    })

    return response.data
  } catch (error) {
    throw new Error('Sign in failed. Please try again later.')
  }
}

async function createPost(formData: PostData) {
  formData.authorId =
    localStorage.getItem('userId') ||
    (sessionStorage.getItem('userId') as string)
  try {
    const response = await axios.post(`${API_URL}/posts`, formData, {
      headers: {
        'Content-Type': 'application/json',
      },
    })

    return response.data
  } catch (error) {
    throw new Error('Failed to create post. Please try again later.')
  }
}

async function getPosts(
  pageNumber: number,
  pageSize: number,
): Promise<AxiosResponse> {
  try {
    const response = await axios.get(
      `${API_URL}/posts/?pageNumber=${pageNumber}&pageSize=${pageSize}`,
    )
    return response.data
  } catch (error) {
    console.error(error)
    throw new Error('Failed to fetch posts. Please try again later.')
  }
}

async function getPostsSuggestions(count: number, excludeSlug: string) {
  try {
    const response = await axios.get(
      `${API_URL}/posts/suggestions/${count}?excludeSlug=${excludeSlug}`,
    )
    return response.data
  } catch (error) {
    throw new Error('Failed to fetch posts. Please try again later.')
  }
}

async function checkSession() {
  try {
    const response = await axios.get(`${API_URL}/user/me`)
    return response.data
  } catch (error) {
    throw new Error('Failed to check user session. Please try again later.')
  }
}

async function getAdminPosts() {
  try {
    const response = await axios.get(`${API_URL}/admin/posts`)
    return response.data
  } catch (error) {
    throw new Error('Failed to fetch posts. Please try again later.')
  }
}

async function getAdminUsers() {
  try {
    const response = await axios.get(`${API_URL}/admin/users`)
    return response.data
  } catch (error) {
    throw new Error('Failed to fetch posts. Please try again later.')
  }
}

async function getFeed() {
  try {
    const response = await axios.get(`${API_URL}/feed`)
    return response.data
  } catch (error) {
    throw new Error('Failed to fetch the feed. Please try again later.')
  }
}

async function getProjects() {
  try {
    const response = await axios.get(`${API_URL}/projects`)
    return response.data
  } catch (error) {
    throw new Error('Failed to fetch projects. Please try again later.')
  }
}

async function getCommentsForPost(postId: string) {
  try {
    const response = await axios.get(`${API_URL}/comments/${postId}`)
    return response.data
  } catch (error) {
    throw new Error('Failed to fetch comments. Please try again later.')
  }
}

async function addCommentToPost(commentData: CommentData) {
  try {
    const userId =
      localStorage.getItem('userId') || sessionStorage.getItem('userId')
    commentData.authorId = userId as string

    if (!userId) {
      throw new Error('User id not found.')
    }

    const response = await axios.post(
      `${API_URL}/comments/${commentData.postSlug}`,
      commentData,
    )

    return response.data
  } catch (error) {
    throw new Error('Failed to add comment. Please try again later.')
  }
}

export const addReplyToComment = async (
  identifier: string,
  replyData: { content: string; authorId: string },
) => {
  const userId =
    localStorage.getItem('userId') || sessionStorage.getItem('userId')
  replyData.authorId = userId as string
  const response = await axios.post(
    `${API_URL}/comments/${identifier}/replies`,
    replyData,
  )
  return response.data
}

async function createProject(projectData: ProjectData): Promise<AxiosResponse> {
  try {
    const formData = new FormData()
    formData.append('title', projectData.title)
    formData.append('url', projectData.url)
    formData.append('overview', projectData.overview)

    if (projectData.image !== null) {
      formData.append('image', projectData.image)
    }

    const response = await axios.post(`${API_URL}/projects`, formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    })

    return response.data
  } catch (error) {
    throw new Error('Failed to create project. Please try again later.')
  }
}

async function getPostBySlug(slug: string) {
  try {
    const response = await axios.get(`${API_URL}/posts/${slug}`)
    const data = response.data
    JSON.stringify(data.content)
    return data
  } catch (error) {
    throw new Error('Failed to fetch post by slug. Please try again later.')
  }
}

async function deleteAdminPost(id: string) {
  try {
    await axios.delete(`${API_URL}/admin/posts/${id}`)
  } catch (error) {
    throw new Error('Failed to delete post. Please try again later.')
  }
}

async function deleteAdminUser(id: string) {
  try {
    await axios.delete(`${API_URL}/admin/users/${id}`)
  } catch (error) {
    throw new Error('Failed to delete post. Please try again later.')
  }
}

async function logoutUser() {
  try {
    await axios.get(`${API_URL}/user/logout`)
  } catch (error) {
    throw new Error('Failed to logout user. Please try again later.')
  }
}

async function subscribeNewsLetter(email: string) {
  try {
    await axios.post(`${API_URL}/subscribers/subscribe`, { email: email })
  } catch (error) {
    throw new Error(
      'Failed to subscribe user to newsletter. Please try again later.',
    )
  }
}

export {
  signUpUser,
  signInUser,
  createPost,
  getPostBySlug,
  getPosts,
  getPostsSuggestions,
  getCommentsForPost,
  addCommentToPost,
  createProject,
  getProjects,
  getFeed,
  getAdminPosts,
  deleteAdminPost,
  getAdminUsers,
  deleteAdminUser,
  createUser,
  logoutUser,
  checkSession,
  subscribeNewsLetter,
}
