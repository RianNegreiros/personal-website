import { createContext, useContext, useEffect, useState } from 'react';
import { autoLoginUser } from '../utils/api';

interface AuthContextType {
  isAdmin: boolean;
  setIsAdmin: React.Dispatch<React.SetStateAction<boolean>>;
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

async function autoLogin(setIsAdmin: React.Dispatch<React.SetStateAction<boolean>>) {
  const storedToken = localStorage.getItem('token');
  if (storedToken) {
    try {
      const data = await autoLoginUser(storedToken);
      setIsAdmin(data.isAdmin);
    } catch (error) {
    }
  }
}

export const AuthProvider: React.FC<AuthProviderProps> = ({ children }) => {
  const [isAdmin, setIsAdmin] = useState(false);

  useEffect(() => {
    autoLogin(setIsAdmin);
  }, []);

  return (
    <AuthContext.Provider value={{ isAdmin, setIsAdmin }}>
      {children}
    </AuthContext.Provider>
  );
};
