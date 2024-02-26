'use client'

import { FormEvent, useState } from 'react'
import { SignInData } from '../models'
import { signInUser } from '../utils/api'
import { useRouter } from 'next/navigation'
import { useAuth } from '../contexts/AuthContext'
import Link from 'next/link'

export default function SignInPage() {
  const [loggingIn, setLoggingIn] = useState(false)
  const [formData, setFormData] = useState<SignInData>({
    Id: '',
    email: '',
    password: '',
    token: '',
    rememberMe: false,
    isAdmin: false,
  })

  const router = useRouter()
  const { setIsAdmin, setIsLogged } = useAuth()

  const handleInputChange = (event: FormEvent<HTMLInputElement>) => {
    const { name, value, type, checked } = event.currentTarget
    const inputValue = type === 'checkbox' ? checked : value
    setFormData({ ...formData, [name]: inputValue })
  }

  const handleSignIn = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault()

    try {
      setLoggingIn(true)
      const response = await signInUser(formData)

      setIsAdmin(response.data.isAdmin)
      setIsLogged(true)

      if (formData.rememberMe) {
        localStorage.setItem('userId', response.data.id)
        localStorage.setItem('isAdmin', response.data.isAdmin)
      } else {
        sessionStorage.setItem('userId', response.data.id)
        sessionStorage.setItem('isAdmin', response.data.isAdmin)

        localStorage.removeItem('userId')
        localStorage.removeItem('isAdmin')
      }

      setLoggingIn(false)
      router.push('/')
    } catch (error) {
      setLoggingIn(false)
      console.error('Sign in error:', error)
    }
  }

  return (
    <section className='dark:bg-gray-900'>
      <div className='mx-auto flex flex-col items-center justify-center px-6 py-8 md:h-screen lg:py-0'>
        <div className='w-full rounded-lg bg-white shadow dark:border dark:border-gray-700 dark:bg-gray-800 sm:max-w-md md:mt-0 xl:p-0'>
          <div className='space-y-4 p-6 sm:p-8 md:space-y-6'>
            <h1 className='text-xl font-bold leading-tight tracking-tight text-gray-900 dark:text-white md:text-2xl'>
              Acesse sua conta
            </h1>
            <form className='space-y-4 md:space-y-6' onSubmit={handleSignIn}>
              <div>
                <label
                  htmlFor='email'
                  className='mb-2 block text-sm font-medium text-gray-900 dark:text-white'
                >
                  Seu email
                </label>
                <input
                  type='email'
                  name='email'
                  value={formData.email}
                  onChange={handleInputChange}
                  id='email'
                  className='block w-full rounded-lg border border-gray-300 bg-gray-50 p-2.5 text-gray-900 focus:border-dracula-purple focus:ring-dracula-purple dark:border-gray-600 dark:bg-gray-700 dark:text-white dark:placeholder-gray-400 dark:focus:border-dracula-purple dark:focus:ring-dracula-purple sm:text-sm'
                  placeholder='name@mail.com'
                />
              </div>
              <div>
                <label
                  htmlFor='password'
                  className='mb-2 block text-sm font-medium text-gray-900 dark:text-white'
                >
                  Sua senha
                </label>
                <input
                  type='password'
                  name='password'
                  value={formData.password}
                  onChange={handleInputChange}
                  id='password'
                  placeholder='••••••••'
                  className='block w-full rounded-lg border border-gray-300 bg-gray-50 p-2.5 text-gray-900 focus:border-dracula-purple focus:ring-dracula-purple dark:border-gray-600 dark:bg-gray-700 dark:text-white dark:placeholder-gray-400 dark:focus:border-dracula-purple dark:focus:ring-dracula-purple sm:text-sm'
                />
              </div>
              <div className='flex items-center justify-between'>
                <div className='flex items-start'>
                  <div className='flex h-5 items-center'>
                    <input
                      id='remember'
                      type='checkbox'
                      name='rememberMe'
                      checked={formData.rememberMe}
                      onChange={handleInputChange}
                      aria-describedby='remember'
                      className='focus:ring-3 h-4 w-4 rounded border border-gray-300 bg-gray-50 focus:ring-dracula-purple dark:border-gray-600 dark:bg-gray-700 dark:ring-offset-gray-800 dark:focus:ring-dracula-purple'
                    />
                  </div>
                  <div className='ml-3 text-sm'>
                    <label
                      htmlFor='remember'
                      className='text-gray-400 dark:text-gray-300'
                    >
                      Lembrar de mim
                    </label>
                  </div>
                </div>
              </div>
              <button
                type='submit'
                className={`w-full rounded-lg bg-dracula-purple px-5 py-2.5 text-center text-sm font-medium text-white transition-colors duration-200 ease-in-out hover:bg-dracula-purple-700 focus:outline-none focus:ring-4 focus:ring-dracula-purple dark:bg-dracula-purple dark:hover:bg-dracula-purple-800 dark:focus:ring-dracula-purple ${loggingIn ? 'cursor-not-allowed opacity-70' : ''}`}
                disabled={loggingIn}
                aria-label='Fazer login'
              >
                {loggingIn ? 'Logando...' : 'Login'}
              </button>
              <p className='text-sm font-light text-gray-500 dark:text-gray-400'>
                Ainda não tem uma conta ?{' '}
                <Link
                  href='/signup'
                  className='font-medium text-dracula-purple hover:underline dark:text-dracula-purple'
                >
                  Registre-se
                </Link>
              </p>
            </form>
          </div>
        </div>
      </div>
    </section>
  )
}
