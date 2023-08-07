import { SignUpData } from "../models";

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

export { signUpUser };
