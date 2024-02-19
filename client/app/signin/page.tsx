"use client"

import { FormEvent, useState } from "react"
import { SignInData } from "../models"
import { signInUser } from "../utils/api";
import { useRouter } from "next/navigation";
import { useAuth } from "../contexts/AuthContext";
import Link from "next/link";

export default function SignInPage() {
  const [loggingIn, setLoggingIn] = useState(false);
  const [formData, setFormData] = useState<SignInData>({
    Id: "",
    email: "",
    password: "",
    token: "",
    rememberMe: false,
    isAdmin: false
  })

  const router = useRouter();
  const { setIsAdmin, setIsLogged } = useAuth();

  const handleInputChange = (event: FormEvent<HTMLInputElement>) => {
    const { name, value, type, checked } = event.currentTarget;
    const inputValue = type === "checkbox" ? checked : value;
    setFormData({ ...formData, [name]: inputValue });
  };

  const handleSignIn = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    try {
      setLoggingIn(true);
      const response = await signInUser(formData);

      setIsAdmin(response.data.isAdmin)
      setIsLogged(true);

      if (formData.rememberMe) {
        localStorage.setItem("userId", response.data.id);
        localStorage.setItem("isAdmin", response.data.isAdmin);
      } else {
        sessionStorage.setItem("userId", response.data.id);
        sessionStorage.setItem("isAdmin", response.data.isAdmin);

        localStorage.removeItem("userId");
        localStorage.removeItem("isAdmin");
      }

      setLoggingIn(false);
      router.push("/");
    } catch (error) {
      setLoggingIn(false);
      console.error("Sign in error:", error);
    }
  }

  return (
    <section className="dark:bg-gray-900">
      <div className="flex flex-col items-center justify-center px-6 py-8 mx-auto md:h-screen lg:py-0">
        <div className="w-full bg-white rounded-lg shadow dark:border md:mt-0 sm:max-w-md xl:p-0 dark:bg-gray-800 dark:border-gray-700">
          <div className="p-6 space-y-4 md:space-y-6 sm:p-8">
            <h1 className="text-xl font-bold leading-tight tracking-tight text-gray-900 md:text-2xl dark:text-white">
              Acesse sua conta
            </h1>
            <form className="space-y-4 md:space-y-6" onSubmit={handleSignIn}>
              <div>
                <label
                  htmlFor="email"
                  className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
                >
                  Seu email
                </label>
                <input
                  type="email"
                  name="email"
                  value={formData.email}
                  onChange={handleInputChange}
                  id="email"
                  className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-dracula-pink focus:border-dracula-pink block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-dracula-pink dark:focus:border-dracula-pink"
                  placeholder="name@mail.com"
                />
              </div>
              <div>
                <label
                  htmlFor="password"
                  className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
                >
                  Sua senha
                </label>
                <input
                  type="password"
                  name="password"
                  value={formData.password}
                  onChange={handleInputChange}
                  id="password"
                  placeholder="••••••••"
                  className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-dracula-pink focus:border-dracula-pink block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-dracula-pink dark:focus:border-dracula-pink"
                />
              </div>
              <div className="flex items-center justify-between">
                <div className="flex items-start">
                  <div className="flex items-center h-5">
                    <input
                      id="remember"
                      type="checkbox"
                      name="rememberMe"
                      checked={formData.rememberMe}
                      onChange={handleInputChange}
                      aria-describedby="remember"
                      className="w-4 h-4 border border-gray-300 rounded bg-gray-50 focus:ring-3 focus:ring-dracula-pink dark:bg-gray-700 dark:border-gray-600 dark:focus:ring-dracula-pink dark:ring-offset-gray-800"
                    />
                  </div>
                  <div className="ml-3 text-sm">
                    <label
                      htmlFor="remember"
                      className="text-gray-400 dark:text-gray-300"
                    >
                      Lembrar de mim
                    </label>
                  </div>
                </div>
              </div>
              <button
                type="submit"
                className={`w-full text-white bg-dracula-pink hover:bg-primary-700 focus:ring-4 focus:outline-none focus:ring-dracula-pink font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-dracula-pink dark:hover:bg-dracula-pink-800 dark:focus:ring-dracula-pink ${loggingIn ? "opacity-70 cursor-not-allowed" : ""
                  }`}
                disabled={loggingIn}
              >
                {loggingIn ? "Logando..." : "Login"}
              </button>
              <p className="text-sm font-light text-gray-500 dark:text-gray-400">
                Ainda não tem uma conta ? <Link href="/signup" className="font-medium text-dracula-pink hover:underline dark:text-dracula-pink">Registre-se</Link>
              </p>
            </form>
          </div>
        </div>
      </div>
    </section>
  )
}