import axios from 'axios';
import { CommentData, PostData, ProjectData, SignInData, SignUpData } from '../models';

async function signUpUser(formData: SignUpData) {
  try {
    const response = await axios.post(`${process.env.NEXT_PUBLIC_API_URL}/user/register`, formData, {
      headers: {
        'Content-Type': 'application/json',
      },
    });

    return response.data;
  } catch (error) {
    throw new Error('Sign up failed. Please try again later.');
  }
}

async function signInUser(formData: SignInData) {
  try {
    const response = await axios.post(`${process.env.NEXT_PUBLIC_API_URL}/user/login`, formData, {
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
    const response = await axios.get(`${process.env.NEXT_PUBLIC_API_URL}/user/isadmin`, {
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${localStorage.getItem('token')}`,
      },
    });
    return response.data;
  } catch (error) {
    throw new Error('Failed to fetch user. Please try again later.');
  }
}

async function createPost(formData: PostData) {
  formData.authorId = localStorage.getItem('userId') as string;
  try {
    const response = await axios.post(`${process.env.NEXT_PUBLIC_API_URL}/post`, formData, {
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${localStorage.getItem('token')}`,
      },
    });

    return response.data;
  } catch (error) {
    throw new Error('Failed to create post. Please try again later.');
  }
}

async function autoLoginUser(token: string) {
  const response = await axios.post(
    `${process.env.NEXT_PUBLIC_API_URL}/user/autologin`,
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
    const response = await axios.get(`${process.env.NEXT_PUBLIC_API_URL}/post`);
    return response.data;
  } catch (error) {
    throw new Error('Failed to fetch posts. Please try again later.');
  }
}

async function getProjects() {
  try {
    const response = await axios.get(`${process.env.NEXT_PUBLIC_API_URL}/projects`);
    return response.data;
  } catch (error) {
    throw new Error('Failed to fetch projects. Please try again later.');
  }
}

async function getCommentsForPost(postId: string) {
  try {
    const response = await axios.get(`${process.env.NEXT_PUBLIC_API_URL}/comments/${postId}`);
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

    const response = await axios.post(`${process.env.NEXT_PUBLIC_API_URL}/comments/${commentData.postSlug}`, commentData, {
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`,
      },
    });

    return response.data;
  } catch (error) {
    throw new Error('Failed to add comment. Please try again later.');
  }
}

async function getIsUserLoggedIn() {
  try {
    const response = await axios.get(`${process.env.NEXT_PUBLIC_API_URL}/user/me`, {
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${localStorage.getItem('token')}`,
      },
    });
    return response.data;
  } catch (error) {
    throw new Error('Failed to fetch user. Please try again later.');
  }
}

async function logoutUser() {
  try {
    const response = await axios.post(`${process.env.NEXT_PUBLIC_API_URL}/user/logout`, null, {
      headers: {
        'Authorization': `Bearer ${localStorage.getItem('token')}`,
      },
    });

    return response.data;
  } catch (error) {
    throw new Error('Logout failed. Please try again later.');
  }
}

async function createProject(projectData: ProjectData) {
  try {
    const formData = new FormData();
    formData.append('title', projectData.title);
    formData.append('url', projectData.url);
    formData.append('overview', projectData.overview);

    if (projectData.image !== null) {
      formData.append('image', projectData.image);
    }

    const response = await axios.post(`${process.env.NEXT_PUBLIC_API_URL}/projects`, formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
        Authorization: `Bearer ${localStorage.getItem('token')}`,
      },
    });

    return response.data;
  } catch (error) {
    throw new Error('Failed to create project. Please try again later.');
  }
}

async function getPostBySlug(slug: string) {
  try {
    const response = await axios.get(`${process.env.NEXT_PUBLIC_API_URL}/post/${slug}`);
    return response.data;
  } catch (error) {
    throw new Error('Failed to fetch post by slug. Please try again later.');
  }
}

export {
  signUpUser,
  signInUser,
  getIsAdmin,
  createPost,
  autoLoginUser,
  getCommentsForPost,
  addCommentToPost,
  getPosts,
  getIsUserLoggedIn,
  createProject,
  getProjects,
  logoutUser,
  getPostBySlug
};
