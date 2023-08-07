"use client"

import { FormEvent, useState } from "react"
import { SignInData } from "../models"
import { signInUser } from "../utils/api";
import { useRouter } from "next/navigation";

export default function SignInPage() {
  const [formData, setFormData] = useState<SignInData>({
    email: "",
    password: "",
  })

  const router = useRouter();

  const handleInputChange = (event: FormEvent<HTMLInputElement>) => {
    const { name, value } = event.currentTarget;
    setFormData({ ...formData, [name]: value });
  };

  const handleSignIn = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    try {
      await signInUser(formData);
      console.log("Sign in successful!");

      router.push("/");
    } catch (error) {
      console.error("Sign in error:", error);
    }
  }

  return (
    <div className="relative flex flex-col items-center justify-center min-h-screen overflow-hidden">
      <div className="w-full p-6 bg-white rounded-md shadow-md lg:max-w-xl">
        <h1 className="text-3xl font-bold text-center text-gray-900">Login to your account</h1>
        <form className="mt-6" onSubmit={handleSignIn}>
          <div className="mb-4">
            <label
              htmlFor="email"
              className="block text-sm font-semibold text-gray-900"
            >
              Email
            </label>
            <input
              type="email"
              name="email"
              value={formData.email}
              onChange={handleInputChange}
              className="block w-full px-4 py-2 mt-2 text-gray-900 bg-white border rounded-md focus:border-gray-400 focus:ring-gray-300 focus:outline-none focus:ring focus:ring-opacity-40"
            />
          </div>
          <div className="mb-2">
            <label
              htmlFor="password"
              className="block text-sm font-semibold text-gray-900"
            >
              Password
            </label>
            <input
              type="password"
              name="password"
              value={formData.password}
              onChange={handleInputChange}
              className="block w-full px-4 py-2 mt-2 text-gray-900 bg-white border rounded-md focus:border-gray-400 focus:ring-gray-300 focus:outline-none focus:ring focus:ring-opacity-40"
            />
          </div>
          <div className="mt-2">
            <button
              type="submit"
              className="w-full px-4 py-2 tracking-wide text-white transition-colors duration-200 transform bg-teal-500 rounded-md hover:bg-teal-600 focus:outline-none focus:bg-teal-600">
              Login
            </button>
          </div>
        </form>
      </div>
    </div>
  )
}