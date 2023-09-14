import axios from 'axios';
import { CommentData, PostData, ProjectData, SignInData, SignUpData } from '../models';

const API_URL = process.env.NEXT_PUBLIC_API_URL;

function getAuthorizationHeader(): Record<string, string> {
  const token = localStorage.getItem('token') || sessionStorage.getItem('token');
  return {
    'Content-Type': 'application/json',
    Authorization: `Bearer ${token}`,
  };
}

async function signUpUser(formData: SignUpData): Promise<any> {
  try {
    const response = await axios.post(`${API_URL}/user/register`, formData, {
      headers: getAuthorizationHeader(),
    });
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
      },
    });

    return response.data;
  } catch (error) {
    throw new Error('Sign in failed. Please try again later.');
  }
}

async function getIsAdmin() {
  try {
    const response = await axios.get(`${API_URL}/user/isadmin`, {
      headers: {
        ...getAuthorizationHeader(),
      },
    });
    return response.data;
  } catch (error) {
    throw new Error('Failed to fetch user. Please try again later.');
  }
}

async function createPost(formData: PostData) {
  formData.authorId = localStorage.getItem('userId') || sessionStorage.getItem('userId') as string;
  formData.content = JSON.stringify(formData.content);
  try {
    const response = await axios.post(`${API_URL}/post`, formData, {
      headers: {
        'Content-Type': 'application/json',
        ...getAuthorizationHeader(),
      },
    });

    return response.data;
  } catch (error) {
    throw new Error('Failed to create post. Please try again later.');
  }
}

async function autoLoginUser(token: string) {
  const response = await axios.post(
    `${API_URL}/user/autologin`,
    JSON.stringify(token),
    {
      headers: {
        'Content-Type': 'application/json',
      },
    }
  );

  return response.data;
}

async function getPosts() {
  try {
    const response = await axios.get(`${API_URL}/post`);
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
    const token = localStorage.getItem('token');
    commentData.token = token as string;

    if (!token) {
      throw new Error('User token not found.');
    }

    const response = await axios.post(`${API_URL}/comments/${commentData.postSlug}`, commentData, {
      headers: {
        ...getAuthorizationHeader(),
      },
    });

    return response.data;
  } catch (error) {
    throw new Error('Failed to add comment. Please try again later.');
  }
}

async function getIsUserLoggedIn() {
  try {
    const response = await axios.get(`${API_URL}/user/me`, {
      headers: {
        ...getAuthorizationHeader(),
      },
    });
    return response.data;
  } catch (error) {
    throw new Error('Failed to fetch user. Please try again later.');
  }
}

async function logoutUser(): Promise<any> {
  try {
    const response = await axios.post(`${API_URL}/user/logout`, null, {
      headers: {
        ...getAuthorizationHeader(),
      },
    });
    return response.data;
  } catch (error) {
    throw new Error('Logout failed. Please try again later.');
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
        'Content-Type': 'multipart/form-data',
        Authorization: `Bearer ${localStorage.getItem('token') || sessionStorage.getItem('token')}`,
      },
    });

    return response.data;
  } catch (error) {
    throw new Error('Failed to create project. Please try again later.');
  }
}

async function getPostBySlug(slug: string) {
  try {
    const response = await axios.get(`${API_URL}/post/${slug}`);
    const data = response.data;
    data.content = JSON.parse(data.content);
    return data;
  } catch (error) {
    throw new Error('Failed to fetch post by slug. Please try again later.');
  }
}

export {
  signUpUser,
  signInUser,
  autoLoginUser,
  getIsAdmin,
  logoutUser,
  getIsUserLoggedIn,
  createPost,
  getPostBySlug,
  getPosts,
  getCommentsForPost,
  addCommentToPost,
  createProject,
  getProjects
};
