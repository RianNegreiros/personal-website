import { createContext, useContext, useEffect, useState } from 'react'
import { checkSession } from '../utils/api'

interface AuthContextType {
  isAdmin: boolean
  isLogged: boolean
  setIsAdmin: React.Dispatch<React.SetStateAction<boolean>>
  setIsLogged: React.Dispatch<React.SetStateAction<boolean>>
}

const AuthContext = createContext<AuthContextType | undefined>(undefined)

export const useAuth = () => {
  const context = useContext(AuthContext)
  if (!context) {
    throw new Error('useAuth must be used within an AuthProvider')
  }
  return context
}

interface AuthProviderProps {
  children: React.ReactNode
}

async function autoLogin(
  setIsLogged: React.Dispatch<React.SetStateAction<boolean>>,
) {
  const session = await checkSession()

  if (session) {
    setIsLogged(true)
  } else {
    setIsLogged(false)
    localStorage.removeItem('userId')
    sessionStorage.removeItem('userId')

    localStorage.removeItem('isAdmin')
    sessionStorage.removeItem('isAdmin')
  }
}

export const AuthProvider: React.FC<AuthProviderProps> = ({ children }) => {
  const [isAdmin, setIsAdmin] = useState(false)
  const [isLogged, setIsLogged] = useState(false)

  useEffect(() => {
    autoLogin(setIsLogged)
    const userId =
      localStorage.getItem('userId') || sessionStorage.getItem('userId')
    const isAdmin =
      localStorage.getItem('isAdmin') || sessionStorage.getItem('isAdmin')
    if (isAdmin && userId) setIsAdmin(true)
  }, [])

  return (
    <AuthContext.Provider
      value={{ isAdmin, isLogged, setIsAdmin, setIsLogged }}
    >
      {children}
    </AuthContext.Provider>
  )
}
