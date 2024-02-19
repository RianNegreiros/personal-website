import Link from "next/link"
import { useAuth } from "../contexts/AuthContext";

export default function AuthLinks({ pathname }: { pathname: string }) {
  const { isLogged } = useAuth();

  if (pathname === "/signin" || pathname === "/signup") {
    return null;
  }

  if (!isLogged) {
    return (
      <div>
        <Link
          href="/signin"
          className="text-sm px-4 py-2 rounded-lg border-2 border-dracula-pink text-gray-500 hover:text-dracula-foreground hover:border-gray-400"
        >
          Fazer login
        </Link>
        <Link
          href="/signup"
          className="text-sm px-4 py-2 rounded-lg bg-dracula-pink text-white hover:bg-gray-400 ml-2"
        >
          Registrar-se
        </Link>
      </div>
    );
  }
}