"use client"

import { useRouter } from "next/navigation";
import { useAuth } from "../contexts/AuthContext";
import { logoutUser } from "../utils/api";

export default function LogoutLink({ pathname }: { pathname: string }) {
  const { setIsAdmin, setIsLogged, isLogged } = useAuth();
  const router = useRouter();

  const handleLogout = async () => {
    try {
      await logoutUser();
      setIsAdmin(false);
      setIsLogged(false);
      localStorage.removeItem('token');
      localStorage.removeItem('userId');
      sessionStorage.removeItem('token');
      sessionStorage.removeItem('userId');
      router.push('/');
    } catch (error) {
      console.error('Logout failed:', error);
    }
  };

  if (pathname === "/signin" || pathname === "/signup" || !isLogged) {
    return null;
  }

  return (
    <button
      onClick={handleLogout}
      className="ml-4 text-sm px-4 py-2 rounded-lg bg-teal-500 text-white hover:bg-teal-600"
    >
      Sair
    </button>
  );
}