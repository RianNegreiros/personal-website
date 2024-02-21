'use client'

import { FormEvent, useState } from 'react'
import { SignUpData } from '../models'
import { signUpUser } from '../utils/api'
import { useRouter } from 'next/navigation'
import Link from 'next/link'
import { useAuth } from '../contexts/AuthContext'

export default function SignUpPage() {
  const { setIsLogged } = useAuth()
  const [signingUp, setSigningUp] = useState(false)
  const [formData, setFormData] = useState<SignUpData>({
    Id: '',
    email: '',
    username: '',
    password: '',
    confirmPassword: '',
  })

  const router = useRouter()

  const handleInputChange = (event: FormEvent<HTMLInputElement>) => {
    const { name, value } = event.currentTarget
    setFormData({ ...formData, [name]: value })
  }

  const handleSignUp = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault()

    try {
      setSigningUp(true)
      const response = await signUpUser(formData)
      setIsLogged(true)
      localStorage.setItem('userId', response.data.id)
      localStorage.setItem('isAdmin', response.data.isAdmin)

      setSigningUp(false)
      router.push('/')
    } catch (error) {
      setSigningUp(false)
      console.error('Sign up error:', error)
    }
  }

  return (
    <section className='dark:bg-gray-900'>
      <div className='mx-auto flex flex-col items-center justify-center px-6 py-8 md:h-screen lg:py-0'>
        <div className='w-full rounded-lg bg-white shadow dark:border dark:border-gray-700 dark:bg-gray-800 sm:max-w-md md:mt-0 xl:p-0'>
          <div className='space-y-4 p-6 sm:p-8 md:space-y-6'>
            <h1 className='text-xl font-bold leading-tight tracking-tight text-gray-900 dark:text-white md:text-2xl'>
              Crie uma conta
            </h1>
            <form className='space-y-4 md:space-y-6' onSubmit={handleSignUp}>
              <div>
                <label
                  htmlFor='email'
                  className='mb-2 block text-sm font-medium text-gray-900 dark:text-white'
                >
                  Seu email
                </label>
                <input
                  id='email'
                  type='email'
                  name='email'
                  value={formData.email}
                  onChange={handleInputChange}
                  className='block w-full rounded-lg border border-gray-300 bg-gray-50 p-2.5 text-gray-900 focus:border-dracula-pink focus:ring-dracula-pink dark:border-gray-600 dark:bg-gray-700 dark:text-white dark:placeholder-gray-400 dark:focus:border-dracula-pink dark:focus:ring-dracula-pink sm:text-sm'
                  placeholder='name@mail.com'
                  required
                />
              </div>
              <div>
                <label
                  htmlFor='username'
                  className='mb-2 block text-sm font-medium text-gray-900 dark:text-white'
                >
                  Seu nome de usuário
                </label>
                <input
                  id='username'
                  type='username'
                  name='username'
                  value={formData.username}
                  onChange={handleInputChange}
                  required
                  className='block w-full rounded-lg border border-gray-300 bg-gray-50 p-2.5 text-gray-900 focus:border-dracula-pink focus:ring-dracula-pink dark:border-gray-600 dark:bg-gray-700 dark:text-white dark:placeholder-gray-400 dark:focus:border-dracula-pink dark:focus:ring-dracula-pink sm:text-sm'
                  placeholder='Nome de usuário'
                />
              </div>
              <div>
                <label
                  htmlFor='password'
                  className='mb-2 block text-sm font-medium text-gray-900 dark:text-white'
                >
                  Senha
                </label>
                <input
                  id='password'
                  type='password'
                  name='password'
                  value={formData.password}
                  onChange={handleInputChange}
                  placeholder='••••••••'
                  required
                  className='block w-full rounded-lg border border-gray-300 bg-gray-50 p-2.5 text-gray-900 focus:border-dracula-pink focus:ring-dracula-pink dark:border-gray-600 dark:bg-gray-700 dark:text-white dark:placeholder-gray-400 dark:focus:border-dracula-pink dark:focus:ring-dracula-pink sm:text-sm'
                />
              </div>
              <div>
                <label
                  htmlFor='confirmPassword'
                  className='mb-2 block text-sm font-medium text-gray-900 dark:text-white'
                >
                  Confirma senha
                </label>
                <input
                  id='confirmPassword'
                  type='password'
                  name='confirmPassword'
                  value={formData.confirmPassword}
                  onChange={handleInputChange}
                  placeholder='••••••••'
                  required
                  className='block w-full rounded-lg border border-gray-300 bg-gray-50 p-2.5 text-gray-900 focus:border-dracula-pink focus:ring-dracula-pink dark:border-gray-600 dark:bg-gray-700 dark:text-white dark:placeholder-gray-400 dark:focus:border-dracula-pink dark:focus:ring-dracula-pink sm:text-sm'
                />
              </div>
              <div className='mb-6 flex items-start'>
                <div className='flex h-5 items-center'>
                  <input
                    id='terms'
                    type='checkbox'
                    value=''
                    className='border-dracula-comment bg-dracula-background focus:ring-3 dark:bg-dracula-background dark:border-dracula-comment dark:ring-offset-dracula-background dark:focus:ring-offset-dracula-background h-4 w-4 rounded border focus:ring-dracula-pink dark:focus:ring-dracula-pink'
                    required
                  />
                </div>
                <label
                  htmlFor='terms'
                  className='text-dracula-foreground ml-2 text-sm font-medium dark:text-dracula-pink-400'
                >
                  Eu concordo com os
                  <Link
                    href='/terms/service'
                    target='_blank'
                    className='m-1 text-dracula-pink hover:underline dark:text-dracula-pink'
                  >
                    Termos de Serviço
                  </Link>
                  e
                  <Link
                    href='/terms/privacy'
                    target='_blank'
                    className='m-1 text-dracula-pink hover:underline dark:text-dracula-pink'
                  >
                    Política de Privacidade
                  </Link>
                </label>
              </div>
              <button
                type='submit'
                className={`text-dracula-background w-full rounded-lg bg-dracula-pink px-5 py-2.5 text-center text-sm font-medium hover:bg-dracula-pink-400 focus:outline-none focus:ring-4 focus:ring-gray-400 dark:bg-dracula-pink dark:hover:bg-dracula-pink-800 dark:focus:ring-dracula-pink-400 ${
                  signingUp ? 'cursor-not-allowed opacity-70' : ''
                }`}
                disabled={signingUp}
              >
                {signingUp ? 'Criando...' : 'Criar conta'}
              </button>
              <p className='text-dracula-comment text-sm font-light dark:text-gray-400'>
                Já possui uma conta?{' '}
                <Link
                  href='/signin'
                  className='font-medium text-dracula-pink hover:underline dark:text-dracula-pink'
                >
                  Entre aqui
                </Link>
              </p>
            </form>
          </div>
        </div>
      </div>
    </section>
  )
}
