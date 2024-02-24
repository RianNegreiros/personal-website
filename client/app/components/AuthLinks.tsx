import Link from 'next/link'
import { useAuth } from '@/app/contexts/AuthContext'

export default function AuthLinks({ pathname }: { pathname: string }) {
  const { isLogged } = useAuth()

  if (pathname === '/signin' || pathname === '/signup') {
    return null
  }

  if (!isLogged) {
    return (
      <div>
        <Link
          href='/signin'
          className='mb-2 me-2 rounded-lg border border-gray-200 bg-white px-5 py-2.5 text-sm font-medium text-gray-900 transition-colors duration-200 ease-in-out hover:bg-gray-100 hover:text-dracula-purple-700 focus:z-10 focus:outline-none focus:ring-4 focus:ring-gray-100 dark:border-gray-600 dark:bg-gray-800 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white dark:focus:ring-gray-700'
        >
          Fazer login
        </Link>
        <Link
          href='/signup'
          className='mb-2 me-2 rounded-lg bg-dracula-purple-700 px-5 py-2.5 text-sm font-medium text-white transition-colors duration-200 ease-in-out hover:bg-dracula-purple-800 focus:outline-none focus:ring-4 focus:ring-dracula-cyan-300 dark:bg-dracula-purple-600 dark:hover:bg-dracula-purple-700 dark:focus:ring-dracula-purple-800'
        >
          Registrar-se
        </Link>
      </div>
    )
  }
}
