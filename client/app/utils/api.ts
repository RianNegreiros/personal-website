import { SignInData, SignUpData } from "../models";

async function signUpUser(formData: SignUpData) {
  try {
    const response = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/user/register`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(formData),
    });

    if (!response.ok) {
      const errorData = await response.json();
      throw new Error(errorData.message || "Sign up failed.");
    }

    const responseData = await response.text();
    if (!responseData) {
      throw new Error("Empty response received from the server.");
    }

    try {
      return JSON.parse(responseData);
    } catch (error) {
      throw new Error("Failed to parse response from the server.");
    }
  } catch (error) {
    throw new Error("Sign up failed. Please try again later.");
  }
}

async function signInUser(formData: SignInData) {
  try {
    const response = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/user/login`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(formData),
    });

    if (!response.ok) {
      const errorData = await response.json();
      throw new Error(errorData.message || "Sign in failed.");
    }

    const responseData = await response.text();
    if (!responseData) {
      throw new Error("Empty response received from the server.");
    }

    try {
      return JSON.parse(responseData);
    } catch (error) {
      throw new Error("Failed to parse response from the server.");
    }
  } catch (error) {
    throw new Error("Sign in failed. Please try again later.");
  }
}

async function checkUserLoggedIn() {
  try {
    const response = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/user/me`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
    });

    if (!response.ok) {
      return null;
    }

    return response.json();
  } catch (error) {
    return null;
  }
}

export { signUpUser, signInUser, checkUserLoggedIn }