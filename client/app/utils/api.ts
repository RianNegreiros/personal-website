import Axios from 'axios';
import { CommentData, PostData, ProjectData, SignInData, SignUpData } from '../models';

const API_URL = process.env.NEXT_PUBLIC_API_URL;

const axios = Axios.create({
  withCredentials: true,
});

axios.defaults.withCredentials = true;

async function signUpUser(formData: SignUpData): Promise<any> {
  try {
    const response = await axios.post(`${API_URL}/user/register`, formData);
    return response.data;
  } catch (error) {
    throw new Error('Sign up failed. Please try again later.');
  }
}

async function signInUser(formData: SignInData) {
  try {
    const response = await axios.post(`${API_URL}/user/login`, formData, {
      headers: {
        'Content-Type': 'application/json',
      }
    });

    return response.data;
  } catch (error) {
    throw new Error('Sign in failed. Please try again later.');
  }
}

async function createPost(formData: PostData) {
  formData.authorId = localStorage.getItem('userId') || sessionStorage.getItem('userId') as string;
  try {
    const response = await axios.post(`${API_URL}/posts`, formData, {
      headers: {
        'Content-Type': 'application/json'
      },
    });

    return response.data;
  } catch (error) {
    throw new Error('Failed to create post. Please try again later.');
  }
}

async function getPosts(pageNumber: number, pageSize: number) {
  try {
    const response = await axios.get(`${API_URL}/posts/?pageNumber=${pageNumber}&pageSize=${pageSize}`);
    return response.data;
  } catch (error) {
    throw new Error('Failed to fetch posts. Please try again later.');
  }
}

async function getFeed() {
  try {
    const response = await axios.get(`${API_URL}/feed`);
    return response.data;
  } catch (error) {
    throw new Error('Failed to fetch posts. Please try again later.');
  }
}

async function getProjects() {
  try {
    const response = await axios.get(`${API_URL}/projects`);
    return response.data;
  } catch (error) {
    throw new Error('Failed to fetch projects. Please try again later.');
  }
}

async function getCommentsForPost(postId: string) {
  try {
    const response = await axios.get(`${API_URL}/comments/${postId}`);
    return response.data;
  } catch (error) {
    throw new Error('Failed to fetch comments. Please try again later.');
  }
}

async function addCommentToPost(commentData: CommentData) {
  try {
    const userId = localStorage.getItem('userId') || sessionStorage.getItem('userId');
    commentData.id = userId as string;

    if (!userId) {
      throw new Error('User id not found.');
    }

    const response = await axios.post(`${API_URL}/comments/${commentData.postSlug}`, commentData);

    return response.data;
  } catch (error) {
    throw new Error('Failed to add comment. Please try again later.');
  }
}

async function createProject(projectData: ProjectData): Promise<any> {
  try {
    const formData = new FormData();
    formData.append('title', projectData.title);
    formData.append('url', projectData.url);
    formData.append('overview', projectData.overview);

    if (projectData.image !== null) {
      formData.append('image', projectData.image);
    }

    const response = await axios.post(`${API_URL}/projects`, formData, {
      headers: {
        'Content-Type': 'multipart/form-data'
      },
    });

    return response.data;
  } catch (error) {
    throw new Error('Failed to create project. Please try again later.');
  }
}

async function getPostBySlug(slug: string) {
  try {
    const response = await axios.get(`${API_URL}/posts/${slug}`);
    const data = response.data;
    JSON.stringify(data.content);
    return data;
  } catch (error) {
    throw new Error('Failed to fetch post by slug. Please try again later.');
  }
}

export {
  signUpUser,
  signInUser,
  createPost,
  getPostBySlug,
  getPosts,
  getCommentsForPost,
  addCommentToPost,
  createProject,
  getProjects,
  getFeed
};
