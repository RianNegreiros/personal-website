'use client'

import { ThemeProvider } from 'next-themes'
import { ReactNode } from 'react'
import { AuthProvider } from '../contexts/AuthContext'
import { LoadingProvider } from '../contexts/LoadingContext'

export function Providers({ children }: { children: ReactNode }) {
  return (
    <ThemeProvider attribute='class'>
      <LoadingProvider>
        <AuthProvider>{children}</AuthProvider>
      </LoadingProvider>
    </ThemeProvider>
  )
}
