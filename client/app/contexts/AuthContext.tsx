import { createContext, useContext, useEffect, useState } from 'react';
import { autoLoginUser } from '../utils/api';

interface AuthContextType {
  isAdmin: boolean;
  isLogged: boolean;
  setIsAdmin: React.Dispatch<React.SetStateAction<boolean>>;
  setIsLogged: React.Dispatch<React.SetStateAction<boolean>>;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error('useAuth must be used within an AuthProvider');
  }
  return context;
};

interface AuthProviderProps {
  children: React.ReactNode;
}

async function autoLogin(
  setIsAdmin: React.Dispatch<React.SetStateAction<boolean>>,
  setIsLogged: React.Dispatch<React.SetStateAction<boolean>>
  ) {
  const storedToken = localStorage.getItem('token') || sessionStorage.getItem('token');
  if (storedToken) {
    try {
      const data = await autoLoginUser(storedToken);
      setIsAdmin(data.isAdmin);
      setIsLogged(true);
    } catch (error) {
    }
    setIsLogged(true);
  }
}

export const AuthProvider: React.FC<AuthProviderProps> = ({ children }) => {
  const [isAdmin, setIsAdmin] = useState(false);
  const [isLogged, setIsLogged] = useState(false);

  useEffect(() => {
    autoLogin(setIsAdmin, setIsLogged);
  }, []);

  return (
    <AuthContext.Provider value={{ isAdmin, isLogged, setIsAdmin, setIsLogged }}>
      {children}
    </AuthContext.Provider>
  );
};
