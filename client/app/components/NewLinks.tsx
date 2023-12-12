import Link from "next/link"
import { useAuth } from "../contexts/AuthContext";
import { logoutUser } from "../utils/api";
import { useRouter } from "next/navigation";

export default function NewLinks({ pathname }: { pathname: string }) {
  const { isAdmin, setIsAdmin, setIsLogged } = useAuth();
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

  if (pathname === "/signin" || pathname === "/signup") {
    return null;
  }

  const logoutLink = (
    <button
      onClick={handleLogout}
      className="ml-4 text-sm px-4 py-2 rounded-lg bg-teal-500 text-white hover:bg-teal-600"
    >
      Sair
    </button>
  );

  if (isAdmin) {
    if (pathname === "/posts") {
      return (
        <div>
          <Link
            href="/posts/new"
            className="text-sm px-4 py-2 rounded-lg border-2 border-teal-500 text-gray-500 hover:text-white hover:border-teal-600"
          >
            Criar Post
          </Link>
          {logoutLink}
        </div>
      );
    } else if (pathname === "/projects") {
      return (
        <div>
          <Link
            href="/projects/new"
            className="text-sm px-4 py-2 rounded-lg border-2 border-teal-500 text-gray-500 hover:text-white hover:border-teal-600"
          >
            Criar Projeto
          </Link>
          {logoutLink}
        </div>
      );
    }
  }
}