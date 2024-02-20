"use client"

import { useRouter } from "next/navigation";
import { useAuth } from "../contexts/AuthContext";
import { toast } from 'react-toastify';
import { logoutUser } from "../utils/api";

export default function LogoutLink({ pathname }: { pathname: string }) {
  const { setIsAdmin, setIsLogged, isLogged } = useAuth();
  const router = useRouter();

  const handleLogout = async () => {
    try {
      await logoutUser();

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
        className: 'bg-dracula-pink',
        bodyClassName: 'text-dracula-pink',
      });
    } catch (error) {
      toast.error('Failed to log out. Please try again.', {
        position: "top-center",
        autoClose: 3000,
        className: 'bg-dracula-red',
        bodyClassName: 'text-dracula-pink',
      });
    }
  };

  if (pathname === "/signin" || pathname === "/signup" || !isLogged) {
    return null;
  }

  return (
    <button
      onClick={handleLogout}
      className="ml-4 text-sm px-4 py-2 rounded-lg bg-dracula-pink text-white hover:bg-dracula-pink-600"
    >
      Sair
    </button>
  );
}