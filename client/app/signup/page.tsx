"use client"

import { FormEvent, useState } from "react"
import { SignUpData } from "../models"
import { signUpUser } from "../utils/api";
import { useRouter } from "next/navigation";
import Link from "next/link";
import { useAuth } from "../contexts/AuthContext";

export default function SignUpPage() {
  const { setIsLogged } = useAuth();
  const [signingUp, setSigningUp] = useState(false);
  const [formData, setFormData] = useState<SignUpData>({
    Id: "",
    email: "",
    username: "",
    password: "",
    confirmPassword: "",
  })

  const router = useRouter();

  const handleInputChange = (event: FormEvent<HTMLInputElement>) => {
    const { name, value } = event.currentTarget;
    setFormData({ ...formData, [name]: value });
  };

  const handleSignUp = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    try {
      setSigningUp(true);
      const response = await signUpUser(formData);
      setIsLogged(true);
      localStorage.setItem("userId", response.data.id);
      localStorage.setItem("token", response.data.token);

      setSigningUp(false);
      router.push("/");
    } catch (error) {
      setSigningUp(false);
      console.error("Sign up error:", error);
    }
  };

  return (
    <section className="dark:bg-gray-900">
      <div className="flex flex-col items-center justify-center px-6 py-8 mx-auto md:h-screen lg:py-0">
        <div className="w-full bg-white rounded-lg shadow dark:border md:mt-0 sm:max-w-md xl:p-0 dark:bg-gray-800 dark:border-gray-700">
          <div className="p-6 space-y-4 md:space-y-6 sm:p-8">
            <h1 className="text-xl font-bold leading-tight tracking-tight text-gray-900 md:text-2xl dark:text-white">
              Crie uma conta
            </h1>
            <form className="space-y-4 md:space-y-6" onSubmit={handleSignUp}>
              <div>
                <label
                  htmlFor="email"
                  className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
                >
                  Seu email
                </label>
                <input
                  id="email"
                  type="email"
                  name="email"
                  value={formData.email}
                  onChange={handleInputChange}
                  className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                  placeholder="name@mail.com"
                  required
                />
              </div>
              <div>
                <label
                  htmlFor="username"
                  className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
                >
                  Seu nome de usuário
                </label>
                <input
                  id="username"
                  type="username"
                  name="username"
                  value={formData.username}
                  onChange={handleInputChange}
                  required
                  className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                  placeholder="Nome de usuário"
                />
              </div>
              <div>
                <label
                  htmlFor="password"
                  className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
                >
                  Senha
                </label>
                <input
                  id="password"
                  type="password"
                  name="password"
                  value={formData.password}
                  onChange={handleInputChange}
                  placeholder="••••••••"
                  required
                  className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                />
              </div>
              <div>
                <label
                  htmlFor="confirmPassword"
                  className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
                >Confirma senha
                </label>
                <input
                  id="confirmPassword"
                  type="password"
                  name="confirmPassword"
                  value={formData.confirmPassword}
                  onChange={handleInputChange}
                  placeholder="••••••••"
                  required
                  className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                />
              </div>
              <div className="flex items-start mb-6">
                <div className="flex items-center h-5">
                  <input id="terms" type="checkbox" value="" className="w-4 h-4 border border-gray-300 rounded bg-gray-50 focus:ring-3 focus:ring-blue-300 dark:bg-gray-700 dark:border-gray-600 dark:focus:ring-blue-600 dark:ring-offset-gray-800 dark:focus:ring-offset-gray-800" required />
                </div>
                <label
                  htmlFor="terms"
                  className="ml-2 text-sm font-medium text-gray-900 dark:text-gray-300">
                  Eu concordo com os
                  <Link href="/terms/service" target="_blank" className="m-1 text-teal-600 hover:underline dark:text-teal-500"
                  >
                    Termos de Serviço
                  </Link>
                  e
                  <Link href="/terms/privacy" target="_blank" className="m-1 text-teal-600 hover:underline dark:text-teal-500">
                    Política de Privacidade
                  </Link>
                </label>
              </div>
              <button
                type="submit"
                className={`w-full text-white bg-teal-500 hover:bg-primary-700 focus:ring-4 focus:outline-none focus:ring-primary-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800 ${signingUp ? "opacity-70 cursor-not-allowed" : ""
                  }`}
                disabled={signingUp}
              >
                {signingUp ? "Criando..." : "Criar conta"}
              </button>
              <p className="text-sm font-light text-gray-500 dark:text-gray-400">
                Já possui uma conta? <Link href="/signin" className="font-medium text-teal-500 hover:underline dark:text-primary-500">Entre aqui</Link>
              </p>
            </form>
          </div>
        </div>
      </div>
    </section>
  )
}