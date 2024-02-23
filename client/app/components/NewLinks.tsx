'use client'

import Link from 'next/link'
import { useAuth } from '../contexts/AuthContext'

export default function NewLinks({ pathname }: { pathname: string }) {
  const { isAdmin } = useAuth()
  if (isAdmin) {
    if (pathname === '/posts') {
      return (
        <div>
          <Link
            href='/posts/new'
            className='rounded-lg border-2 border-dracula-purple px-4 py-2 text-sm text-gray-500 hover:border-dracula-purple-600 hover:text-white'
          >
            Criar Post
          </Link>
        </div>
      )
    } else if (pathname === '/projects') {
      return (
        <div>
          <Link
            href='/projects/new'
            className='rounded-lg border-2 border-dracula-purple px-4 py-2 text-sm text-gray-500 hover:border-dracula-purple-600 hover:text-white'
          >
            Criar Projeto
          </Link>
        </div>
      )
    }
  }
}
