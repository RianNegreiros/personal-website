import { createContext, useContext, useEffect, useState } from 'react';

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
  const userId = localStorage.getItem('userId') || sessionStorage.getItem('userId');
  const isAdmin = localStorage.getItem('isAdmin') || sessionStorage.getItem('isAdmin');

  if (userId) {
    setIsLogged(true);
  }

  if (isAdmin) {
    setIsAdmin(true);
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
