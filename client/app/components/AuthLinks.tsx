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
          className="text-sm px-4 py-2 rounded-lg border-2 border-teal-500 text-gray-500 hover:text-white hover:border-teal-600"
        >
          Fazer login
        </Link>
        <Link
          href="/signup"
          className="text-sm px-4 py-2 rounded-lg bg-teal-500 text-white hover:bg-teal-600 ml-2"
        >
          Registrar-se
        </Link>
      </div>
    );
  }
}