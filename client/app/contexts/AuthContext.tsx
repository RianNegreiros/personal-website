import { createContext, useContext, useEffect, useState, ReactNode } from "react";

type AuthContextType = {
  isLoggedIn: boolean;
  setIsLoggedIn: (value: boolean) => void;
};

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export function AuthProvider({ children }: { children: ReactNode }) {
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);

  useEffect(() => {
    // Initialize the state from localStorage when running on the client-side
    const storedState = localStorage.getItem("isLoggedIn");
    setIsLoggedIn(storedState ? JSON.parse(storedState) : false);
  }, []); // Empty dependency array to ensure this runs only once on mount, not during SSR

  useEffect(() => {
    // Save the state to localStorage whenever it changes
    localStorage.setItem("isLoggedIn", JSON.stringify(isLoggedIn));
  }, [isLoggedIn]);

  return (
    <AuthContext.Provider value={{ isLoggedIn, setIsLoggedIn }}>
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth() {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
}
