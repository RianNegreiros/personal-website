"use client"

import { useRouter } from "next/navigation";
import { useAuth } from "../contexts/AuthContext";
import { toast } from 'react-toastify';

export default function LogoutLink({ pathname }: { pathname: string }) {
  const { setIsAdmin, setIsLogged, isLogged } = useAuth();
  const router = useRouter();

  const handleLogout = async () => {
    try {

      setIsAdmin(false);
      setIsLogged(false);

      localStorage.removeItem('userId');
      localStorage.removeItem('isAdmin');
      sessionStorage.removeItem('userId');
      sessionStorage.removeItem('isAdmin');

      if (pathname !== "/") router.push('/');

      toast.success('Logged out successfully!', {
        position: "top-center",
        autoClose: 3000,
      });
    } catch (error) {
      toast.error('Failed to log out. Please try again.', {
        position: "top-center",
        autoClose: 5000,
      });
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