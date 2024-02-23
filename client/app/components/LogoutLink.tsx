'use client'

import { useRouter } from 'next/navigation'
import { useAuth } from '../contexts/AuthContext'
import { toast } from 'react-toastify'
import { logoutUser } from '../utils/api'

export default function LogoutLink({ pathname }: { pathname: string }) {
  const { setIsAdmin, setIsLogged, isLogged } = useAuth()
  const router = useRouter()

  const handleLogout = async () => {
    try {
      await logoutUser()

      setIsAdmin(false)
      setIsLogged(false)

      localStorage.removeItem('userId')
      localStorage.removeItem('isAdmin')
      sessionStorage.removeItem('userId')
      sessionStorage.removeItem('isAdmin')

      if (pathname !== '/') router.push('/')

      toast.success('Logged out successfully!', {
        className:
          'bg-white dark:bg-gray-900 text-dracula-purple hover:underline dark:text-dracula-purple',
      })
    } catch (error) {
      toast.error('Failed to log out. Please try again.', {
        className:
          'bg-white dark:bg-gray-900 text-dracula-purple hover:underline dark:text-dracula-purple',
      })
    }
  }

  if (pathname === '/signin' || pathname === '/signup' || !isLogged) {
    return null
  }

  return (
    <button
      onClick={handleLogout}
      className='ml-4 rounded-lg bg-dracula-purple px-4 py-2 text-sm text-white hover:bg-dracula-purple-600'
    >
      Sair
    </button>
  )
}
