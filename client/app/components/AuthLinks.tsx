import Link from 'next/link'
import { useAuth } from '../contexts/AuthContext'

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
          className='hover:text-dracula-foreground rounded-lg border-2 border-dracula-purple px-4 py-2 text-sm text-gray-500 hover:bg-dracula-purple-800'
        >
          Fazer login
        </Link>
        <Link
          href='/signup'
          className='ml-2 rounded-lg bg-dracula-purple px-4 py-2 text-sm text-white hover:bg-dracula-purple-800'
        >
          Registrar-se
        </Link>
      </div>
    )
  }
}
